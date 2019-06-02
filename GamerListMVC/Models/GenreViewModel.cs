using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GamerListMVC.Models
{
    public class GenreViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Type { get; set; }

        public GenreViewModel()
        {
        }
        
        public GenreViewModel(Genre genre)
        {
            Id = genre.Id;
            Type = genre.Type;
        }
    }
}