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
    public class GrupoController : ApiController
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

        // GET api/Grupo
        public IQueryable<Grupo> GetGrupo()
        {
            return db.Grupo;
        }

        // GET api/Grupo/5
        [ResponseType(typeof(Grupo))]
        public IHttpActionResult GetGrupo(int id)
        {
            Grupo grupo = db.Grupo.Find(id);
            if (grupo == null)
            {
                return NotFound();
            }

            return Ok(grupo);
        }


        //public IHttpActionResult AgregarAlumnosAGrupo(int id, IQueryable<Alumno> db.Alumno/*List<Alumno> alumnos*/ )//recibe el id del grupo
        //{
        //    Grupo miGrupo = db.Grupo.Find(id);
        //    if (miGrupo == null)
        //    {
        //        return NotFound();
        //    }

        //    if (alumnos != null)
        //    {
        //      foreach (Alumno mialumno in alumnos)
        //      {
        //        miGrupo.Alumno.Add(mialumno);
        //        ctx.Grupo.AddObject(miGrupo);
        //        //ctx.SaveChanges();
        //      }
        //    }


        //    return Ok(grupo);
        //}




        // PUT api/Grupo/5
        public IHttpActionResult PutGrupo(int id, Grupo grupo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != grupo.id)
            {
                return BadRequest();
            }

            db.Entry(grupo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GrupoExists(id))
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

        // POST api/Grupo
        [ResponseType(typeof(Grupo))]
        public IHttpActionResult PostGrupo(Grupo grupo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Grupo.Add(grupo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = grupo.id }, grupo);
        }

        // DELETE api/Grupo/5
        [ResponseType(typeof(Grupo))]
        public IHttpActionResult DeleteGrupo(int id)
        {
            Grupo grupo = db.Grupo.Find(id);
            if (grupo == null)
            {
                return NotFound();
            }

            db.Grupo.Remove(grupo);
            db.SaveChanges();

            return Ok(grupo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GrupoExists(int id)
        {
            return db.Grupo.Count(e => e.id == id) > 0;
        }
    }
}