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

        public string Sexo { get; set; }

        public string Especializacion { get; set; }

        public string MetodoIngreso { get; set; }

        public DateTime HoraIngreso { get; set; }


        public static bool Guardar(PacienteModel paciente)
        {
            Data.Instance.SalaEmergencias.Insert(paciente);
            return true;
        }

        public static PacienteModel Atender()
        {
            return Data.Instance.SalaEmergencias.Remove();
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
            else if (edadPaciente >= 0 && edadPaciente <= 5)
            {
                prioridad += 8;
            }
            else
            {
                prioridad += 0;
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

        public static bool PrioridadEntrada(PacienteModel primerP, PacienteModel segundoP)
        {
            if (primerP.HoraIngreso.Hour > segundoP.HoraIngreso.Hour)
            {
                return false;
            }
            else if (primerP.HoraIngreso.Hour < segundoP.HoraIngreso.Hour)
            {
                return true;
            }
            else
            {
                if (primerP.HoraIngreso.Minute > segundoP.HoraIngreso.Minute)
                {
                    return false;
                }
                else if (primerP.HoraIngreso.Minute < segundoP.HoraIngreso.Minute)
                {
                    return true;
                }
                else
                {
                    if (primerP.HoraIngreso.Second > segundoP.HoraIngreso.Second)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
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
