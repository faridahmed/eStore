using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineApp.Controllers
{
    [Authorize]
    public class StoreTransactionController : Controller
    {
        // GET: StoreTransaction
        public ActionResult ItemReceived()
        {
            return View("ItemReceived");
        }
    }
}