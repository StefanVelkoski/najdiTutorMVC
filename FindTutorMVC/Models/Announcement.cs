using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.UI.WebControls;

namespace FindTutorMVC.Models
{
    public class Announcement
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Field of study")]
        public string Category { get; set; }

        public List<string> Categories { get; set; }

        [Display(Name = "Published by")]
        public ApplicationUser Tutor { get; set; }

        [Range(1, 1000, ErrorMessage = "You must enter a positive value.")]
        public int Price { get; set; }

        [Display(Name = "Level of difficulty")]
        public string Difficulty { get; set; }

        public List<string> Difficulties { get; set; }

        [Display(Name = "Short course summary")]
        public string Description { get; set; }

        [Display(Name = "Customers' score")]
        public int Score { get; set; }

        [Display(Name = "Creation date")]
        public DateTime Date { get; set; }

        public Announcement()
        {
            Difficulties = new List<string>(3) {
                "Beginner", "Intermediate", "Advanced"
            };

            Categories = new List<string>() {
                "Arts", "Languages", "Mathematics", "Music",
                "Computer Science", "Natural and Social Sciences"
            };

            Score = 0;
        }
    }
}