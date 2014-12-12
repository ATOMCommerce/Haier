using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Goexw.Config
{
    public class SystemConfig
    {
        public static string AtomComRoot
        {
            get
            {
                string root = ConfigurationManager.AppSettings["AtomComRoot"];
                if (string.IsNullOrEmpty(root))
                {
                    throw new ArgumentNullException("AtomComRoot", "Configuration for Atom Commerce Root can't be empty string.");
                }
                else if (!root.EndsWith("/"))
                {
                    root += "/";
                }

                return root;
            }
        }

        public static string ChannelId
        {
            get
            {
                return "BZ";
            }
        }

        public static string DefaultCust
        {
            get
            {
                string name = ConfigurationManager.AppSettings["Customer"];
                if (string.IsNullOrEmpty(name))
                {
                    name = "MSAAuthUserID";
                }

                return name;
            }
        }
    }
}