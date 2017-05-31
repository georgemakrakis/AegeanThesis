using AegeanThesis.Mail;
using AegeanThesis.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace AegeanThesis.Controllers
{
    public class ThesisFormsController : Controller
    {
        private ThesisFormBContext db = new ThesisFormBContext();

        private ThesisForm model = null;

        // GET: ThesisForms
        public ActionResult Index()
        {
            return View(db.Thesises.ToList());
        }

        // GET: ThesisForms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null || Startup.curr_user=="")
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
            if (Startup.curr_role.Equals("Professor"))
            {

                model = new ThesisForm
                {
                    Items = new SelectList(new[]
                    {
                        new SelectListItem { Value = "Structed Programming", Text = "Structed Programming" },
                        new SelectListItem { Value = "Object Oriented Programming", Text = "Object Oriented Programming" },
                    }, "Value", "Text"),
                    //This assignment is used to pass logged in user into DB
                    Supervisor = Startup.curr_user


                };

                return View(model);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
        }

        // POST: ThesisForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Supervisor,NumStudents,Purpose,Description,LessonsList,PrereqLessons,PrereqKnowledge,StudentInfo,AnnouncDate,AdoptionDate,FinishDate,Grade,Assigned,Approved,ReadyPres")] ThesisForm thesisForm)
        {
            if (ModelState.IsValid)
            {

                thesisForm.PrereqLessons = string.Join(",", thesisForm.LessonsList);
                db.Thesises.Add(thesisForm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(thesisForm);
        }

        // GET: ThesisForms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!Startup.curr_role.Equals("Professor"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThesisForm thesisForm = db.Thesises.Find(id);
            if (thesisForm == null)
            {
                return HttpNotFound();
            }
            model = new ThesisForm
            {
                Items = new SelectList(new[]
                {
                        new SelectListItem { Value = "Structed Programming", Text = "Structed Programming" },
                        new SelectListItem { Value = "Object Oriented Programming", Text = "Object Oriented Programming" },
                    }, "Value", "Text"),
                //This assignment is used to pass logged in user into DB
                Supervisor = Startup.curr_user

            };
            return View(model);
        }

        // POST: ThesisForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Supervisor,NumStudents,Purpose,Description,LessonsList,PrereqLessons,PrereqKnowledge,StudentInfo,AnnouncDate,AdoptionDate,FinishDate,Grade,Assigned,Approved,ReadyPres")] ThesisForm thesisForm)
        {
            model = new ThesisForm
            {
                Items = new SelectList(new[]
                {
                        new SelectListItem { Value = "Structed Programming", Text = "Structed Programming" },
                        new SelectListItem { Value = "Object Oriented Programming", Text = "Object Oriented Programming" },
                }, "Value", "Text"),
                //This assignment is used to pass logged in user into DB
                Supervisor = Startup.curr_user

            };
            if (ModelState.IsValid)
            {
                thesisForm.PrereqLessons = string.Join(",", thesisForm.LessonsList);
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

        // GET: ThesisForms/Interested
        public async System.Threading.Tasks.Task<ActionResult> Interested(int? id)
        {
            ThesisForm thesisForm = db.Thesises.Find(id);
            if (Startup.curr_role.Equals("Professor"))
            {
                return RedirectToAction("InterestedMessage");

            }
            else if (Startup.curr_role.Equals("Student"))
            {
                //This what a mail will contain
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress(thesisForm.Supervisor+"@aegean.gr")); //replace with valid value
                message.Subject = "Your email subject";
                message.From=(new MailAddress("georgemakrakis88@gmail.com"));
                message.Body = string.Format(body, "AegeanThesis", "georgemakrakis88@gmail.com", "User " + Startup.curr_mail + " is interested about thesis " + thesisForm.Title);
                message.IsBodyHtml = true;
                //using the Gmail service used before for user validation
                using (var smtp = new GmailEmailService())
                {                               
                    await smtp.SendMailAsync(message);                    
                }
                return RedirectToAction("InterestedSent");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (thesisForm == null)
            {
                return HttpNotFound();
            }
            return View();
        }
        public ActionResult InterestedSent()
        {
            return View("InterestedSent");
        }
        public ActionResult InterestedMessage()
        {
            return View("InterestedMessage");
        }
        [HttpPost, ActionName("Interested")]
        [ValidateAntiForgeryToken]
        public ActionResult Interested(int id)
        {            
            return RedirectToAction("ThesisForm");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Chat()
        {
            return View();
        }
    }
}
