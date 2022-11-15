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
    public class ProjectsController : Controller
    {
        private SibersEntities db = new SibersEntities();

        // GET: Projects
        public ActionResult Index(string sortOrder, string currentFilter, string searchString)
        {
            ViewBag.PrioritySortParm = sortOrder == "Priority" ? "Prioritet_desc" : "Priority";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";

            ViewBag.currentFilter = searchString;

            var projects = from s in db.Project
                           select s;

            if(!String.IsNullOrEmpty(searchString))
            {
                projects = projects.Where(s => s.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Date_desc":
                    projects = projects.OrderByDescending(s => s.Date_of_start);
                    break;
                case "Date":
                    projects = projects.OrderBy(s => s.Date_of_start);
                    break;
                case "Prioritet_desc":
                    projects = projects.OrderByDescending(s => s.Prority);
                    break;
                default:
                    projects = projects.OrderBy(s => s.Prority);
                    break;
                
            }
            return View(projects.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            ViewBag.ID_customer = new SelectList(db.Customer, "ID", "Name");
            ViewBag.ID_leader = new SelectList(db.Employees, "ID", "Name");
            ViewBag.ID_executor = new SelectList(db.Executor, "ID", "Name");
            return View();
        }

        // POST: Projects/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,ID_customer,ID_executor,ID_leader,Date_of_start,Date_of_end,Prority")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Project.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_customer = new SelectList(db.Customer, "ID", "Name", project.ID_customer);
            ViewBag.ID_leader = new SelectList(db.Employees, "ID", "Name", project.ID_leader);
            ViewBag.ID_executor = new SelectList(db.Executor, "ID", "Name", project.ID_executor);
            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_customer = new SelectList(db.Customer, "ID", "Name", project.ID_customer);
            ViewBag.ID_leader = new SelectList(db.Employees, "ID", "Name", project.ID_leader);
            ViewBag.ID_executor = new SelectList(db.Executor, "ID", "Name", project.ID_executor);
            return View(project);
        }

        // POST: Projects/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,ID_customer,ID_executor,ID_leader,Date_of_start,Date_of_end,Prority")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_customer = new SelectList(db.Customer, "ID", "Name", project.ID_customer);
            ViewBag.ID_leader = new SelectList(db.Employees, "ID", "Name", project.ID_leader);
            ViewBag.ID_executor = new SelectList(db.Executor, "ID", "Name", project.ID_executor);
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Project.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Project.Find(id);
            db.Project.Remove(project);
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
