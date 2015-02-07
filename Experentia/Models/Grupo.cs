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
    
    public partial class Grupo
    {
        public Grupo()
        {
            this.Proyecto = new HashSet<Proyecto>();
            this.Alumno = new HashSet<Alumno>();
        }
    
        public int id { get; set; }
        public string nombre { get; set; }
        public string tecnologia { get; set; }
        public Nullable<int> idComision { get; set; }
        public Nullable<System.DateTime> fechaCreacion { get; set; }
    
        public virtual Comision Comision { get; set; }
        public virtual ICollection<Proyecto> Proyecto { get; set; }
        public virtual ICollection<Alumno> Alumno { get; set; }
    }
}
