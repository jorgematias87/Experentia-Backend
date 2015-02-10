using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Experentia.Models
{
    public class TareaMapeada
    {
        private string Id;

        public string id
        {
            get { return Id; }
            set { Id = value; }
        }

        private string Nombre;

        public string nombre
        {
            get { return Nombre; }
            set { Nombre = value; }
        }

        private string Descripcion;

        public string descripcion
        {
            get { return Descripcion; }
            set { Descripcion = value; }
        }

        private string Estado;

        public string estado
        {
            get { return Estado; }
            set { Estado = value; }
        }

        private string Calificacion;

        public string calificacion
        {
            get { return Calificacion; }
            set { Calificacion = value; }
        }

        private DateTime? FechaInicio;

        public DateTime? fechaInicio
        {
            get { return FechaInicio; }
            set { FechaInicio = value; }
        }

        private string IdProyecto;

        public string idProyecto
        {
            get { return IdProyecto; }
            set { IdProyecto = value; }
        }

        private string IdAlumno;

        public string idAlumno
        {
            get { return IdAlumno; }
            set { IdAlumno = value; }
        }

        private string Alumno;

        public string alumno
        {
            get { return Alumno; }
            set { Alumno = value; }
        }



        public DateTime? fechaCreacion { get; set; }
    }
}