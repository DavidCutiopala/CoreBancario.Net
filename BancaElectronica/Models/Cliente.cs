using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BancaElectronica.Models
{
    public class Cliente
    {


        public string Nombre { get; set; }


        //[Required(ErrorMessage = "Apellido requerido")]
        public string Apellido { get; set; }

 
        //[Required(ErrorMessage = "Cedula requerido")]
        public string Cedula { get; set; }


        //[Required(ErrorMessage = "Cedula requerido")]
        public string Direccion { get; set; }

   
        //[Required(ErrorMessage = "Telefono requerido")]
        public string Telefono { get; set; }

        public string Email { get; set; }
    }
}