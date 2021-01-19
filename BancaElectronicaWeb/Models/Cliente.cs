using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BancaElectronicaWeb.Models
{
    public class Cliente
    {


   
        public string idCliente { get; set; }
        public string cedula { get; set; }

        public string nombre { get; set; }
        public string apellido { get; set; }


        [StringLength(100)]
        [Required(ErrorMessage = "Nombre requerido")]
        public string usuario { get; set; }


  
        [StringLength(100)]
        [Required(ErrorMessage = "Contraseña requerido")]
        public string password { get; set; }


    }
}