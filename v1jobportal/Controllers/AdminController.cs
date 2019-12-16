using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using v1jobportal.Models;
using v1jobportal.Controllers;
using v1jobportal.Data;
using System.Web.Helpers;


namespace v1jobportal.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public static string DecryptSystem(string cipherText, string keyString)
        {
            var fullCipher = Convert.FromBase64String(cipherText);

            var iv = new byte[16];
            var cipher = new byte[16];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                {
                    string result;
                    using (var msDecrypt = new MemoryStream(cipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return result;
                }
            }
        }



        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> DisplayJobContent_ByID(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var AvailableJob_Data = await _context.AllJobListings.FindAsync(id);
            if (AvailableJob_Data == null)
            {
                return NotFound();
            }
            return View(AvailableJob_Data);
        }



       // public string JobLink(string camelot)
        public async Task<IActionResult> JobLink(string camelot)
        {

            var DecryptedJobID = DecryptSystem(camelot, "E546C8DF278CD5931069B522E695D4F2");
            var JobDataFound = Int32.Parse(DecryptedJobID);

            //var AvailableJob_Data = _context.AllJobListings.Where(a => a.Id = JobDataFound);
            var AvailableJob_Data = await _context.AllJobListings.FindAsync(JobDataFound);

            if (AvailableJob_Data == null)
            {
                return NotFound();
            }
            return View(AvailableJob_Data);
        }


        public static string EncryptString(string text, string keyString)
        {
            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                        return Convert.ToBase64String(result);
                    }
                }
            }
        }
        public IActionResult GenarateJobLinks()
        {
            var request = HttpContext.Request;
            ViewBag.JobID_Found = "https://localhost:44384/Admin/JobLink?camelot=" + EncryptString(Request.Cookies["loged_job"].ToString(), "E546C8DF278CD5931069B522E695D4F2")+"&hydra_ramela="+ EncryptString("command-1", "E546C8DF278CD5931069B522E695D4F2")+"xctm-route="+ EncryptString("cracken-hades", "E546C8DF278CD5931069B522E695D4F2");

            return View();
        }

    }
}