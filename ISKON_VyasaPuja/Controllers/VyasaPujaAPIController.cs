using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ISKON_VyasaPuja.Models;

namespace ISKON_VyasaPuja.Controllers
{
    public class VyasaPujaAPIController : ApiController
    {
        private VyasapujaContext db = new VyasapujaContext();

        // GET api/VyasaPujaAPI
        public IEnumerable<VyasaPuja> GetVyasaPujas(string year)
        {
            var _dbList = db.VyasaPujas.ToList();
            var _filteredList = from list in _dbList
                                where list.Year == year
                                select list;

            return _filteredList.ToList();

            //return db.VyasaPujas.ToList();
        }

        // GET api/VyasaPujaAPI/5
        [HttpGet]
        public VyasaPuja GetVyasaPuja(int id)
        {
            VyasaPuja vyasapuja = db.VyasaPujas.Find(id);
            if (vyasapuja == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return vyasapuja;
        }

        // PUT api/VyasaPujaAPI/5
        public HttpResponseMessage PutVyasaPuja(int id, VyasaPuja vyasapuja)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != vyasapuja.Vyasapujaid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(vyasapuja).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/VyasaPujaAPI
        public HttpResponseMessage PostVyasaPuja(VyasaPuja vyasapuja)
        {
            if (ModelState.IsValid)
            {
                db.VyasaPujas.Add(vyasapuja);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, vyasapuja);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = vyasapuja.Vyasapujaid }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/VyasaPujaAPI/5
        public HttpResponseMessage DeleteVyasaPuja(int id)
        {
            VyasaPuja vyasapuja = db.VyasaPujas.Find(id);
            if (vyasapuja == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.VyasaPujas.Remove(vyasapuja);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, vyasapuja);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}