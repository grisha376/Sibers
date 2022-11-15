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
    public class TasksController : Controller
    {
        private SibersEntities db = new SibersEntities();

        // GET: Tasks
        public ActionResult Index(string currentFilter, string searchString)
        {

            ViewBag.currentFilter = searchString;

            var tasks = from s in db.Task
                       select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                tasks = tasks.Where(s => s.Name_Task.Contains(searchString));
            }

            var task = db.Task.Include(t => t.Employees).Include(t => t.Employees1).Include(t => t.Status_Task);
            return View(task.ToList());
        }

        // GET: Tasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Task.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            ViewBag.Author_task = new SelectList(db.Employees, "ID", "Name");
            ViewBag.Executor = new SelectList(db.Employees, "ID", "Name");
            ViewBag.ID_Status_task = new SelectList(db.Status_Task, "ID", "Name");
            return View();
        }

        // POST: Tasks/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name_Task,Author_task,Executor,ID_Status_task,Comment,Priority")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Task.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Author_task = new SelectList(db.Employees, "ID", "Name", task.Author_task);
            ViewBag.Executor = new SelectList(db.Employees, "ID", "Name", task.Executor);
            ViewBag.ID_Status_task = new SelectList(db.Status_Task, "ID", "Name", task.ID_Status_task);
            return View(task);
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Task.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.Author_task = new SelectList(db.Employees, "ID", "Name", task.Author_task);
            ViewBag.Executor = new SelectList(db.Employees, "ID", "Name", task.Executor);
            ViewBag.ID_Status_task = new SelectList(db.Status_Task, "ID", "Name", task.ID_Status_task);
            return View(task);
        }

        // POST: Tasks/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name_Task,Author_task,Executor,ID_Status_task,Comment,Priority")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Author_task = new SelectList(db.Employees, "ID", "Name", task.Author_task);
            ViewBag.Executor = new SelectList(db.Employees, "ID", "Name", task.Executor);
            ViewBag.ID_Status_task = new SelectList(db.Status_Task, "ID", "Name", task.ID_Status_task);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Task.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Task.Find(id);
            db.Task.Remove(task);
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
