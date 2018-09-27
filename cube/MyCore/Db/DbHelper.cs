using SqlSugar;
using Infrastructure;
using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace MyCore.Db
{
    public class DbHelper
    {
        public SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
        {
            ConnectionString = ConfigHelper.GetSectionValue("ConnectionStrings:DefaultConnection"),//必填, 数据库连接字符串
            DbType = DbType.SqlServer,         //必填, 数据库类型
            IsAutoCloseConnection = true,    //默认false, 时候知道关闭数据库连接, 设置为true无需使用using或者Close操作
            InitKeyType = InitKeyType.SystemTable    //默认SystemTable, 字段信息读取, 如：该属性是不是主键，是不是标识列等等信息
        });


    }


    public static class ConfigHelper
    {
        private static IConfiguration _configuration;

        static ConfigHelper()
        {
            //在当前目录或者根目录中寻找appsettings.json文件
            var fileName = "appsettings.json";

            var directory = AppContext.BaseDirectory;
            directory = directory.Replace("\\", "/");

            var filePath = $"{directory}/{fileName}";
            if (!File.Exists(filePath))
            {
                var length = directory.IndexOf("/bin");
                filePath = $"{directory.Substring(0, length)}/{fileName}";
            }

            var builder = new ConfigurationBuilder()
                .AddJsonFile(filePath, false, true);

            _configuration = builder.Build();
        }

        public static string GetSectionValue(string key)
        {
            return _configuration.GetSection(key).Value;
        }
    }

}
