using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcControls.Models;

namespace MvcControls.Controllers
{
    public class ListViewController : Controller
    {
        // GET: ListView
        public ActionResult Index()
        {
            return View(new ListViewTestModel());
        }

        public ActionResult Test(ListViewTestModel model)
        {

            if (model.DieChicks != null)
            {
                model.SelectionResult = "Chicks: " +
                                        string.Join(",", model.DieChicks.Where(item => item.IsSelected).Select(item => item.Caption));
            }

            return View("Index", model);
        }
    }
}