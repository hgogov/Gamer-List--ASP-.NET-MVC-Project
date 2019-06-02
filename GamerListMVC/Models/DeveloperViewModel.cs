using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GamerListMVC.Models
{
    public class DeveloperViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Name { get; set; }

        public DeveloperViewModel()
        {
        }

        public DeveloperViewModel(Developer developer)
        {
            Id = developer.Id;
            Name = developer.Name;
        }
    }
}