using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataAccess.Entities
{
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        public int DeveloperId { get; set; }

        [Required]
        public int GenreId { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Title { get; set; }

        [Required]
        [MaxLength(2048)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        public string CoverImagePath { get; set; }

        [ForeignKey("DeveloperId")]
        public virtual Developer Developer { get; set; }

        [ForeignKey("GenreId")]
        public virtual Genre Genre { get; set; }
    }
}
