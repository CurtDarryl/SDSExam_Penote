using System.Net;
using System.Web.Mvc;
using SDSExam_Penote.Models;

namespace SDSExam_Penote.Controllers
{
    public class RecyclableTypeController : Controller
    {
        private RecyclableTypeDAL dal = new RecyclableTypeDAL();

        public ActionResult Index()
        {
            return View(dal.GetAll());
        }

        public ActionResult Create()
        {
            var model = new RecyclableType();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public ActionResult Create(RecyclableType rt)
        {
            if (ModelState.IsValid)
            {
                dal.Insert(rt);
                return RedirectToAction("Index");
            }
            return View(rt);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var rt = dal.GetById(id.Value);
            if (rt == null) return HttpNotFound();
            return View(rt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RecyclableType rt)
        {
            if (ModelState.IsValid)
            {
                dal.Update(rt);
                return RedirectToAction("Index");
            }
            return View(rt);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var rt = dal.GetById(id.Value);
            if (rt == null) return HttpNotFound();
            return View(rt);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            dal.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
