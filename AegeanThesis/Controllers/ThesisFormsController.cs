using AegeanThesis.Mail;
using AegeanThesis.Models;
using Rotativa;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace AegeanThesis.Controllers
{
    public class ThesisFormsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private MailModel mailmodel;

        // GET: ThesisForms
        public ActionResult Index(string searchString)
        {
            var thesises = from m in db.Thesises
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                thesises = thesises.Where(s => s.Title.Contains(searchString));
            }

            return View(thesises);

            //return View(db.Thesises.ToList());
        }
        [HttpPost]
        public string Index(FormCollection fc, string searchString)
        {
            return "<h3> From [HttpPost]Index: " + searchString + "</h3>";
        }

        // GET: ThesisForms/Details/5
        public ActionResult Details(int? id)
        {
            var user = Helpers.GetCurrentUser(this.User);
            if (id == null || user == null)
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
            var user = Helpers.GetCurrentUser(this.User);
            ThesisForm model = null;

            if (user.Role.Equals("Professor"))
            {

                model = new ThesisForm
                {
                    Items = new SelectList(new[]
                            {
                        new SelectListItem { Value = "Structed Programming", Text = "Structed Programming" },
                        new SelectListItem { Value = "Object Oriented Programming", Text = "Object Oriented Programming" },
                    }, "Value", "Text"),
                    //This assignment is used to pass logged in user into DB
                    Supervisor = user.Name,


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
                thesisForm.Progresses = new System.Collections.Generic.List<ThesisProgress>() { new ThesisProgress { Name = "Announced", StartDate = DateTime.Now.ToShortDateString(), EndDate = DateTime.Now.AddDays(1).ToShortDateString(), Dependencies = "", ProgressPercentage = 100 } };
                db.Thesises.Add(thesisForm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(thesisForm);
        }
        /*public ActionResult GetChart(int? id)
        {
            using (ApplicationDbContext con = new ApplicationDbContext())
            {
                var t = con.Thesises.Find(id);

                return Json(t.Progresses.Select(x => new { x.ID, x.Name, x.StartDate, x.EndDate, x.ProgressPercentage, x.Dependencies }));
            }
        }*/
        // GET: ThesisForms/Edit/5
        public ActionResult Edit(int? id)
        {
            var user = Helpers.GetCurrentUser(this.User);

            if (!user.Role.Equals("Professor"))
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
            ThesisForm model = null;

            model = new ThesisForm
            {
                Items = new SelectList(new[]
                {
                        new SelectListItem { Value = "Structed Programming", Text = "Structed Programming" },
                        new SelectListItem { Value = "Object Oriented Programming", Text = "Object Oriented Programming" },
                    }, "Value", "Text"),
                //This assignment is used to pass logged in user into DB
                Supervisor = user.Name

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
            var user = Helpers.GetCurrentUser(this.User);
            ThesisForm model = null;

            model = new ThesisForm
            {
                Items = new SelectList(new[]
                {
                        new SelectListItem { Value = "Structed Programming", Text = "Structed Programming" },
                        new SelectListItem { Value = "Object Oriented Programming", Text = "Object Oriented Programming" },
                }, "Value", "Text"),
                //This assignment is used to pass logged in user into DB
                Supervisor = user.Name

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
            var user = Helpers.GetCurrentUser(this.User);

            if (id == null || user.Role != "Professor")
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
            var user = Helpers.GetCurrentUser(this.User);

            ThesisForm thesisForm = db.Thesises.Find(id);
            if (user.Role.Equals("Professor"))
            {
                return RedirectToAction("InterestedMessage");

            }
            else if (user.Role.Equals("Student"))
            {
                //This what a mail will contain
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress(thesisForm.Supervisor+"@aegean.gr")); //replace with valid value
                message.Subject = "Your email subject";
                message.From=(new MailAddress("georgemakrakis88@gmail.com"));
                message.Body = string.Format(body, "AegeanThesis", "georgemakrakis88@gmail.com", "User " + user.Email + " is interested about thesis " + thesisForm.Title);
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
        public ActionResult Print(int? id)
        {
            var user = Helpers.GetCurrentUser(this.User);

            if (id == null || user.Role != "Professor")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThesisForm thesisForm = db.Thesises.Find(id);
            if (thesisForm == null)
            {
                return HttpNotFound();
            }
            return new ActionAsPdf("Details", thesisForm);
        }

        public ActionResult MailPage(int? id)
        {
            var user = Helpers.GetCurrentUser(this.User);

            if (id == null || user.Role != "Professor")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //ThesisTitle = db.Thesises.Find(id).Title;
            mailmodel = new MailModel();
            mailmodel.ThesisId = id;
            return View("Mail", mailmodel);
        }       
        
        [HttpPost, ActionName("SendMailResult")]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> SendMailResult(MailModel mailModel,int? id, HttpPostedFileBase uploadFile)
        {
            var user = Helpers.GetCurrentUser(this.User);
            //var id=Url.RequestContext.RouteData.Values["id"];
            if (id == null || user.Role == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThesisForm thesisForm = db.Thesises.Find(id);
            if (thesisForm == null)
            {
                return HttpNotFound();
            }            
            
            //This what a mail will contain
            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(mailModel.Email)); //replace with valid value
            message.Subject = mailModel.Subject;
            message.From = (new MailAddress(user.Email));
            message.Body = string.Format(body, "AegeanThesis", user.Email, "User " + user.Name + " is wants approve for this thesis <a href =\"http://localhost:61006/ThesisForms/Details/"+id+">localhost:61006/ThesisForms/Details/</a>" + "\n" + mailModel.Notes);
            message.IsBodyHtml = true;
            if (uploadFile != null)
            {
                string fileName = Path.GetFileName(uploadFile.FileName);
                message.Attachments.Add(new Attachment(uploadFile.InputStream, fileName));
            }
            //using the Gmail service used before for user validation
            using (var smtp = new GmailEmailService())
            {
                await smtp.SendMailAsync(message);
            }

            if (user.Role == "Professor")
            {
                return View("BoardSent");
            }
            else
            {
                return View("MailProfessor");
            }
            
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
