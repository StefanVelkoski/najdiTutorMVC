using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FindTutorMVC.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Reviewed announcement")]
        public Announcement Announcement { get; set; }

        [Display(Name = "Submitted by")]
        public ApplicationUser Submitter { get; set; }

        [Display(Name = "Given score")]
        public int Score { get; set; }

        [Display(Name = "Short reasoning")]
        public string Reason { get; set; }
    }
}