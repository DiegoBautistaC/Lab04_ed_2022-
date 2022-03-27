using System;
using ClassLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab04_ed_2022.Models;

namespace Lab04_ed_2022.Helpers
{
    public class Data
    {
        private static Data _instance = null;

        public static Data Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Data();
                }
                return _instance;
            }
        }

        public QueuePriority<int> Prueba = new QueuePriority<int>((int v) => 1, (int v1, int v2) => v1 > v2);

        public QueuePriority<PacienteModel> SalaConsultas = new QueuePriority<PacienteModel>((PacienteModel v)=> 2, (PacienteModel v1, PacienteModel v2) => v1.ID > v2.ID);
    }
}
