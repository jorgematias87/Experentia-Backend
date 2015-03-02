//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Experentia.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tarea
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string estado { get; set; }
        public Nullable<int> calificacion { get; set; }
        public string comentario { get; set; }
        public Nullable<System.DateTime> fechaInicio { get; set; }
        public int idProyecto { get; set; }
        public Nullable<int> idAlumno { get; set; }
        public Nullable<System.DateTime> fechaCreacion { get; set; }
        public Nullable<System.DateTime> fechaFin { get; set; }
    
        public virtual Alumno Alumno { get; set; }
        public virtual Proyecto Proyecto { get; set; }
    }
}
