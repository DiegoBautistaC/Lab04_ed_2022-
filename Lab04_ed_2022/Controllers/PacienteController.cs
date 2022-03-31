using Lab04_ed_2022.Helpers;
using Lab04_ed_2022.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab04_ed_2022.Controllers
{
    public class PacienteController : Controller
    {
        // GET: PacienteController
        public ActionResult Index()
        {
            return View(Data.Instance.SalaEmergencias);
        }


        // GET: PacienteController
        public ActionResult Atender()
        {
            return View(PacienteModel.Atender());
        }

        // GET: PacienteController/Create
        public ActionResult Create()
        {
            return View(new PacienteModel());
        }

        // POST: PacienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                if (collection["Sexo"] == "0" || collection["Especializacion"] == "0" || collection["MetodoIngreso"] == "0")
                {
                    return View();
                }
                var validacion = PacienteModel.Guardar(new PacienteModel
                {
                    ID = Data.Instance.ConteoID + 1,
                    Nombres = collection["Nombres"],
                    Apellidos = collection["Apellidos"],
                    FechaDeNacimiento = Convert.ToDateTime(collection["FechaDeNacimiento"]),
                    Sexo = collection["Sexo"],
                    Especializacion = collection["Especializacion"],
                    MetodoIngreso = collection["MetodoIngreso"],
                    HoraIngreso = DateTime.Now
                });
                if (validacion)
                {
                    Data.Instance.ConteoID++;
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
