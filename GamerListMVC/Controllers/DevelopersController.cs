using Business.Services.DeveloperService;
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
    [RoutePrefix("developers")]
    public class DevelopersController : Controller
    {
        private readonly DeveloperService developerService;

        public DevelopersController()
        {
            developerService = new DeveloperService();
        }

        public DevelopersController(DbEntitiesContext context)
        {
            developerService = new DeveloperService(context);
        }

        // GET: Developers
        public ActionResult Index()
        {
            var allDevelopers = developerService.GetAll().Select(d => new DeveloperViewModel(d)).ToList();
            if (allDevelopers == null)
                return View("Index");

            return View(allDevelopers);
        }

        // GET: Developers/Details/5
        [Route("details/{id:int}")]
        public ActionResult Details(int? id)
        {
            if (id == null || id <= 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var developer = developerService.GetByID((int)id);
            if (developer == null)
                return HttpNotFound();
            var model = new DeveloperViewModel(developer);

            return View(model);
        }

        // GET: Developers/Create
        [Route("create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Developers/Create
        [HttpPost]
        public ActionResult Create(DeveloperViewModel developerViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var developer = new Developer()
                    {
                        Name = developerViewModel.Name
                    };

                    developerService.Add(developer);
                    developerService.Save();

                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("", "Database error!");
                    return View(developerViewModel);
                }
            }
            return View(developerViewModel);
        }

        // GET: Developers/Edit/5
        [Route("edit/{id:int}")]
        public ActionResult Edit(int? id)
        {
            if (id == null || id <= 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var developer = developerService.GetByID((int)id);
            if (developer == null)
                return HttpNotFound();
            var model = new DeveloperViewModel(developer);

            return View(model);
        }

        // POST: Developers/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, DeveloperViewModel developerViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var developer = developerService.GetByID((int)id);
                    developer.Name = developerViewModel.Name;

                    developerService.Save();

                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("", "Database error!");
                    return View(developerViewModel);
                }
            }
            return View(developerViewModel);
        }

        // GET: Developers/Delete/5
        [Route("delete/{id:int}")]
        public ActionResult Delete(int? id)
        {
            if (id == null || id <= 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var developer = developerService.GetByID((int)id);
            if (developer == null)
                return HttpNotFound();
            var model = new DeveloperViewModel(developer);
            return View();
        }

        // POST: Developers/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, DeveloperViewModel  developerViewModel)
        {
            try
            {
                var developer = developerService.GetByID((int)id);

                developerService.Delete(developer);
                developerService.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Database error!");
                return View(developerViewModel);
            }
        }
    }
}
