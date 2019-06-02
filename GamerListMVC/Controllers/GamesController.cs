using Business.Services.DeveloperService;
using Business.Services.GameRatingService;
using Business.Services.GameService;
using Business.Services.GameStatusService;
using Business.Services.GenreService;
using Business.Services.UserGameRatingService;
using Business.Services.UserGameStatusService;
using DataAccess;
using DataAccess.Entities;
using GamerListMVC.Helpers;
using GamerListMVC.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GamerListMVC.Controllers
{
    [RoutePrefix("games")]
    [Authorize(Roles = "admin")]
    public class GamesController : Controller
    {
        private readonly GameService gameService;
        private readonly DeveloperService developerService;
        private readonly GenreService genreService;

        public GamesController()
        {
            gameService = new GameService();
            developerService = new DeveloperService();
            genreService = new GenreService();
        }

        public GamesController(DbEntitiesContext context)
        {
            gameService = new GameService(context);
            developerService = new DeveloperService(context);
            genreService = new GenreService(context);
        }

        // GET: Games
        [AllowAnonymous]
        [Route("")]
        public ActionResult Index(string searchString, string gameGenre, int? page)
        {
            var genres = genreService.GetAll().OrderBy(g => g.Type).Select(g => g.Type).ToList();

            ViewBag.genres = new SelectList(genres);
            ViewBag.searchString = searchString;
            ViewBag.gameGenre = gameGenre;

            var games = gameService.GetAll().Select(g => new GameViewModel(g)).ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                games = games.Where(s => s.Title.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }

            if (!string.IsNullOrEmpty(gameGenre))
            {
                games = games.Where(g => g.Genre.Type.Contains(gameGenre)).ToList();
            }

            if (games.Count <= 0)
                TempData["Message"] = "No movies found!";
            ViewBag.Message = TempData["Message"];

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(games.ToPagedList(pageNumber, pageSize));
        }        

        // GET: Games/Details/5
        [AllowAnonymous]
        [Route("details/{id:int}")]
        public ActionResult Details(int? id)
        {
            if (id == null || id <= 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var game = gameService.GetByID((int)id);
            if (game == null)
                return HttpNotFound();

            var model = new GameViewModel(game);
            using (var ratingService = new GameRatingService())
            {
                UserGameRating userGameRating;
                using (var userRatingService = new UserGameRatingService())
                {
                    userGameRating = userRatingService.GetExistingUserGameRating(User.Identity.GetUserId(), (int)id);
                    var allUserGameRatings = userRatingService.GetAllUserGameRatingsByGameId((int)id); //Get all user game ratings for a single game
                    if (allUserGameRatings.Count > 0)//calculate game rating
                    {
                        var rating = 0;
                        foreach (var el in allUserGameRatings)
                        {
                            rating += el.GameRating.Rating;
                        }
                        double result = rating / (double)allUserGameRatings.Count;
                        ViewBag.Score = result.ToString("n2");
                    }
                    else
                    {
                        ViewBag.Score = "N/A";
                    }
                }

                var ratings = ratingService.GetAll().Select(x => new SelectListItem { Text = x.Rating.ToString(), Value = x.Id.ToString(), Selected = userGameRating == null ? false : userGameRating.GameRatingId == x.Id }).ToList();
                model.Ratings = ratings;
                model.SelectedRating = ratings.Where(r => r.Selected != false).Select(r => Convert.ToInt32(r.Value)).FirstOrDefault();
                //model.SelectedRating = ratings.Where(r => r.Selected != false).Select(r => r.Value = "Rate game").FirstOrDefault();
            }
            using (var statusService = new GameStatusService())
            {
                UserGameStatus userGameStatus;
                using (var userStatusService = new UserGameStatusService())
                {//load existing user game status for a game for it to be selected
                    userGameStatus = userStatusService.GetExistingUserGameStatus(User.Identity.GetUserId(), (int)id);
                }
                var statuses = statusService.GetAll().Select(s => new SelectListItem { Text = s.Status, Value = s.Id.ToString(), Selected = userGameStatus == null ? false : userGameStatus.GameStatusId == s.Id }).ToList();
                model.Statuses = statuses;
                model.SelectedStatus = statuses.Where(s => s.Selected != false).Select(s => s.Value.ToString()).FirstOrDefault();
            }
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("details/{id:int}")]
        public ActionResult Details(int? id, GameViewModel gameViewModel)
        {
            string userId = User.Identity.GetUserId();
            if (userId == null)
            {
                return View("Error");
            }
            if (id == null || id <= 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (gameViewModel.SelectedStatus != null)
            {
                int gameStatusIdToStore = int.Parse(gameViewModel.SelectedStatus);
                //using (var gameStatusService = new GameStatusService())
                //{
                //    gameStatusIdToStore = gameStatusService.GetStatusIdBySelectedStatus(gameViewModel.SelectedStatus);
                //}
                using (var userGameStatusService = new UserGameStatusService())
                {
                    var dbUserGameStatus = userGameStatusService.GetExistingUserGameStatus(userId, (int)id);
                    if (dbUserGameStatus == null)
                    {
                        var userGameStatus = new UserGameStatus
                        {
                            UserId = userId,
                            GameId = (int)id,
                            GameStatusId = gameStatusIdToStore
                        };
                        userGameStatusService.Add(userGameStatus);
                        userGameStatusService.Save();
                    }
                    else
                    {
                        var dbUGSToStore = userGameStatusService.GetByID(dbUserGameStatus.Id);
                        dbUGSToStore.GameStatusId = gameStatusIdToStore;
                        userGameStatusService.Save();
                    }
                }
            }

            if (gameViewModel.SelectedRating == 0)
            {
                using (var userGameRatingService = new UserGameRatingService())
                {
                    var existingUserGameRating = userGameRatingService.GetExistingUserGameRating(userId, (int)id);
                    if (existingUserGameRating != null)
                    {
                        var dbUserGameRating = userGameRatingService.GetByID(existingUserGameRating.Id);
                        userGameRatingService.Delete(dbUserGameRating);
                        userGameRatingService.Save();
                    }
                }
            }

            if (gameViewModel.SelectedRating != 0)
            {
                int gameRatingIdToStore;
                using (var gameRatingService = new GameRatingService())
                {
                    gameRatingIdToStore = gameRatingService.GetGameRatingByRating((int)gameViewModel.SelectedRating);
                }
                using (var userGameRatingService = new UserGameRatingService())
                {
                    var dbUserGameRating = userGameRatingService.GetExistingUserGameRating(userId, (int)id);
                    if (dbUserGameRating == null)
                    {
                        var userGameRating = new UserGameRating
                        {
                            UserId = userId,
                            GameId = (int)id,
                            GameRatingId = gameRatingIdToStore
                        };
                        userGameRatingService.Add(userGameRating);
                        userGameRatingService.Save();
                    }
                    else
                    {
                        var dbUGRToStore = userGameRatingService.GetByID(dbUserGameRating.Id);
                        dbUGRToStore.GameRatingId = gameRatingIdToStore;
                        userGameRatingService.Save();
                    }
                }
            }
            return RedirectToAction("Details", "Games");
        }

        // GET: Games/Create
        [Route("create")]
        public ActionResult Create()
        {
            var developers = developerService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            var genres = genreService.GetAll().Select(x => new SelectListItem { Text = x.Type, Value = x.Id.ToString() }).ToList();

            var model = new GameViewModel { Developers = developers, Genres = genres };
            return View(model);
        }

        // POST: Games/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(GameViewModel gameViewModel, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (gameViewModel.CoverImageFile != null && gameViewModel.CoverImageFile.ContentLength > 0)
                    {
                        try
                        {
                            string FileName = Path.GetFileNameWithoutExtension(gameViewModel.CoverImageFile.FileName);

                            //To Get File Extension  
                            string FileExtension = Path.GetExtension(gameViewModel.CoverImageFile.FileName);

                            //Add Current Date To Attached File Name  
                            FileName = FileName.Trim() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + FileExtension;

                            //Get Upload path from Web.Config file AppSettings.  
                            //string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();
                            string path = Path.Combine(Server.MapPath("~/Images/Game_Cover_Images/"),
                                          Path.GetFileName(FileName));

                            //To copy and save file into server.
                            gameViewModel.CoverImagePath = FileName;
                            gameViewModel.CoverImageFile.SaveAs(path);
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Message = "ERROR:" + ex.Message.ToString();
                            ModelState.AddModelError("CoverImageFile", "Could not save file!");
                            return View(gameViewModel);
                        }
                    }
                    else
                    {
                        gameViewModel.CoverImagePath = "noimage.jpg";
                    }

                    var game = new Game()
                    {
                        Title = gameViewModel.Title,
                        DeveloperId = gameViewModel.DeveloperId,
                        GenreId = gameViewModel.GenreId,
                        Description = gameViewModel.Description,
                        ReleaseDate = gameViewModel.ReleaseDate,
                        CoverImagePath = gameViewModel.CoverImagePath
                    };

                    gameService.Add(game);
                    gameService.Save();

                    TempData["Message"] = "Movie created successfuly!";
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Database error!");
                return View(gameViewModel);
            }

            return View(gameViewModel);
        }

        // GET: Games/Edit/5
        [Route("edit/{id:int}")]
        public ActionResult Edit(int? id)
        {
            if (id == null || id <= 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var game = gameService.GetByID((int)id);
            var model = new GameViewModel();

            if (game == null)
                return HttpNotFound();

            model = new GameViewModel(game);

            var developers = developerService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            var genres = genreService.GetAll().Select(x => new SelectListItem { Text = x.Type, Value = x.Id.ToString() }).ToList();
            model.Developers = developers;
            model.Genres = genres;

            return View(model);
        }

        // POST: Games/Edit/5
        [HttpPost]
        [Route("edit/{id:int}")]
        public ActionResult Edit(int? id, GameViewModel gameViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (gameViewModel.CoverImageFile != null && gameViewModel.CoverImageFile.ContentLength > 0)
                    {
                        try
                        {
                            string FileName = Path.GetFileNameWithoutExtension(gameViewModel.CoverImageFile.FileName);

                            string FileExtension = Path.GetExtension(gameViewModel.CoverImageFile.FileName);

                            FileName = FileName.Trim() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + FileExtension;

                            string path = Path.Combine(Server.MapPath("~/Images/Game_Cover_Images/"),
                                          Path.GetFileName(FileName));

                            gameViewModel.CoverImagePath = FileName;
                            gameViewModel.CoverImageFile.SaveAs(path);
                        }
                        catch
                        {
                            ModelState.AddModelError("CoverImageFile", "Could not save file!");
                            return View(gameViewModel);
                        }
                    }
                    else
                    {
                        gameViewModel.CoverImagePath = "noimage.jpg";
                    }
                    try
                    {
                        var game = gameService.GetByID((int)id);
                        game.Title = gameViewModel.Title;
                        game.DeveloperId = gameViewModel.DeveloperId;
                        game.GenreId = gameViewModel.GenreId;
                        game.Description = gameViewModel.Description;
                        game.ReleaseDate = gameViewModel.ReleaseDate;
                        if (gameViewModel.CoverImageFile != null && gameViewModel.CoverImageFile.ContentLength > 0)
                        {
                            if (game.CoverImagePath != "noimage.jpg")
                            {
                                string fullFilePath = Request.MapPath("~/Images/Game_Cover_Images/" + game.CoverImagePath);
                                System.IO.File.Delete(fullFilePath);
                            }
                            game.CoverImagePath = gameViewModel.CoverImagePath;
                        }

                        //gameService.Update(game);
                        gameService.Save();

                        TempData["Message"] = "Movie updated successfuly!";
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Database error. Could not update game.");
                        return View(gameViewModel);
                    }
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Database error!");
                return View(gameViewModel);
            }

            return View(gameViewModel);
        }

        // GET: Games/Delete/5
        [Route("delete/{id:int}")]
        public ActionResult Delete(int? id)
        {
            if (id == null || id <= 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var game = gameService.GetByID((int)id);
            if (game == null)
                return HttpNotFound();
            var model = new GameViewModel(game);

            return View(model);
        }

        // POST: Games/Delete/5
        [HttpPost]
        [Route("delete/{id:int}")]
        public ActionResult Delete(int? id, GameViewModel gameViewModel)
        {
            try
            {
                var game = gameService.GetByID((int)id);

                if (game.CoverImagePath != "noimage.jpg")
                {
                    string fullFilePath = Request.MapPath("~/Images/Game_Cover_Images/" + game.CoverImagePath);
                    System.IO.File.Delete(fullFilePath);
                }

                gameService.Delete(game);
                gameService.Save();

                TempData["Message"] = "Movie deleted successfuly!";
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Database error!");
                return View(gameViewModel);
            }
        }
    }
}
