using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Goexw.Controllers
{
    public class SettingsController : Controller
    {
        public ActionResult ShowSettings()
        {
            return View();
        }

        public ActionResult ChangeLanguage()
        {
            return View();
        }

        public ActionResult ChangeCurrency()
        {
            return View();
        }

        public ActionResult ChangeSettings()
        {
            return View();
        }

        public ActionResult SaveSettings()
        {
            return View();
        }
    }
}