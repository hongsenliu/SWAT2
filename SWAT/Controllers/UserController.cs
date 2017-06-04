using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SWAT.Models;
using System.Web.SessionState;
using System.Web.Http;
//using SWAT.ViewModels;

namespace SWAT.Controllers
{
    public class UserController : Controller
    {
        private SWATEntities db = new SWATEntities();

        //
        // GET: /User/

        //public ActionResult Index()
        //{
        //    var users = db.Userids.Where(e => e.type == 0);
        //    return View(users.ToList());
        //}

        // TODO uncomment after merge
        //public ActionResult About()
        //{
        //    var data = from country in db.lkpSubnationals
        //               group country by country.lkpCountry.Name
        //                   into countryGroup
        //                   select new CountrySubnationalsGroup()
        //                   {
        //                       CountryName = countryGroup.Key,
        //                       SubNationalCount = countryGroup.Count()
        //                   };
        //    return View(data);
        //}

        // test session
        

        // GET: /User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userid user_id = db.userids.Find(id);
            Session.Add("id", id);
            int ss = (int)Session["id"];
            ViewBag.ss = ss;

            if (user_id == null)
            {
                return HttpNotFound();
            }
            return View(user_id);
        }

        //// GET: /User/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: /User/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Userid1,Username,Password,eMail,Notes,SessionVars,secondaryId,type,properties,targetName,NotifStatus,NotifCode,NotifReqCodeTime,NotifActCodeTime,PMEmailNotificationsEnabled,UseOntarioMaps,First_Name,Last_Name,Address,utmX,utmY,Joined,Interests,Workplace,SecretQuestion,SecretAnswer,AvatarUserPhotoID,LastSuggestedContentDate,FullName")] Userid user_id)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Userids.Add(user_id);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(user_id);
        //}

        //// GET: /User/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Userid user_id = db.Userids.Find(id);
        //    if (user_id == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(user_id);
        //}

        //// POST: /User/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Userid1,Username,Password,eMail,Notes,SessionVars,secondaryId,type,properties,targetName,NotifStatus,NotifCode,NotifReqCodeTime,NotifActCodeTime,PMEmailNotificationsEnabled,UseOntarioMaps,First_Name,Last_Name,Address,utmX,utmY,Joined,Interests,Workplace,SecretQuestion,SecretAnswer,AvatarUserPhotoID,LastSuggestedContentDate,FullName")] Userid user_id)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(user_id).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(user_id);
        //}

        //// GET: /User/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Userid user_id = db.Userids.Find(id);
        //    if (user_id == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(user_id);
        //}

        //// POST: /User/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Userid user_id = db.Userids.Find(id);
        //    db.Userids.Remove(user_id);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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