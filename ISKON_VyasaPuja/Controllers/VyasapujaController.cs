using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISKON_VyasaPuja.Models;

namespace ISKON_VyasaPuja.Controllers
{
    public class VyasapujaController : Controller
    {
        private VyasapujaContext db = new VyasapujaContext();

        //
        // GET: /Vyasapuja/

        public ActionResult Index(string year)
        {

            return View(db.VyasaPujas.ToList());
        }

        //
        // GET: /Vyasapuja/Details/5

        public ActionResult Details(int id = 0)
        {
            VyasaPuja vyasapuja = db.VyasaPujas.Find(id);
            if (vyasapuja == null)
            {
                return HttpNotFound();
            }
            return View(vyasapuja);
        }

        //
        // GET: /Vyasapuja/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Vyasapuja/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(VyasaPuja vyasapuja)
        {
            if (ModelState.IsValid)
            {
                vyasapuja.AlphabetFirstLetter = vyasapuja.FirstName.Substring(0, 1);
                DateTime dtYear = DateTime.Now;
                vyasapuja.Year = dtYear.Year.ToString();
                db.VyasaPujas.Add(vyasapuja);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vyasapuja);
        }

        //
        // GET: /Vyasapuja/Edit/5

        public ActionResult Edit(int id = 0)
        {
            VyasaPuja vyasapuja = db.VyasaPujas.Find(id);
            if (vyasapuja == null)
            {
                return HttpNotFound();
            }
            return View(vyasapuja);
        }

        //
        // POST: /Vyasapuja/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(VyasaPuja vyasapuja)
        {
            if (ModelState.IsValid)
            {
                vyasapuja.AlphabetFirstLetter = vyasapuja.FirstName.Substring(0, 1);
                db.Entry(vyasapuja).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vyasapuja);
        }

        //
        // GET: /Vyasapuja/Delete/5

        public ActionResult Delete(int id = 0)
        {
            VyasaPuja vyasapuja = db.VyasaPujas.Find(id);
            if (vyasapuja == null)
            {
                return HttpNotFound();
            }
            return View(vyasapuja);
        }

        //
        // POST: /Vyasapuja/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VyasaPuja vyasapuja = db.VyasaPujas.Find(id);
            db.VyasaPujas.Remove(vyasapuja);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}