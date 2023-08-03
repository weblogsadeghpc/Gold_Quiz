using AutoMapper;
using Gold_Quiz.CommonLayer.PublicClasses;
using Gold_Quiz.DataModel.Entities;
using Gold_Quiz.DataModel.Models;
using Gold_Quiz.DataModel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Gold_Quiz.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]
    public class StudentManagementController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly UserManager<ApplicationUsers> _userManager;
        private readonly ICenterRepository _center;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hosting;

        public StudentManagementController(IUnitOfWork context, UserManager<ApplicationUsers> userManager,
            ICenterRepository center, IMapper mapper, IWebHostEnvironment hosting)
        {
            _context = context;
            _userManager = userManager;
            _center = center;
            _mapper = mapper;
            _hosting = hosting;
        }
        public IActionResult Index()
        {
            var model = _context.centerUsersUW.
                Get(c => c.CenterAdminID == _userManager.GetUserId(HttpContext.User) && c.Users_ST.UserType == 3, "Users_ST");
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentViewModel model)
        {
            if (ModelState.IsValid) // yani hame etelaat be dorosti vared shode bood
            {
                using (var tr = _context.BaseTransaction())
                {
                    // نقش عملیات را دارد
                    //Register
                    try
                    {
                        var getCenterID = _center.GetCenterID(_userManager.GetUserId(HttpContext.User));

                        var user = await _userManager.FindByNameAsync(model.UserName);
                        if (user != null)
                        {
                            // user peyda shod
                            ModelState.AddModelError("UserName", "شماره تماس شما در سیستم ثبت شده است .");// enteha neshan bede hamoon akhar ke ConfirmPassword neveshtim 
                            return View(model);
                        }

                        var mapUser = _mapper.Map<ApplicationUsers>(model);
                        mapUser.PhoneNumber = model.UserName; // phone number hamoon username ast
                        mapUser.UserType = 3;//Student
                        mapUser.IsActive = 1;// Active

                        //amaliate sabte nam
                        // 1 - ثبت کاربر در دیتابیس
                        IdentityResult result = await _userManager.CreateAsync(mapUser, "123456");// karbar dar in khat sabte nam mishe // password sade gharar midim bar aval 
                        if (result.Succeeded)
                        {
                            // 2 - ثبت نقش برای کاربر
                            await _userManager.AddToRoleAsync(mapUser, "Student");// be karbar mapuser yek role bede // Role admin be user dadim 

                            // 3 - ثبت اطلاعات دانش آموز در مرکز
                            CenterUsers CU = new CenterUsers
                            {
                                CenterAdminID = _userManager.GetUserId(HttpContext.User),// hamin usr ke dare etelaat ro submit mikone user admin ast 
                                CenterID = getCenterID,
                                CenterUserID = mapUser.Id, // id modares ya daneshamooz
                                UserType = 3
                            };
                            _context.centerUsersUW.Create(CU);

                        }
                        tr.commit();
                        _context.Save();

                        // estefade az transaction ha baraye inke ya hamash anjam beshe ya aslan anjam nashe chon mesalan vasatesh internet ghat shod moshke nakhore mesalan Role sakhte nashe az transaction estefade mikonim ke mesle yek tarakonesh kar mikone

                        return RedirectToAction("Index");
                        // 3 ja etelaat sabt mishe : 
                        //1- Role
                        //2- User
                        //3- Centers
                    }
                    catch (Exception)
                    {
                        tr.RollBack(); // hich kari anjam nade
                        return RedirectToAction("Error", "Home"); // agar error didi boro be controller => home action => error 

                    }
                }
            }
            else
            {
                //namayesh khataha
                return View(model);
            }

        }

        [HttpGet]
        public IActionResult UploadAndImportExcell()
        {
            return PartialView("_uploadAndImportExcell");
        }

        public IActionResult ExcellImport(int cellCount = 4) // tedad sotoon haye file excell ke 4 soton ast 
        {
            IFormFile file = Request.Form.Files[0]; // mitonim az tarighe in file daryaft konim 
            PublicVariable.GetExcell.Clear(); // agar etelaati ya dataii dare hazf bokon felan chon static ast

            // تغییر اسم فایل دریافتی 
            string MyFileName = Path.GetFileNameWithoutExtension(file.FileName.ToString() + "_" +
                Guid.NewGuid() + Path.GetExtension(file.FileName.ToString()));// esmesho taghir bedim name nabayad dar server tekrari bashe 

            // zakhire dar bakhsh excel file wwwroot
            string foldername = "excelfile";
            // boro dar root project va file ke inja taghir dadi upload kon dar project
            string webRootpath = _hosting.WebRootPath;  // baraye kar ba root project
            string newPath = Path.Combine(webRootpath, foldername);
            StringBuilder sb = new StringBuilder();
            if (!Directory.Exists(newPath))
            { // agar Directory vojod nadasht 
                //ijadesh bokon 
                Directory.CreateDirectory(newPath);
                // farze inke folder va ina nabood 
            }
            if (file.Length > 0)
            {
                string sFileExtension = Path.GetExtension(file.FileName).ToLower();
                ISheet sheet;
                string fullPath = Path.Combine(newPath, MyFileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    stream.Position = 0;
                    if (sFileExtension == ".xls")
                    {
                        HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                    }
                    else
                    {
                        XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                    }
                    IRow headerRow = sheet.GetRow(0); //Get Header Row
                    //cellCount = headerRow.LastCellNum;
                    sb.Append("<table class='table'><tr>");
                    for (int j = 0; j < cellCount; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
                        if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                        sb.Append("<th>" + cell.ToString() + "</th>");
                    }
                    sb.Append("</tr>");
                    sb.AppendLine("<tr>");
                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;
                        if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;


                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (row.GetCell(j) != null)
                            {
                                sb.Append("<td>" + row.GetCell(j).ToString() + "</td>");
                                PublicVariable.GetExcell.Add(row.GetCell(j).ToString());
                            }
                            else
                            {
                                sb.Append("<td></td>");
                                PublicVariable.GetExcell.Add("");
                            }
                        }
                        sb.AppendLine("</tr>");
                    }
                    sb.Append("</table>");
                }
            }

            return Json(new { rsb = sb.ToString(), status = "success", filename = MyFileName });
        }
    }
}
