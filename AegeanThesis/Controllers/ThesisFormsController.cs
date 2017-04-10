using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AegeanThesis.Models;

namespace AegeanThesis.Controllers
{
    public class ThesisFormsController : Controller
    {
        private ThesisFormBContext db = new ThesisFormBContext();

        // GET: ThesisForms
        public ActionResult Index()
        {
            return View(db.Thesises.ToList());
        }

        // GET: ThesisForms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThesisForm thesisForm = db.Thesises.Find(id);
            if (thesisForm == null)
            {
                return HttpNotFound();
            }
            return View(thesisForm);
        }

        // GET: ThesisForms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ThesisForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Supervisor,NumStudents,Purpose,Description,PrereqKnowledge,StudentInfo,AnnouncDate,AdoptionDate,FinishDate,Grade,Assigned")] ThesisForm thesisForm)
        {
            if (ModelState.IsValid)
            {
                db.Thesises.Add(thesisForm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(thesisForm);
        }

        // GET: ThesisForms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThesisForm thesisForm = db.Thesises.Find(id);
            if (thesisForm == null)
            {
                return HttpNotFound();
            }
            return View(thesisForm);
        }

        // POST: ThesisForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Supervisor,NumStudents,Purpose,Description,PrereqKnowledge,StudentInfo,AnnouncDate,AdoptionDate,FinishDate,Grade,Assigned")] ThesisForm thesisForm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thesisForm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(thesisForm);
        }

        // GET: ThesisForms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThesisForm thesisForm = db.Thesises.Find(id);
            if (thesisForm == null)
            {
                return HttpNotFound();
            }
            return View(thesisForm);
        }

        // POST: ThesisForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThesisForm thesisForm = db.Thesises.Find(id);
            db.Thesises.Remove(thesisForm);
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
