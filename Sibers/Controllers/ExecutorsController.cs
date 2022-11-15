using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sibers.Models;

namespace Sibers.Controllers
{
    public class ExecutorsController : Controller
    {
        private SibersEntities db = new SibersEntities();

        // GET: Executors
        public ActionResult Index()
        {
            return View(db.Executor.ToList());
        }

        // GET: Executors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Executor executor = db.Executor.Find(id);
            if (executor == null)
            {
                return HttpNotFound();
            }
            return View(executor);
        }

        // GET: Executors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Executors/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] Executor executor)
        {
            if (ModelState.IsValid)
            {
                db.Executor.Add(executor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(executor);
        }

        // GET: Executors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Executor executor = db.Executor.Find(id);
            if (executor == null)
            {
                return HttpNotFound();
            }
            return View(executor);
        }

        // POST: Executors/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] Executor executor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(executor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(executor);
        }

        // GET: Executors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Executor executor = db.Executor.Find(id);
            if (executor == null)
            {
                return HttpNotFound();
            }
            return View(executor);
        }

        // POST: Executors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Executor executor = db.Executor.Find(id);
            db.Executor.Remove(executor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
