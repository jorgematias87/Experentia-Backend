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
    
    public partial class Alumno
    {
        public Alumno()
        {
            this.Tarea = new HashSet<Tarea>();
            this.Grupo = new HashSet<Grupo>();
        }
    
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public string contrasenia { get; set; }
        public bool estado { get; set; }
    
        public virtual ICollection<Tarea> Tarea { get; set; }
        public virtual ICollection<Grupo> Grupo { get; set; }
    }
}
