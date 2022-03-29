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
            return View(Data.Instance.SalaConsultas);
        }

        // GET: PacienteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
                    ID = Convert.ToInt32(collection["ID"]),
                    Nombres = collection["Nombres"],
                    Apellidos = collection["Apellidos"],
                    FechaDeNacimiento = Convert.ToDateTime(collection["FechaDeNacimiento"]),
                    Sexo= collection["Sexo"],
                    Especializacion = collection["Especializacion"],
                    MetodoIngreso = collection["MetodoIngreso"],
                    HoraIngreso = Convert.ToDateTime(collection["HoraIngreso"])
                });
                if (validacion)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: PacienteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PacienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PacienteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PacienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
