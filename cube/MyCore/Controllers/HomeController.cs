using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCore.Models;
using Infrastructure;
using Microsoft.Extensions.Configuration;
namespace MyCore.Controllers
{
    public class HomeController : Controller
    {
        Db.DbHelper dbHelp = new Db.DbHelper();
        public IActionResult Index()
        {
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
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <returns></returns>
        public IActionResult Login() {
            
           
            return View();
        }

        /// <summary>
        /// 登陆验证
        /// </summary>
        /// <returns></returns>
        public IActionResult LoginVerify(string UserId, string UserPsd) {
            
            TempData["id"] = UserId;
            TempData["pwd"]= Md5.Encrypt(UserPsd);

            
            int count = dbHelp.db.Queryable<Login>().Where(i => i.ActiveFlag == "0" && i.UserID == UserId && i.Password == Md5.Encrypt(UserPsd)).ToList().Count;

            if (count == 1)
            {
                TempData["result"] = "ok";
            }
            else {
                TempData["result"] = "no";
            }

            return RedirectToAction("Success");
            
            //retu View("Success");
            
        }

        public IActionResult Success()
        {
            return View();
        }

    }
}
