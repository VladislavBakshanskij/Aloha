using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Aloha.Helpers {
    public static class Inforamtion {
        public static string Copyright {
            get {
                object[] attributes = Assembly.GetExecutingAssembly()
                                                .GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                    return "";
                else
                    return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public static string Version {
            get {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public static string Description {
            get {
                object[] attributes = Assembly.GetExecutingAssembly()
                                                .GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                    return "";
                else
                    return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public static string Product {
            get {
                object[] attributes = Assembly.GetExecutingAssembly()
                                                .GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                    return "";
                else 
                    return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public static string Company {
            get {
                object[] attributes = Assembly.GetExecutingAssembly()
                                                .GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                    return "";
                else 
                    return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
    }
}
