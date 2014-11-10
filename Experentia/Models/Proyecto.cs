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
    
    public partial class Proyecto
    {
        public Proyecto()
        {
            this.Tarea = new HashSet<Tarea>();
        }
    
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string estado { get; set; }
        public string tecnologia { get; set; }
        public Nullable<System.DateTime> fechaLimite { get; set; }
        public Nullable<int> idGrupo { get; set; }
        public int idEmpresa { get; set; }
    
        public virtual Empresa Empresa { get; set; }
        public virtual Grupo Grupo { get; set; }
        public virtual ICollection<Tarea> Tarea { get; set; }
    }
}
