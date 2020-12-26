using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FindTutorMVC.Models
{
    public class AnnouncementToTutor
    {
        public String TutorId { get; set; }

        public Announcement Announcement { get; set; }
    }
}