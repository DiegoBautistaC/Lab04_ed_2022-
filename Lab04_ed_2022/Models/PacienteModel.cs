using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Lab04_ed_2022.Helpers;

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

        public DateTime HoraIngreso { get; set; }

        public static int NivelPrioridad(int id)
            {
            int prioridad = 0;
            var sexo  = Convert.ToString(Data.Instance.PacientesCola[id].sexo);
            var edad = Convert.ToInt32(Data.Instance.PacientesCola[id].Edad);
            var TIngreso = Convert.ToString(Data.Instance.PacientesCola[id].MetodoIngreso);
            var especializacion = Convert.ToString(Data.Instance.PacientesCola[id].Especializacion);
            if(sexo == "Masculino")
            {
                prioridad = prioridad + 3;
            }else if(sexo == "Femenino")
            {
                prioridad = prioridad + 5;
            }
            if(0 < edad || edad <= 5)
            {
                prioridad = prioridad + 8;
            }else if(6 <= edad || edad <= 17)
            {
                prioridad = prioridad + 5;
            }
            else if (18 <= edad || edad <= 49)
            {
                prioridad = prioridad + 3;
            }
            else if (50 <= edad || edad <= 69)
            {
                prioridad = prioridad + 8;
            }
            else if (edad >= 70)
            {
                prioridad = prioridad + 10;
            }
            if(especializacion == "Traumatologia Interna")
            {
                prioridad = prioridad + 3;
            }else if(especializacion == "Traumatologia Expuesta")
            {
                prioridad = prioridad + 8;
            }
            else if (especializacion == "Ginecologia")
            {
                prioridad = prioridad + 5;
            }
            else if (especializacion == "Cardiologia")
            {
                prioridad = prioridad + 10;
            }
            else if (especializacion == "Neumologia")
            {
                prioridad = prioridad + 8;
            }
            if(TIngreso == "Ambulancia")
            {
                prioridad = prioridad + 5;
            }else if(TIngreso == "Asistido")
            {
                prioridad = prioridad + 3;
            }
            return prioridad;
            }
    }
}
