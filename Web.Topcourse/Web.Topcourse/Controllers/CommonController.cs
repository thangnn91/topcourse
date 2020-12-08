using Common.Model;
using Common.Utilities.Security;
using DataAccess.Topcourse.Factory;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace Web.Topcourse.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
        public ActionResult Header()
        {
            var userID = Helper.SessionAccount.UserID;
            ViewBag.UserAvatar = "";
            if (userID > 0)
            {
                var avatarDirectory = $"{ConfigurationManager.AppSettings["MediaImageUploadDirectory"]}Avatar\\{userID}\\";
                var avatarName = Security.MD5Encrypt($"{userID}_avatar") + ".png";
                if (System.IO.File.Exists(avatarDirectory + "\\" + avatarName))
                {
                    ViewBag.UserAvatar = $"{Helper.Utils.Domain}Upload/Avatar/{userID}/{avatarName}";
                }

            }
            //Lay du lieu dropdown tim kiem
            var listSpecilityCourse = AbstractFactory.Instance().TopcourseDAO().GetListSpecilityCourse(-1, 2).OrderBy(o => o.Name).ToList();
            var listSpecilityCourseShort = AbstractFactory.Instance().TopcourseDAO().GetListSpecilityCourse(-1, 1).OrderBy(o => o.Name).ToList();
            ViewBag.ListSpecilityCourse = listSpecilityCourse ?? new List<SpecilityCourse>();
            ViewBag.ListSpecilityCourseShort = listSpecilityCourseShort ?? new List<SpecilityCourse>();
            //Lay khoa hoc yeu thich
            var dao = AbstractFactory.Instance().TopcourseDAO();
            var listCity = dao.GetLocation(0, 100000000, 0);
            ViewBag.ListCity = listCity;
            var listCountry = dao.GetLocation(1,0, -1);
            ViewBag.Locations = listCountry.Any() ? listCountry : null;
            ViewBag.CourseFavorites = AbstractFactory.Instance().TopcourseDAO().GetCourseFavorites(userID) ?? new List<Course>();
            return PartialView();
        }
        public ActionResult Footer()
        {
            return PartialView();
        }
        public ActionResult HeaderTinTuc()
        {
            var userID = Helper.SessionAccount.UserID;
            ViewBag.UserAvatar = "";
            if (userID > 0)
            {
                var avatarDirectory = $"{ConfigurationManager.AppSettings["MediaImageUploadDirectory"]}Avatar\\{userID}\\";
                var avatarName = Security.MD5Encrypt($"{userID}_avatar") + ".png";
                if (System.IO.File.Exists(avatarDirectory + "\\" + avatarName))
                {
                    ViewBag.UserAvatar = $"{Helper.Utils.Domain}Upload/Avatar/{userID}/{avatarName}";
                }

            }
            return PartialView();
        }
    }
}