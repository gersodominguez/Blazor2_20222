using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Modelos
{
   
    public  class Usuario
    {
        [Required(ErrorMessage = "El codigo es obligatorio")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string  Nombre { get; set; }

        [Required(ErrorMessage = "La clave  es obligatorio")]
        public  string Clave { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio")]
        public String Rol { get; set; }

        public bool EstaActivo { get; set; }
    }
}
