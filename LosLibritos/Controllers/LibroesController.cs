using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LosLibritos.Models;

namespace LosLibritos.Controllers
{
    public class LibroesController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Libroes
        public ActionResult Index(bool? estado)
        {
            if (estado == true)
            {
                var consulta = db.Libro.Include(l => l.Genero);
                return View(consulta.Where(x => x.Estado == true));
            }
            else if (estado == false)
            {
                var consulta = db.Libro.Include(l => l.Genero);
                return View(consulta.Where(x => x.Estado == false));
            }
            var libroes = db.Libro.Include(l => l.Genero);
            return View(libroes.ToList());
        }
        [HttpPost]
        public ActionResult Index(string Busqueda, string opc)
        {
            //Se recibe vía post los datos del usuario y se evaluan dentro de un switch
            switch (opc)
            {
                case "Nombre":
                    //Consulta a todos los datos de la tabla libros
                    var query = from a in db.Libro select a;
                    //Evalua el campo nombre de la tabla libros y hace filtrado
                    var filtrado = query.Where(p => p.Nombre.Contains(Busqueda));
                    return View(filtrado.ToList());
                case "Codigo":
                    var query2 = from a in db.Libro select a;
                    //Evalua el campo codigo de la tabla libros y hace filtrado
                    var filtrado2 = query2.Where(p => p.CodigoLibro.Contains(Busqueda));
                    return View(filtrado2.ToList());
               
            }
            
            var libroes = db.Libro.Include(l => l.Genero);
            return View(libroes.ToList());
        }

        // GET: Libroes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Libro libro = db.Libro.Find(id);
            if (libro == null)
            {
                return HttpNotFound();
            }
            return View(libro);
        }

        // GET: Libroes/Create
        public ActionResult Create()
        {
            ViewBag.Id_Genero = new SelectList(db.Genero, "Id_Genero", "NombreGenero");
            return View();
        }

        // POST: Libroes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id_Libro,CodigoLibro,Nombre,FechaPrestamo,FechaEntrega,Estado,Id_Genero")] Libro libro)
        {
            libro.Estado = true;
            if (ModelState.IsValid)
            {
                db.Libro.Add(libro);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            ViewBag.Id_Genero = new SelectList(db.Genero, "Id_Genero", "NombreGenero", libro.Id_Genero);
            return View(libro);
        }

        // GET: Libroes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Libro libro = db.Libro.Find(id);
            if (libro == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Genero = new SelectList(db.Genero, "Id_Genero", "NombreGenero", libro.Id_Genero);
            return View(libro);
        }

        // POST: Libroes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Libro,CodigoLibro,Nombre,FechaPrestamo,FechaEntrega,Estado,Id_Genero")] Libro libro)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(libro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            ViewBag.Id_Genero = new SelectList(db.Genero, "Id_Genero", "NombreGenero", libro.Id_Genero);
            return View(libro);
        }

        // GET: Libroes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Libro libro = db.Libro.Find(id);
            if (libro == null)
            {
                return HttpNotFound();
            }
            return View(libro);
        }

        // POST: Libroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Libro libro = db.Libro.Find(id);
            db.Libro.Remove(libro);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
