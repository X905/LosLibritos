using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LosLibritos.Models;

namespace LosLibritos.Controllers
{
    public class HomeController : Controller
    {
        Contexto db = new Contexto();
        // GET: Principal
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Disponible()
        {
            var query = from a in db.Libro select a;
            var filtrado = query.Where(p => p.Estado == true);
            return View(filtrado.ToList());
        }
        public ActionResult NoDisponible()
        {
            var query = from a in db.Libro select a;
            var filtrado = query.Where(p => p.Estado == false);
            return View(filtrado.ToList());
        }
        [HttpPost]
        public ActionResult Prestar(int? id, DateTime fechae)
        {
            var query = (db.Libro.Where(x => x.Id_Libro == id)).FirstOrDefault();
            query.Estado = false;
            query.FechaEntrega = fechae;
            query.FechaPrestamo = DateTime.Now;
            db.Entry(query).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Entregar(int? id)
        {
            var query = (db.Libro.Where(x => x.Id_Libro == id)).FirstOrDefault();
            query.Estado = true;
            query.FechaPrestamo = null;
            query.FechaEntrega = null;
            db.Entry(query).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Editar(int? id, string nombre, string codigo)
        {
            var libro = db.Libro.Where(x => x.Id_Libro == id).FirstOrDefault();

            libro.Nombre = nombre;
            libro.CodigoLibro = codigo;

            db.Entry(libro).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}




