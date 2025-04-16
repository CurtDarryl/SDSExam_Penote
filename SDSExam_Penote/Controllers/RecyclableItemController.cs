using System.Net;
using System.Web.Mvc;
using SDSExam_Penote.Models;

namespace SDSExam_Penote.Controllers
{
    public class RecyclableItemController : Controller
    {
        private RecyclableItemDAL dal = new RecyclableItemDAL();

        public ActionResult Index()
        {
            return View(dal.GetAll());
        }

        public ActionResult Create()
        {
            var typeDal = new RecyclableTypeDAL();
            var recyclableTypes = typeDal.GetAll();

            ViewBag.RecyclableTypes = new SelectList(recyclableTypes, "Id", "Type");

            var model = new RecyclableItem();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RecyclableItem rt)
        {
            if (ModelState.IsValid)
            {
                dal.InsertRecyclableItem(rt);
                return RedirectToAction("Index");
            }

            var typeDal = new RecyclableTypeDAL();
            var recyclableTypes = typeDal.GetAll();
            ViewBag.RecyclableTypes = new SelectList(recyclableTypes, "Id", "Type");

            return View(rt);
        }

        public ActionResult Edit(int id)
        {
            var item = dal.GetById(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            var typeDal = new RecyclableTypeDAL();
            var recyclableTypes = typeDal.GetAll();
            ViewBag.RecyclableTypes = new SelectList(recyclableTypes, "Id", "Type", item.RecyclableTypeId);

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RecyclableItem item)
        {
            if (ModelState.IsValid)
            {
                dal.UpdateRecyclableItem(item);
                return RedirectToAction("Index");
            }

            var typeDal = new RecyclableTypeDAL();
            var recyclableTypes = typeDal.GetAll();
            ViewBag.RecyclableTypes = new SelectList(recyclableTypes, "Id", "Type", item.RecyclableTypeId);

            return View(item);
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
