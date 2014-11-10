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
    public class ProyectoController : ApiController
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

        // GET api/Proyecto
        public IQueryable<Proyecto> GetProyecto()
        {
            return db.Proyecto;
        }

        // GET api/Proyecto/5
        [ResponseType(typeof(Proyecto))]
        public IHttpActionResult GetProyecto(int id)
        {
            Proyecto proyecto = db.Proyecto.Find(id);
            if (proyecto == null)
            {
                return NotFound();
            }

            return Ok(proyecto);
        }

        // PUT api/Proyecto/5
        public IHttpActionResult PutProyecto(int id, Proyecto proyecto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != proyecto.id)
            {
                return BadRequest();
            }

            db.Entry(proyecto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProyectoExists(id))
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

        // POST api/Proyecto
        [ResponseType(typeof(Proyecto))]
        public IHttpActionResult PostProyecto(Proyecto proyecto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Proyecto.Add(proyecto);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = proyecto.id }, proyecto);
        }

        // DELETE api/Proyecto/5
        [ResponseType(typeof(Proyecto))]
        public IHttpActionResult DeleteProyecto(int id)
        {
            Proyecto proyecto = db.Proyecto.Find(id);
            if (proyecto == null)
            {
                return NotFound();
            }

            db.Proyecto.Remove(proyecto);
            db.SaveChanges();

            return Ok(proyecto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProyectoExists(int id)
        {
            return db.Proyecto.Count(e => e.id == id) > 0;
        }
    }
}