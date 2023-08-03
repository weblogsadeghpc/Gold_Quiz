using AutoMapper;
using Gold_Quiz.DataModel.Entities;
using Gold_Quiz.DataModel.Models;
using Gold_Quiz.DataModel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Gold_Quiz.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]
    public class TeacherManagementController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly UserManager<ApplicationUsers> _userManager;
        private readonly IMapper _mapper;
        private readonly ICenterRepository _center;
        private readonly IUserRepository _user;

        public TeacherManagementController(IUnitOfWork context, ICenterRepository center,
            UserManager<ApplicationUsers> userManager, IMapper mapper, IUserRepository user)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _center = center;
            _user = user;
        }
        public IActionResult Index()
        {
            var model = _context.centerUsersUW.
                Get(c => c.CenterAdminID == _userManager.GetUserId(HttpContext.User) && c.Users_ST.UserType == 2, "Users_ST");
            // hamin admin id ke toye system hast ro bia neshon bede users haye khodesh ro neshon bede
            // dar centerusers be name moalem dastresi nadaim join ham bayad konim 
            // listi  az center users vali ba shart
            // && List moalemin bedone && feghat list kole karbaran ast 2 = moalem , 1 = Admin ..
            return View(model);
        }

        [HttpGet] // action method az type get
        public IActionResult Create()
        {
            CourseList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeacherViewModel model, int[] CourseID) // yek vorodi array integer dare be sorate vorodi 
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
                        if (CourseID.Length == 0)
                        {
                            ModelState.AddModelError("CourseID", "لطفا حداقل یک درس انتخاب کنید .");
                            CourseList();
                            return View(model);
                        }
                        var user = await _userManager.FindByNameAsync(model.UserName);
                        if (user != null)
                        {
                            // user peyda shod
                            ModelState.AddModelError("UserName", "شماره تماس شما در سیستم ثبت شده است .");// enteha neshan bede hamoon akhar ke ConfirmPassword neveshtim 
                            return View(model);
                        }

                        var mapUser = _mapper.Map<ApplicationUsers>(model);
                        mapUser.PhoneNumber = model.UserName; // phone number hamoon username ast
                        mapUser.UserType = 2;//Teacher
                        mapUser.IsActive = 1;// Active

                        //amaliate sabte nam
                        // 1 - ثبت کاربر در دیتابیس
                        IdentityResult result = await _userManager.CreateAsync(mapUser, "123456");// karbar dar in khat sabte nam mishe // password sade gharar midim bar aval 
                        if (result.Succeeded)
                        {
                            // 2 - ثبت نقش برای کاربر
                            await _userManager.AddToRoleAsync(mapUser, "Teacher");// be karbar mapuser yek role bede // Role admin be user dadim 

                            // 3 - ثبت اطلاعات مدرس در مرکز
                            CenterUsers CU = new CenterUsers
                            {
                                CenterAdminID = _userManager.GetUserId(HttpContext.User),// hamin usr ke dare etelaat ro submit mikone user admin ast 
                                CenterID = getCenterID,
                                CenterUserID = mapUser.Id, // id modares ya daneshamooz
                                UserType = 2
                            };
                            _context.centerUsersUW.Create(CU);

                            //ثبت دروس برای معلم - 4 
                            for (int i = 0; i < CourseID.Length; i++)
                            {
                                TeacherCourse TC = new TeacherCourse
                                {
                                    TeacherID = mapUser.Id,  // 
                                    CenterID = getCenterID,
                                    CourseID = CourseID[i],
                                    TeacherAdminID = _userManager.GetUserId(HttpContext.User) // useri ke alan dare sabt etelaat anjam mide
                                };
                                _context.teacherCourseUW.Create(TC); // har bar darsi sabt mishe bayad baraye moalem ham sabt beshe
                            }

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

        private void CourseList()
        {
            List<Courses> lstcourses = _context.coursesUW.Get().ToList(); //listi az course ro mikhaim namayesh bedim 
                                                                          //Courses mdl = new Courses
                                                                          //{
                                                                          //    CourseID = -1,
                                                                          //    CourseName = "... انتخاب درس"
                                                                          //    // satr ezafe kardim 
                                                                          //};
                                                                          //lstcourses.Insert(0, mdl);
            ViewBag.CourseList = lstcourses;
        }

        [HttpGet]
        public IActionResult Edit(string TeacherID)
        {
            if (TeacherID == null)
            {
                return RedirectToAction("Error", "Home");
            }
            var Teacher = _mapper.Map<TeacherViewModel>(_context.userUW.GetById(TeacherID));
            // az class application users ast vali dar view az teacherviewmodel estefade kardim pas bayad az mapper estefade konim 
            Teacher.UserID = TeacherID;
            CourseList(); // bayad seda zade shavad
            return View(Teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TeacherViewModel model, int[] CourseID)
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
                        if (CourseID.Length == 0)
                        {
                            ModelState.AddModelError("CourseID", "لطفا حداقل یک درس انتخاب کنید .");
                            CourseList();
                            return View(model);
                        }

                        //1 - حذف دروس معلم 
                        var TeachercourseList =
                            _context.teacherCourseUW.Get(t => t.TeacherID == model.UserID).ToList(); // list az doros moalemi ke entekhab kardim 
                        if (TeachercourseList.Count > 0)
                        {
                            _context.teacherCourseUW.DeleteByRange(TeachercourseList);
                            _context.Save();
                        }

                        //ثبت دروس برای معلم - 2 
                        for (int i = 0; i < CourseID.Length; i++)
                        {
                            TeacherCourse TC = new TeacherCourse
                            {
                                TeacherID = model.UserID,  // 
                                CenterID = getCenterID,
                                CourseID = CourseID[i],
                                TeacherAdminID = _userManager.GetUserId(HttpContext.User) // useri ke alan dare sabt etelaat anjam mide
                            };
                            _context.teacherCourseUW.Create(TC); // har bar darsi sabt mishe bayad baraye moalem ham sabt beshe
                        }

                        // 3 - ویرایش اطلاعات کاربر 
                        // نام و نام خانوادگی تغییر 
                        _user.UpdateUserInfo(model); // dastor save ro inja neveshtim _context.Save() ro bar midarim 


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
    }
}
