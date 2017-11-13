using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Collections.Specialized;

namespace timeKeeper
{
    public static class Settings
    {
        private static NameValueCollection appSettings = ConfigurationManager.AppSettings;

        public static string TfsConnectionUri
        {
            get
            {
                return appSettings["TfsUri"];
            }
        }

        public static string ProjectNames
        {
            get
            {
                return appSettings["ProjectName"];
            }
        }

        public static string TeamNames
        {
            get
            {
                return appSettings["TeamName"];
            }
        }
    }
}
