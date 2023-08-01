using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Gold_Quiz.DataModel.Entities;
using Gold_Quiz.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gold_Quiz.DataModel;
using Gold_Quiz.DataModel.Services;

namespace Gold_Quiz.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUsers> _userManager;
        private readonly SignInManager<ApplicationUsers> _SignInManager;
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;

        //baraye estefade az in do class bayad initial bokonim tazrigh bokonim :

        //tazrighe vabastegi 
        public AccountController(UserManager<ApplicationUsers> userManager, IUnitOfWork context, SignInManager<ApplicationUsers> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _SignInManager = signInManager;
            _mapper = mapper;
            _context = context;
            //  mitonim az motaghayere mapper estefade konim khili ham sade
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // attribute baraye bahse amniate // baes mishe vaghti kasi login mikone tavasote token shenasaii beshe // token haye mokhtalef baraye yek user gheyre motabar ast
        public async Task<IActionResult> Login(LoginViewModel model) // model ro misfrestim samte server
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    ModelState.AddModelError("UserName", "اطلاعات وارد شده صحیح نمی باشد ."); // inja ke neveshtim password yani dar zire password khata haro neshoon bede // bakhshe asp-validation-for
                    return View(model);
                }
                else // agar user peyda shod
                {
                    var result = await _SignInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent: false, lockoutOnFailure: false);//mige user name chie migim user password fovomi sevomi ham bayae mara be khater bespar ast chaharomi hame mige age mesalan s3 bar ramz eshtebah zad user block beshe 
                    if (result.Succeeded)
                    {
                        //Login
                        if (user.IsActive == 0)
                        {// user accountesh enable nashode bood
                            ModelState.AddModelError("UserName", "اکانت شما در حال بررسی میباشد هنوز فعال نشده است ."); // inja ke neveshtim password yani dar zire password khata haro neshoon bede // bakhshe asp-validation-for
                            return View(model);
                        }
                        if (user.UserType == 1)
                        {
                            //Admin
                            return Redirect("AdminPanel/AdminDashboard/Index");
                            // Redirect to action baraye yek area hast mamoolan va inja kar nemikone
                            // yani redirect dakheli 
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("UserName", "اطلاعات وارد شده صحیح نمی باشد .");
                        return View(model);
                    }
                }
            }
            return View(model); // namayesh khata

        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // dar zamane estefade az  identity : (bayad tavjoh kard) 
            // baraye  ehraz hoviate user dota class darim : 

            if (ModelState.IsValid) // yani hame etelaat be dorosti vared shode bood
            {
                //Register
                try
                {
                    // aval control inke karbar dobare sabt name nakarde bashe
                    var user = await _userManager.FindByNameAsync(model.UserName);
                    //mige boro to database bebin in shomare telephone ro peyda mikoni ya na 
                    //chon az dastore await estefade kardim bayad kole method ro async konim 
                    // be sorate barname nevisi nahamzaman
                    if (user != null)
                    {
                        // user peyda shod
                        ModelState.AddModelError("ConfirmPassword", "شماره تماس شما در سیستم ثبت شده است .");// enteha neshan bede hamoon akhar ke ConfirmPassword neveshtim 
                        return View(model);
                    }

                    // auto maper ro khodesh bind mikone
                    // che cizi ro beriz dar application users model ro beriz 
                    //albate bayad moshakhas kard register view model be kodom jadval rikhte beshe
                    var mapUser = _mapper.Map<ApplicationUsers>(model);
                    mapUser.PhoneNumber = model.UserName; // phone number hamoon username ast
                    mapUser.UserType = 1;//Admin // kesi ke dare sabte nam mikone admin ast
                    mapUser.IsActive = 0;// deActive

                    //amaliate sabte nam
                    // 1 - ثبت کاربر در دیتابیس
                    IdentityResult result = await _userManager.CreateAsync(mapUser, model.Password);// karbar dar in khat sabte nam mishe
                    if (result.Succeeded)
                    {
                        // 2 - ثبت نقش برای کاربر
                        await _userManager.AddToRoleAsync(mapUser, "Admin");// be karbar mapuser yek role bede // Role admin be user dadim 

                        // 3 - ثبت مرکز در دیتابیس 
                        Centers C = new Centers
                        {
                            CenterName = model.CustomerName,
                            CenterType = 1,
                            CenterAdminID = mapUser.Id
                        };
                        _context.centersUW.Create(C);
                        _context.Save();

                        // estefade az transaction ha baraye inke ya hamash anjam beshe ya aslan anjam nashe chon mesalan vasatesh internet ghat shod moshke nakhore mesalan Role sakhte nashe az transaction estefade mikonim ke mesle yek tarakonesh kar mikone

                        return RedirectToAction("SuccesfullyRegister");
                    }
                    return RedirectToAction("Error", "Home");
                    // class identity result natijeye amaliat ro negah midarad

                    //----------------------------------------------------------------------------//

                    //ApplicationUsers u = new ApplicationUsers
                    //{

                    //    FirstName = model.FirstName,
                    //    Family = model.Family,
                    //    PhoneNumber = model.UserName,
                    //    UserName = model.UserName,

                    //};
                }
                catch (Exception)
                {
                    return RedirectToAction("Error", "Home"); // agar error didi boro be controller => home action => error 

                }
            }
            else
            {
                //namayesh khataha
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult SuccesfullyRegister()
        {
            return View();
        }
    }
}
