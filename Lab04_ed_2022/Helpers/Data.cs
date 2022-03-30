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

        public int ConteoID = 0;

        public QueuePriority<PacienteModel> SalaEmergencias = new QueuePriority<PacienteModel>(PacienteModel.Prioridad, (PriorityNode<PacienteModel> v1, PriorityNode<PacienteModel> v2) => v1.Priority > v2.Priority, PacienteModel.PrioridadEntrada);
    }
}
