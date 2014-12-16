using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Experentia.Models
{
    public class TareaMapeada
    {
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        private string estado;

        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        private string calificacion;

        public string Calificacion
        {
            get { return calificacion; }
            set { calificacion = value; }
        }

        private string fechaInicio;

        public string FechaInicio
        {
            get { return fechaInicio; }
            set { fechaInicio = value; }
        }

        private string idProyecto;

        public string IdProyecto
        {
            get { return idProyecto; }
            set { idProyecto = value; }
        }

        private string idAlumno;

        public string IdAlumno
        {
            get { return idAlumno; }
            set { idAlumno = value; }
        }


        private string alumno;

        public string  Alumno
        {
            get { return alumno; }
            set { alumno = value; }
        }
        
        
    }
}