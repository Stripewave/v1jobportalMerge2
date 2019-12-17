using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Collections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using System.Security.Cryptography;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using v1jobportal.Controllers;
using MailKit.Net.Smtp;
using MimeKit;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using System.Security.Cryptography;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using v1jobportal.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Data.SqlClient;
using v1jobportal.Data;
using v1jobportal.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections;

namespace v1jobportal.Controllers
{
    public class CamelotPasswordController : Controller
    {
        SqlConnection con = new SqlConnection("Server=OLYMPUS-CLOUD\\SQLEXPRESS;Initial Catalog=JobPortal;Integrated Security=True");
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(string rimxeus)
        {

            CookieOptions cookies = new CookieOptions();
            cookies.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("_JAR_reset_conf", rimxeus);

            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> ConfirmAccount(string rimxeus)
        {

            CookieOptions cookies = new CookieOptions();
            cookies.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("_confit", rimxeus);

            string query = "UPDATE AspNetUsers SET EmailConfirmed='1' WHERE Id='" + rimxeus + "'";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                con.Open();

                var TY = await cmd.ExecuteNonQueryAsync();
                con.Close();
            }

            return View();
        }

        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> change_password([FromForm] string validation, string NewPassword)
        {
            var EncryptNewPassword = HashPassword(NewPassword);

            string query = "UPDATE AspNetUsers SET PasswordHash='" + EncryptNewPassword + "' WHERE Id='" + validation + "'";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Connection = con;
                con.Open();

                var TY = cmd.ExecuteNonQueryAsync();
                ViewBag.VB = validation;
                con.Close();
            }

            return View();
        }
        
    }
}