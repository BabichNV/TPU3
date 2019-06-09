using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using AggregationLibrary;
using WebAggregator.Models.ViewModels;

namespace WebAggregator.Controllers
{
    public class GetDataController : Controller
    {
        // GET: GetData
        public GetDataController()
        {

        }
        public ActionResult LoadData(string message = "")
        {
            string path = Server.MapPath("");
            DirectoryInfo info = new DirectoryInfo(path);
            path = info.Parent.Parent.Parent.FullName;
            Session["1"] = path;
            ViewBag.Message = message;
            return View();
        }

        public ActionResult NewAttributes()
        {
            Functions f = new Functions(Session["1"].ToString());
            var list = f.GetHeaders(Session["0"].ToString());
            return View(list);
        }

        public ActionResult MappingData()
        {
            Functions f = new Functions(Session["1"].ToString());
            MappingDataViewModel model = new MappingDataViewModel();
            model.NewHeaders = f.GetHeaders(Session["0"].ToString());
            model.CurrentHeaders = f.GetCurrentHeaders();
            return View(model);
        }

        [HttpPost]
        public ActionResult MappingData(List<int> id)
        {
            Functions f = new Functions(Session["1"].ToString());
            f.WriteToFile(Session["0"].ToString(), DateTime.Now.Year, id);
            return Redirect("/GetData/LoadData?message=Слияние произошло успешно!");
        }
        [HttpPost]
        public ActionResult NewAttributes(List<string> met)
        {
            List<int> indexs = new List<int>();
            for (int i = 0; i < met.Count; ++i)
            {
                    indexs.Add(int.Parse(met[i]));
            }
            Functions f = new Functions(Session["1"].ToString());
            f.WriteToEmptyFile(Session["0"].ToString(), DateTime.Now.Year, indexs);
            return Redirect("/GetData/LoadData?message=Загрузка произошла успешно!");
        }
        [HttpPost]
        public ActionResult IsResultFileEmpty()
        {
            var file = Request.Files[0];
            Session["0"] = Session["1"].ToString() + @"/Files/" + file.FileName;
            if (file.FileName == "")
            {
                return Redirect("/GetData/LoadData?message=");
            }
            Functions f = new Functions(Session["1"].ToString());
            if (f.IsResultFileEmpty())
            {
                return RedirectToAction("NewAttributes");
            }
            else
            {
                return RedirectToAction("MappingData");
            }
        }
    }
}