using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindTutorMVC.Models
{
    public class ReviewForAnnouncement
    {
        public String UserId { get; set; }

        public int AnnouncementId { get; set; }

        public Review Review { get; set; }
    }
}