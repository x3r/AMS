using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMS.Controllers
{
    public class UploadController : Controller
    {
        public ActionResult Upload(HttpPostedFileBase file)
        {
            var filename = Path.GetFileName(file.FileName);
            if (filename != null)
            {
                var path = Path.Combine(Server.MapPath("~/Upload"), filename);
                file.SaveAs(path);
            }
            return Json(filename, JsonRequestBehavior.AllowGet);
        }

    }
}
