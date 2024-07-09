using Demo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public JsonResult Upload(HttpPostedFileBase files)
        {
            var resultList = new List<UploadFilesResult>();

            string path = Server.MapPath("~/Content/uploads/");
            files.SaveAs(path + files.FileName);

            UploadFilesResult uploadFiles = new UploadFilesResult();
            uploadFiles.name = files.FileName;
            uploadFiles.size = files.ContentLength;
            uploadFiles.type = "image/jpeg";
            uploadFiles.url = "/Content/uploads/" + files.FileName;
            uploadFiles.deleteUrl = "/Home/Delete?file=" + files.FileName;
            uploadFiles.thumbnailUrl = "/Content/uploads/" + files.FileName;
            uploadFiles.deleteType = "GET";

            resultList.Add(uploadFiles);

            JsonFiles jFiles = new JsonFiles(resultList);

            return Json(jFiles);
        }

        public JsonResult Delete(string file)
        {
            System.IO.File.Delete(Path.Combine(Server.MapPath("~/Content/uploads/"), file));
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}