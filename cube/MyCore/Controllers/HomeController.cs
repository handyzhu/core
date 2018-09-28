using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCore.Models;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using WebApi;
using System.Data;
using Microsoft.AspNetCore.Http;

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
            
            
            
            int count = dbHelp.db.Queryable<Login>().Where(i => i.ActiveFlag == "0" && i.UserID == UserId && i.Password == Md5.Encrypt(UserPsd)).ToList().Count;

            if (count == 1)
            {
                new WebApi.Service.LoginService().InsertLoginInfo(UserId);
                HttpContext.Session.SetString("id", UserId); 
                return RedirectToAction("ManagerIndex");
            }
            else {
                 
                TempData["msg"] = "账号或密码错误！";
                
                return RedirectToAction("Login");
                
            }

            
            
            //retu View("Success");
            
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult ManagerIndex()
        {
            
            string currectID = HttpContext.Session.GetString("id");
            DataTable dt = new WebApi.Service.IndexService().GetLeftMenu(currectID);

            string strSql = null;
            DataView dv = null;
            //strSql = "flagMenuType='0' AND pid='" & pid & "'"
            strSql = " pid in (1,2,3,4) ";
            dv = new DataView(dt, strSql, "sort", DataViewRowState.CurrentRows);
            


            return View();
        }

    }
}
