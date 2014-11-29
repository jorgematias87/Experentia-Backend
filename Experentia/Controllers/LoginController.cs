using Experentia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace Experentia.Controllers
{
    public class LoginController : ApiController
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
        // GET api/login

        //public Object Login(Login usuario)
        //{
        //    //Empresa miEmpresa = new Empresa();
        //    //Coordinador miCoordinador = new Coordinador();
        //    //Alumno miAlumno = new Alumno();

        //    var empresa = (from c in db.Empresa
        //                 where c.email == usuario.Email
        //                 select c).FirstOrDefault();

        //    var coordinador = (from c in db.Coordinador
        //                 where c.email == usuario.Email
        //                 select c).FirstOrDefault();

        //    var alumno = (from c in db.Alumno
        //                  where c.email == usuario.Email
        //                  select c).FirstOrDefault();

        //    if (empresa != null)
        //    { return empresa; }
        //    else
        //    {
        //        if (coordinador != null)
        //        {
        //            return coordinador;
        //        }
        //        else
        //        {
        //            if (alumno != null) { return alumno; }

        //            else
        //            {
        //               return HttpContext.Current.Response.StatusCode = 401;
        //                //return null;
        //            }
        //        }
        //    }
        // }


        // GET api/login/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/login
        public HttpResponseMessage Post([FromBody]Login usuario)
        {
            HttpResponseMessage responseOk;

            var empresa = (from c in db.Empresa
                         where c.email == usuario.Email
                         select c).FirstOrDefault();

            var coordinador = (from c in db.Coordinador
                         where c.email == usuario.Email
                         select c).FirstOrDefault();
            
            var alumno = (from c in db.Alumno
                          where c.email == usuario.Email
                          select c).FirstOrDefault();

            if (empresa != null)
            { 
                //return empresa;

                var jsonEmpresa = new {email = empresa.email , id = empresa.id, razonSocial = empresa.razonSocial, user = "empresa"};
                
                responseOk = Request.CreateResponse(HttpStatusCode.OK, jsonEmpresa);

                return responseOk;
            }
            else
            {
                if (coordinador != null)
                {
                    //return coordinador;
                    var jsonCoordinador = new { email = coordinador.email , id = coordinador.id, nombre = coordinador.nombre, apellido = coordinador.apellido, user = "coordinador" };

                    responseOk = Request.CreateResponse(HttpStatusCode.OK, jsonCoordinador);

                    return responseOk;
                }
                else
                {
                    if (alumno != null) { 
                        //return alumno; 
                        var jsonAlumno = new { email = alumno.email, id = alumno.id, nombre= alumno.nombre, apellido= alumno.apellido, Grupo= alumno.Grupo, user = "alumno" };

                        responseOk = Request.CreateResponse(HttpStatusCode.OK, alumno);

                        return responseOk;
                    }

                    else
                    {
                        HttpResponseMessage responseError = Request.CreateResponse(HttpStatusCode.Unauthorized, "Error");

                        return responseError;
                        //return null;
                    }
                }
            }
        }

        // PUT api/login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/login/5
        public void Delete(int id)
        {
        }
    }
}
