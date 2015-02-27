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
using Newtonsoft.Json.Linq;

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

        // GET api/Grupo/5
        [ResponseType(typeof(Grupo))]
        public IHttpActionResult GetGrupo(int id)
        {
            var Grupo = (from grupo in db.Grupo
                        join materia in db.Materia on grupo.idComision equals materia.idComision
                        where grupo.id == id
                      select new { 
                          id = grupo.id, 
                          nombre = grupo.nombre, 
                          tecnologia = grupo.tecnologia,
                          fechaCreacion= grupo.fechaCreacion,
                          materia= materia.nombre
                      }).FirstOrDefault();

            if (Grupo == null)
            {
                return NotFound();
            }

            return Ok(Grupo);
        }

         //GET api/Grupo/5
        [ResponseType(typeof(Grupo))]
        public IHttpActionResult GetGrupos(int id)
        {
            var grupos = (from grupo in db.Grupo
                        join materia in db.Materia on grupo.idComision equals materia.idComision
                        join comision in db.Comision on grupo.idComision equals comision.id
                        where materia.Coordinador.FirstOrDefault().id == id
                      select new { 
                          id = grupo.id, 
                          nombre = grupo.nombre, 
                          tecnologia = grupo.tecnologia,
                          fechaCreacion= grupo.fechaCreacion,
                          materia= materia.nombre,
                          comision= comision.nombre
                      });

            if (grupos == null)
            {
                return NotFound();
            }

            return Ok(grupos);
        }

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
        public IHttpActionResult PostGrupo(JObject data)
        {
            Grupo dataGrupo =  data["grupo"].ToObject<Grupo>();
            List<Alumno> alumnos = data["alumnos"].ToObject<List<Alumno>>();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Grupo.Add(dataGrupo);
            db.SaveChanges();
            int idGrupo = dataGrupo.id;
            Grupo grupo = db.Grupo.Find(idGrupo);

            if (alumnos != null)
            {
                foreach (Alumno item in alumnos)
                {
                    Alumno alumno = db.Alumno.Find(item.id);
                    if (alumno == null)
                    {
                        return NotFound();
                    }

                    grupo.Alumno.Add(alumno);
                    db.SaveChanges();
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = dataGrupo.id }, dataGrupo);
        }

        // DELETE api/Grupo/5
        [ResponseType(typeof(Grupo))]
        public IHttpActionResult DeleteGrupo(int id)
        {
            Grupo grupo = db.Grupo.Include(r => r.Alumno)
                            .Single(r => r.id == id);
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