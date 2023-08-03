using AutoMapper;
using Gold_Quiz.DataModel.Entities;
using Gold_Quiz.DataModel.Models;
using Gold_Quiz.DataModel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Security.Policy;
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

        public StudentManagementController(IUnitOfWork context, UserManager<ApplicationUsers> userManager, ICenterRepository center, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _center = center;
            _mapper = mapper;
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
    }
}
