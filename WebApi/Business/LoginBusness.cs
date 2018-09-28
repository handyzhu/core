using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Business
{
    public class LoginBusness
    {
        /// <summary>
        /// 获取子目录
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public DataView getdvSubGroupByPid(DataTable dt, string pid)
        {
            string strSql = null;
            DataView dv = null;
            //strSql = "flagMenuType='0' AND pid='" & pid & "'"
            strSql = " pid='" + pid + "'";


            dv = new DataView(dt, strSql, "sort", DataViewRowState.CurrentRows);
            return dv;
        }
    }
}
