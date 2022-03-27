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


        public static bool Guardar(PacienteModel paciente)
        {
            Data.Instance.SalaConsultas.Insert(paciente);
            return true;
        }

        public static int Prioridad(PacienteModel paciente)
        {
            int prioridad = 0;
            switch (paciente.Sexo)
            {
                case "Masculino":
                    prioridad += 3;
                    break;
                case "Femenino":
                    prioridad += 5;
                    break;
                default:
                    break;
            }
            int edadPaciente = Edad(paciente.FechaDeNacimiento);
            if (edadPaciente >= 70)
            {
                prioridad += 10;
            }
            else if (edadPaciente >= 50 && edadPaciente <= 69)
            {
                prioridad += 8;
            }
            else if (edadPaciente >= 18 && edadPaciente <= 49)
            {
                prioridad += 3;
            }
            else if (edadPaciente >= 6 && edadPaciente <= 17)
            {
                prioridad += 5;
            }
            else
            {
                prioridad += 8;
            }
            switch (paciente.Especializacion)
            {
                case "Traumatología (interna)":
                    prioridad += 3;
                    break;
                case "Traumatología (expuesta)":
                    prioridad += 8;
                    break;
                case "Ginecología":
                    prioridad += 5;
                    break;
                case "Cardiología":
                    prioridad += 10;
                    break;
                case "Neumología":
                    prioridad += 8;
                    break;
                default:
                    break;
            }
            switch (paciente.MetodoIngreso)
            {
                case "Ambulancia":
                    prioridad += 5;
                    break;
                case "Asistido":
                    prioridad += 3;
                    break;
                default:
                    break;
            }
            return prioridad;
        }

        static int Edad(DateTime fechaNacimiento)
        {
            if (DateTime.Now.Month >= fechaNacimiento.Month)
            {
                if (DateTime.Now.Day >= fechaNacimiento.Day)
                {
                    return DateTime.Now.Year - fechaNacimiento.Year;
                }
            }
            return DateTime.Now.Year - fechaNacimiento.Year - 1;
        }
    }
}
