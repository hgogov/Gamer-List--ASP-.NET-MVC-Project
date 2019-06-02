using Business.Services.DeveloperService;
using Business.Services.GenreService;
using DataAccess.Entities;
using GamerListMVC.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GamerListMVC.Models
{
    public class GameViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Developer")]
        public int DeveloperId { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        [Required]
        [MaxLength(2048)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }
        
        [Display(Name = "Upload Image")]
        public string CoverImagePath { get; set; }
        
        [FileType("JPG, JPEG, PNG")]
        [FileSize(2000000)]
        public HttpPostedFileBase CoverImageFile { get; set; }

        public Developer Developer { get; set; }
        public Genre Genre { get; set; }

        public IEnumerable<SelectListItem> Developers { get; set; }
        public IEnumerable<SelectListItem> Genres { get; set; }
        public IEnumerable<SelectListItem> Ratings { get; set; }
        public IEnumerable<SelectListItem> Statuses { get; set; }

        public int SelectedRating { get; set; }
        public string SelectedStatus { get; set; }

        public GameViewModel()
        {
            using (var developerService = new DeveloperService())
            {
                Developers = developerService.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            };
            using (var genreService = new GenreService())
            {
                Genres = genreService.GetAll().Select(x => new SelectListItem { Text = x.Type, Value = x.Id.ToString() }).ToList();
            };
        }

        public GameViewModel(Game game)
        {
            Id = game.Id;
            Title = game.Title;
            DeveloperId = game.DeveloperId;
            GenreId = game.GenreId;
            Description = game.Description;
            ReleaseDate = game.ReleaseDate;
            Developer = game.Developer;
            Genre = game.Genre;
            CoverImagePath = game.CoverImagePath;
        }
    }
}