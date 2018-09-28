using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Service
{
    public class IndexService
    {
        /// <summary>
        /// 获得所有目录
        /// </summary>
        /// <param name="id">工号</param>
        /// <returns></returns>
        public DataTable GetLeftMenu(string id) {
            string sql =string.Format( @"SELECT EDS_SYS_Menu.rid, EDS_SYS_Menu.itemid, EDS_SYS_Menu.menu_id, EDS_SYS_Menu.pid, EDS_SYS_Menu.menu_name
	, EDS_SYS_Menu.flagMenuType, EDS_SYS_Menu.purviewType, EDS_SYS_Menu.sort, EDS_SYS_Menu.url, EDS_SYS_Menu.flagPortal
	, EDS_SYS_Menu.flagMenuType, EDS_SYS_Menu.flagCtrl, EDS_SYS_Menu.flagType, EDS_SYS_Menu.sort, EDS_SYS_Menu.detail
	, EDS_SYS_Menu.mark
FROM EDS_SYS_Menu
WHERE EDS_SYS_Menu.flagUse = '1'
	AND (EDS_SYS_Menu.purviewType = '1'
		OR flagMenuType = '0')
	OR EDS_SYS_Menu.purviewType = '0'
	AND rid IN (
		SELECT menu_id
		FROM EDS_SYS_RolePurview
			INNER JOIN EDS_SYS_PersonRole
			ON EDS_SYS_RolePurview.roleid = EDS_SYS_PersonRole.roleid
				AND EDS_SYS_PersonRole.personid = '{0}'
	)
	OR rid IN (
		SELECT menu_id
		FROM EDS_SYS_RolePurview
		WHERE EDS_SYS_RolePurview.flagType = 'G'
	)
	OR rid IN (
		SELECT menu_id
		FROM EDS_SYS_PersonPurview
		WHERE EDS_SYS_PersonPurview.personid = '{0}'
	)
	AND flagPortal = 0
ORDER BY EDS_SYS_Menu.flagMenuType, EDS_SYS_Menu.SORT", id);

            return Common.SQLHelper.GetTable(sql);
        }
    }
}
