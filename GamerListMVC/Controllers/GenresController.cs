using Business.Services.GenreService;
using DataAccess;
using DataAccess.Entities;
using GamerListMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GamerListMVC.Controllers
{
    [Authorize(Roles = "admin")]
    [RoutePrefix("genres")]
    public class GenresController : Controller
    {
        private readonly GenreService genreService;

        public GenresController()
        {
            genreService = new GenreService();
        }

        public GenresController(DbEntitiesContext context)
        {
            genreService = new GenreService(context);
        }

        // GET: Genres
        public ActionResult Index()
        {
            var allGenres = genreService.GetAll().Select(g => new GenreViewModel(g));

            return View(allGenres);
        }

        // GET: Genres/Details/5
        [Route("details/{id:int}")]
        public ActionResult Details(int? id)
        {
            if (id == null || id <= 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var genre = genreService.GetByID((int)id);
            if (genre == null)
                return HttpNotFound();
            var model = new GenreViewModel(genre);
            return View(model);
        }

        // GET: Genres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        [HttpPost]
        [Route("create")]
        public ActionResult Create(GenreViewModel genreViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var genre = new Genre()
                    {
                        Type = genreViewModel.Type
                    };

                    genreService.Add(genre);
                    genreService.Save();

                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("", "Database error!");
                    return View(genreViewModel);
                }
            }
            return View(genreViewModel);
        }

        // GET: Genres/Edit/5
        [Route("edit/{id:int}")]
        public ActionResult Edit(int? id)
        {
            if (id == null || id <= 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var genre = genreService.GetByID((int)id);
            if (genre == null)
                return HttpNotFound();
            var model = new GenreViewModel(genre);
            return View(model);
        }

        // POST: Genres/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, GenreViewModel genreViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var genre = genreService.GetByID((int)id);
                    genre.Type = genreViewModel.Type;

                    genreService.Save();

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(genreViewModel);
                }
            }
            return View(genreViewModel);
        }

        // GET: Genres/Delete/5
        [Route("delete/{id:int}")]
        public ActionResult Delete(int? id)
        {
            if (id == null || id <= 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var genre = genreService.GetByID((int)id);
            if (genre == null)
                return HttpNotFound();
            var model = new GenreViewModel(genre);
            return View(model);
        }

        // POST: Genres/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, GenreViewModel genreViewModel)
        {
            try
            {
                var genre = genreService.GetByID((int)id);

                genreService.Delete(genre);
                genreService.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Database error!");
                return View(genreViewModel);
            }
        }
    }
}
