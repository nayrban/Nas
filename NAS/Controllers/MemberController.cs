using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NAS.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member        
        public ActionResult Index()
        {
            return View();
        }

        [ActionName("Index")]  
        [HttpPost]
        public ActionResult MyAction(int i)
        {
            return View();
        }
    }
}