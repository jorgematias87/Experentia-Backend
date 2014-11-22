using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Experentia.Models
{
    public class Login
    {

        private string email;

        public string Email
        {
            get { return  email; }
            set {  email = value; }
        }

        private string contrasenia;

        public string Contrasenia
        {
            get { return contrasenia; }
            set { contrasenia = value; }
        }
        
        
    
    }
}