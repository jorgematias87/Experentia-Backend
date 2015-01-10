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
    public class AlumnoController : ApiController
    {
        private ExperentiaEntities db = new ExperentiaEntities();

        [AcceptVerbs("OPTIONS")]
        public HttpResponseMessage Options()
        {
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            resp.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
            resp.Headers.Add("Access-Control-Allow-Methods", "GET,DELETE,PUT,POST");

            return resp;
        }

        // GET api/Alumno
        public IQueryable<Alumno> GetAlumno()
        {
            return db.Alumno;
        }

        // GET api/Alumno/5
        [ResponseType(typeof(Alumno))]
        public IHttpActionResult GetAlumno(int id)
        {
            Alumno alumno = db.Alumno.Find(id);
            if (alumno == null)
            {
                return NotFound();
            }

            return Ok(alumno);
        }

        // PUT api/Alumno/5
        public IHttpActionResult PutAlumno(int id, Alumno alumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != alumno.id)
            {
                return BadRequest();
            }

            db.Entry(alumno).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlumnoExists(id))
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

        // POST api/Alumno
        [ResponseType(typeof(Alumno))]
        public IHttpActionResult PostAlumno(Alumno alumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Alumno.Add(alumno);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = alumno.id }, alumno);
        }

        // DELETE api/Alumno/5
        [ResponseType(typeof(Alumno))]
        public IHttpActionResult DeleteAlumno(int id)
        {
            Alumno alumno = db.Alumno.Find(id);
            if (alumno == null)
            {
                return NotFound();
            }

            db.Alumno.Remove(alumno);
            db.SaveChanges();

            return Ok(alumno);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlumnoExists(int id)
        {
            return db.Alumno.Count(e => e.id == id) > 0;
        }
    }
}