using DataAccess;
using GamerListMVC.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GamerListMVC.Models
{
    public class UserProfileViewModel
    {
        public string Email { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Profile Image")]
        public string ProfileImagePath { get; set; }

        [FileType("JPG, JPEG, PNG")]
        [FileSize(2000000)]
        public HttpPostedFileBase ProfileImageFile { get; set; }

        public UserProfileViewModel()
        {
        }
        public UserProfileViewModel(ApplicationUser user)
        {
            Email = user.Email;
            UserName = user.UserName;
            ProfileImagePath = user.ProfileImagePath;
        }
    }
}