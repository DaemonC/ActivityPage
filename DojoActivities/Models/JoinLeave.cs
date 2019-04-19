using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DojoActivities.Models {
    public class Join {

        [Key]
        public int JoinId { get; set; }
        public int UserId { get; set; }
        public int ActivId { get; set; }
        public User User { get; set; }
        public Activ Activ { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}