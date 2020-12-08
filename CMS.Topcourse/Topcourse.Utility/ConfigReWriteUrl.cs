using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Topcourse.Utility
{
    public class ConfigReWriteUrl
    {
        public string routeName { get; set; }
        public string routeURL { get; set; }
        public string controller { get; set; }
        public string action { get; set; }

        private static ConfigReWriteUrl _instance = null;
        public static ConfigReWriteUrl Instance()
        {
            if (_instance == null)
            {
                _instance = new ConfigReWriteUrl();
                string filePath = System.Web.HttpContext.Current.Server.MapPath("~/rewrite.xml");
                _instance.LoadXml(filePath);
            }
            return _instance;
        }

        public static ConfigReWriteUrl Instance(string filePath)
        {
            if (_instance == null)
            {
                _instance = new ConfigReWriteUrl();
                if (string.IsNullOrEmpty(filePath))
                    filePath = System.Web.HttpContext.Current.Server.MapPath("~/rewrite.xml");
                _instance.LoadXml(filePath);
            }
            return _instance;
        }

        public void ReInit()
        {
            _instance = new ConfigReWriteUrl();
            string filePath = System.Web.HttpContext.Current.Server.MapPath("~/rewrite.xml");
            _instance.LoadXml(filePath);
        }

        private List<ConfigReWriteUrl> _listReWrite = new List<ConfigReWriteUrl>();
        public List<ConfigReWriteUrl> ListReWrite
        {
            get
            {
                return _listReWrite;
            }
        }

        private void LoadXml(string filePath)
        {
            try
            {
                XElement xmlFile = XElement.Load(filePath);
                _listReWrite = (from item in xmlFile.Elements("rewrite")
                                select new ConfigReWriteUrl() { routeName = item.Attribute("routeName").Value, routeURL = item.Attribute("routeURL").Value, controller = item.Attribute("controller").Value, action = item.Attribute("action").Value }).ToList<ConfigReWriteUrl>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
