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
using System.Text;

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
                

                LoginInfo userInfo = new LoginInfo();
                userInfo = dbHelp.db.Queryable<LoginInfo>().Where(i => i.UserID == UserId).First();

                HttpContext.Session.SetString("id", UserId);

                HttpContext.Session.SetString("user", Infrastructure.Json.ToJson(userInfo));
                    
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


            StringBuilder sb = new StringBuilder();
            foreach (DataRow row in dv.ToTable().Rows)
            {
                string groupid = row["itemid"].ToString();
                string groupName = row["menu_name"].ToString();
              

                DataView dvSecond = new WebApi.Business.LoginBusness().getdvSubGroupByPid(dt, groupid);
                if (dvSecond.Count > 0) {
                    sb.Append("<li class=\"layui-nav-item layui-nav-itemed\">\r\n");
                    sb.Append("<a class=\"\" href=\"javascript:;\">" + groupName + "</a>\r\n");
                    sb.Append("<dl class=\"layui-nav-child\">\r\n");
                    foreach (DataRow row2 in dvSecond.ToTable().Rows)
                    {
                        string subgroupid = row2["itemid"].ToString();
                        string subgroupName = row2["menu_name"].ToString();
                        sb.Append("<dd><a href =\"javascript:; \" >"+subgroupName+ "</a></dd>\r\n");                                                                                          
                    }
                    sb.Append("</dl>\r\n");
                    sb.Append("</li>\r\n");
                }
                
            }

            LoginInfo user=Infrastructure.Json.ToObject<LoginInfo>( HttpContext.Session.GetString("user"));

            ViewData["left"] = sb.ToString();
            return View();
        }



        
      

    }
}
