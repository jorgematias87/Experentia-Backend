using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Experentia.Models;

namespace Experentia.Controllers
{
    public class MateriaController : ApiController
    {
        [AcceptVerbs("OPTIONS")]
        public HttpResponseMessage Options()
        {
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            resp.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
            resp.Headers.Add("Access-Control-Allow-Methods", "GET,DELETE,PUT,POST");

            return resp;
        }

        private ExperentiaEntities db = new ExperentiaEntities();

        // GET api/Materia
        public IQueryable<Materia> GetMateria()
        {
            return db.Materia;
        }

        // GET api/Materia/5
        [ResponseType(typeof(Materia))]
        public IHttpActionResult GetMateria(int id)
        {
            Materia materia = db.Materia.Find(id);
            if (materia == null)
            {
                return NotFound();
            }

            return Ok(materia);
        }

         // GET api/Materia/5
        public HttpResponseMessage GetMaterias(int id)
        {
            HttpResponseMessage responseOk;

            List<Materia> materias = new List<Materia>();
            materias = (from materia in db.Materia
                        where materia.Coordinador.FirstOrDefault().id == id
                        select materia).ToList();

            if (materias == null)
            {
                HttpResponseMessage responseError = Request.CreateResponse(HttpStatusCode.NotFound, "Error");
                return responseError;
            }

            responseOk = Request.CreateResponse(HttpStatusCode.OK, materias);
            return responseOk;
        }

        // PUT api/Materia/5
        public IHttpActionResult PutMateria(int id, Materia materia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != materia.id)
            {
                return BadRequest();
            }

            db.Entry(materia).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MateriaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Materia
        [ResponseType(typeof(Materia))]
        public IHttpActionResult PostMateria(Materia materia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Materia.Add(materia);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = materia.id }, materia);
        }

        // DELETE api/Materia/5
        [ResponseType(typeof(Materia))]
        public IHttpActionResult DeleteMateria(int id)
        {
            Materia materia = db.Materia.Find(id);
            if (materia == null)
            {
                return NotFound();
            }

            db.Materia.Remove(materia);
            db.SaveChanges();

            return Ok(materia);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MateriaExists(int id)
        {
            return db.Materia.Count(e => e.id == id) > 0;
        }
    }
}