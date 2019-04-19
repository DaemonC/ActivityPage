using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DojoActivities.Models {
    public class Activ {

        [Key]
        public int ActivId { get; set; }

        [Required (ErrorMessage = "Activ  is required.")]
        [MinLength (2, ErrorMessage = "Activ must be at least 2 characters.")]
        [Display (Name = "Activ One:")]
        public string ActivOne { get; set; }

        [Required (ErrorMessage = "Activ Coordinator name is required.")]
        [MinLength (2, ErrorMessage = "Activ Coordinator name must be at least 2 characters.")]
        [Display (Name = "Coordinator:")]
        public string ActivTwo { get; set; }

        [Required (ErrorMessage = "Activ date is required.")]
        [Display (Name = "Activ Date:")]
        public DateTime ActivDate { get; set; }

        [Required (ErrorMessage = "Street address is required.")]
        [MinLength (2, ErrorMessage = "Street address must be at least 2 characters.")]
        [Display (Name = "Street Address:")]
        public string Street { get; set; }

        [Required (ErrorMessage = "City/Town is required.")]
        [MinLength (2, ErrorMessage = "City/Town must be at least 2 characters.")]
        [Display (Name = "City/Town:")]

        public string City { get; set; }

        [Required (ErrorMessage = "State is required.")]
        [Display (Name = "State:")]

        public string State { get; set; }

        [Required (ErrorMessage = "Zip code is required.")]
        [MinLength (5, ErrorMessage = "Zip code must be at least 5 characters.")]
        [Display (Name = "Zip code:")]

        public string Zip { get; set; }

        [Required (ErrorMessage = "What time is your event?")]
        [Display (Name = "Time: ")]
        public DateTime? Time { get; set; }

        [Required (ErrorMessage = "How long will your event last?")]
        [Display (Name = "Duration: ")]
        public int? Duration { get; set; }

        [Required (ErrorMessage = "Hours, minutes, or days?")]
        [Display (Name = "Duration (cont): ")]
        public string DuratUnit { get; set; }
        public int UserId { get; set; }

        public User Creator { get; set; }

        public List<Join> Joiners { get; set; } = new List<Join> ();

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}