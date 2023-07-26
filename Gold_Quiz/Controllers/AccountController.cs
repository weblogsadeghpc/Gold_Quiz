using AutoMapper;
using Gold_Quiz.DataModel.Entities;
using Gold_Quiz.DataModel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;

namespace Gold_Quiz.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUsers> _userManager;
        private readonly SignInManager<ApplicationUsers> _SignInManager;
        private readonly IMapper _mapper;

        //baraye estefade az in do class bayad initial bokonim tazrigh bokonim :

        //tazrighe vabastegi 
        public AccountController(UserManager<ApplicationUsers> userManager, SignInManager<ApplicationUsers> SignInManager, IMapper mapper)
        {
            _userManager = userManager;
            _SignInManager = SignInManager;
            _mapper = mapper;
            //  mitonim az motaghayere mapper estefade konim khili ham sade
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
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
            //baraye  ehraz hoviate user dota class darim : 

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
                    mapUser.IsActive = false;// deActive

                    //amaliate sabte nam
                    //ثبت نام کاربر
                    IdentityResult result = await _userManager.CreateAsync(mapUser, model.Password);// karbar dar in khat sabte nam mishe
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(mapUser, "Admin");// be karbar mapuser yek role bede
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
    }
}
