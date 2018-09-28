using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Service
{
    public class LoginService
    {
        /// <summary>
        /// 记录登陆信息
        /// </summary>
        /// <param name="id">工号</param>
        /// <returns></returns>
        public bool InsertLoginInfo(string id) {
            string sql = "insert into tabemployeeonline(employeeid,logintime) values('" + id + "','" + System.DateTime.Now + "')";
            return Common.SQLHelper.ExcuteSQL(sql)>0;
        }
    }
}
