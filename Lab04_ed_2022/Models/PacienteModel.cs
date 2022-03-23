using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Lab04_ed_2022.Models
{
    public class PacienteModel
    {
        public int ID { get; set; }

        [MaxLength(50)]
        [MinLength(5)]
        public string Nombres { get; set; }

        [MaxLength(50)]
        [MinLength(5)]
        public string Apellidos { get; set; }

        public DateTime FechaDeNacimiento { get; set; }

        [MaxLength(9)]
        [MinLength(1)]
        public string Sexo { get; set; }

        [MaxLength(23)]
        [MinLength(5)]
        public string Especializacion { get; set; }

        [MaxLength(10)]
        [MinLength(5)]
        public string MetodoIngreso { get; set; }
    }
}
