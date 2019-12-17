using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using v1jobportal.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using v1jobportal.Models;
using System.Data.SqlClient;
using System.Collections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using System.Security.Cryptography;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using v1jobportal.Controllers;
//using MailKit.Net.Smtp;
//using MimeKit;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;



namespace v1jobportal.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        SqlConnection con = new SqlConnection("Server=OLYMPUS-CLOUD\\SQLEXPRESS;Initial Catalog=JobPortal;Integrated Security=True");
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "AllJobListings");

        }
        [AcceptVerbs("Get","Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await userManager.FindByNameAsync(email);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email { email} is already registered, try Another email");
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]

       
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email};
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    string ID = user.Id;

                    CookieOptions cookies = new CookieOptions();
                    cookies.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Append("_JAR_user", ID);
                    Response.Cookies.Append("_JAR_user_status", "02");
                    Response.Cookies.Append("_JAR_user_mail", user.Email);

                    // Add all new users to the User role
                    await userManager.AddToRoleAsync(user,"User");
                    //ignore--await signInManager.SignInAsync(user, isPersistent: false);
                    if (user.Email.StartsWith("admin"))
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }

                    return RedirectToAction("index", "AllJobListings");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                    
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    var result1 = await userManager.FindByEmailAsync(model.Email);
                    var roles = await userManager.GetRolesAsync(result1);
                    return RedirectToAction("Index", "AllJobListings");
                }

                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt Details");

            }
            return View(model);
        }

        public async Task<IActionResult> reset_password(string rimxeus)
        {

            CookieOptions cookies = new CookieOptions();
            cookies.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("_JAR_reset_conf", rimxeus);

            return View();
        }

        

        [AllowAnonymous]
        public IActionResult RessetPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult ResetRequest1([FromForm] string RequestingEmail)
        {
            string query = "SELECT Id FROM AspNetUsers WHERE Email='"+RequestingEmail+"'";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();
                var TY = cmd.ExecuteReader();

                ArrayList al = new ArrayList();

                while (TY.Read())
                {
                    object[] values = new object[TY.FieldCount];
                    TY.GetValues(values);
                    al.Add(values);
                }

                TY.Close();
                con.Close();

                string jsonString;
                jsonString = JsonSerializer.Serialize(al);
                CookieOptions cookies = new CookieOptions();
                cookies.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Append("_JAR_reset", jsonString);

                con.Close();
            }

            return View();
        }
    }
}
