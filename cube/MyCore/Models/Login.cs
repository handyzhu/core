﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;
using SqlSugar;

namespace MyCore.Models
{
    [SugarTable("tabEmployee")]
    public class Login
    {

        public Login() { }
        public Login(string userid,string pwd,string activeflag) {
            userid = UserID;
            pwd = Md5.Encrypt(pwd);
            activeflag = ActiveFlag;
        }
        /// <summary>
        /// 工号
        /// </summary>
        [SugarColumn(ColumnName = "employeeid")]
        public string UserID { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [SugarColumn(ColumnName = "password")]
        public string Password { get; set; }

        /// <summary>
        /// 在职状态 0:在职 1:离职 2：离职中
        /// </summary>
        public string ActiveFlag { get; set; }
    }
}
