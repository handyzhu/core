using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class Globals
    {
        public static IServiceProvider ServiceProvider { get; set; }
        /// <summary>
        /// 应用程序配置
        /// </summary>
        public static IConfigurationRoot Configuration { get; set; }

        public static string token { get; set; }

    }
}
