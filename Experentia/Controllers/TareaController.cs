﻿using System;
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
using System.Globalization;

namespace Experentia.Controllers
{
    public class TareaController : ApiController
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

        // GET api/Tarea
        public IQueryable<Tarea> GetTarea()
        {
            return db.Tarea;
        }

         //get api/tarea/5
        [ResponseType(typeof(Tarea))]
        public IHttpActionResult GetTarea(int id)// ESTE METODO ES PARA VER LOS DATOS DE LA TAREA
        {
            Tarea tarea = db.Tarea.Find(id);
            if (tarea == null)
            {
                return NotFound();
            }

            return Ok(tarea);
        }
        
         //get api/tarea/5
        public HttpResponseMessage GetTareasProyecto(int id)//obtiene las tareas del proyecto
        {
            HttpResponseMessage responseOk;
            //return empresa;
            var tareas = (from c in db.Tarea
                          where c.idProyecto == id
                          select c).ToList();
            List<TareaMapeada> misTareas = new List<TareaMapeada>();
            foreach (Tarea miTarea in tareas)
            {
               var alumno = (from c in db.Alumno
                             where c.id == miTarea.idAlumno
                             select c).FirstOrDefault();
               
                
                TareaMapeada miTareaMapeada = new TareaMapeada();
                miTareaMapeada.id = Convert.ToString(miTarea.id);
                miTareaMapeada.idProyecto = Convert.ToString(miTarea.idProyecto);
                miTareaMapeada.idAlumno = Convert.ToString(miTarea.idAlumno);
                miTareaMapeada.nombre = miTarea.nombre;
                miTareaMapeada.descripcion = miTarea.descripcion;
                miTareaMapeada.estado = miTarea.estado;
                miTareaMapeada.calificacion = Convert.ToString(miTarea.calificacion);
                miTareaMapeada.fechaInicio = miTarea.fechaInicio;
                miTareaMapeada.fechaCreacion = miTarea.fechaCreacion;
                if (alumno != null) 
                { 
                    miTareaMapeada.alumno = alumno.nombre + " " + alumno.apellido; 
                }
               
                misTareas.Add(miTareaMapeada);
            }
            //string [] jsonTareas = new string[tareas.Count];
            if (tareas != null)
            {
                responseOk = Request.CreateResponse(HttpStatusCode.OK, misTareas);

                return responseOk;
            }
            else
            {
                HttpResponseMessage responseError = Request.CreateResponse(HttpStatusCode.Unauthorized, "Error");

                return responseError;
                //return null;
            }
        }

         //GET api/Tarea/5
        [ResponseType(typeof(Grupo))]
        public IHttpActionResult GetTareasCoordinador(int id)
        {
            var tareas = (from tarea in db.Tarea
                          join proyecto in db.Proyecto on tarea.idProyecto equals proyecto.id
                          join alumno in db.Alumno on tarea.idAlumno equals alumno.id
                          where proyecto.idCoordinador == id
                          select new { item = tarea, alumnoNombre = alumno.nombre, alumnoApellido = alumno.apellido });

            if (tareas == null)
            {
                return NotFound();
            }

            return Ok(tareas);
        }

        //GET api/Tarea/5
        [ResponseType(typeof(Grupo))]
        public IHttpActionResult GetTareasAlumno(int id)
        {
            var tareas = (from tarea in db.Tarea
                          join alumno in db.Alumno on tarea.idAlumno equals alumno.id
                          where alumno.id == id
                          select tarea);

            if (tareas == null)
            {
                return NotFound();
            }

            return Ok(tareas);
        }

       
        // PUT api/Tarea/5
        public IHttpActionResult PutTarea(int id, Tarea tarea)//esta harcodeada abajo 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tarea.id)
            {
                return BadRequest();
            }

            db.Entry(tarea).State = EntityState.Modified;
            
            //tarea.descripcion = "nueva descripción";//en realidad estos campos no van solo se guarda el modelo tarea
            //tarea.calificacion = 9;//
            //tarea.comentario = "soy un comentario";//

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TareaExists(id))
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

        // POST api/Tarea
        [ResponseType(typeof(Tarea))]
        public IHttpActionResult PostTarea(Tarea tarea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Tarea.Add(tarea);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tarea.id }, tarea);
        }

        // DELETE api/Tarea/5
        [ResponseType(typeof(Tarea))]
        public IHttpActionResult DeleteTarea(int id)
        {
            Tarea tarea = db.Tarea.Find(id);
            if (tarea == null)
            {
                return NotFound();
            }

            db.Tarea.Remove(tarea);
            db.SaveChanges();

            return Ok(tarea);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TareaExists(int id)
        {
            return db.Tarea.Count(e => e.id == id) > 0;
        }

    }
}