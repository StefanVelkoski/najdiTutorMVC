using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FindTutorMVC.Models
{
    public class FavouriteAnnouncementToUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AnnouncementId { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}