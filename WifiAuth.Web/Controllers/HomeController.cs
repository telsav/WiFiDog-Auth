﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Sqlite;
using Dapper;
using WifiAuth.Web.Services;

namespace WifiAuth.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;

        public HomeController(
            IEmailSender emailSender,
            ISmsSender smsSender)
        {
            _emailSender = emailSender;
            _smsSender = smsSender;
        }

        public IActionResult Index()
        {
            using (SqliteConnection conn = new SqliteConnection(@"Data Source=E:/Database/SQLite/default.db;Cache=Shared"))
            {
                conn.Open();
                var d = conn.Query("SELECT * FROM FOO;");
            }

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
        
        //[HttpGet("api/smscode")]
        public async Task<IActionResult> SendCode(string phoneNumber)
        {
            string result = await _smsSender.SendTemplateSmsAsync(phoneNumber, 14756, DateTime.Now.Millisecond.ToString());
            return Content(result);
        }
    }
}
