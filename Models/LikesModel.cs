using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace C_Sharp_Belt_II.Models
{
    public class Likes
    {
        [Key]
        public int LikesId { get; set; }

        public int IdeaId { get; set; }
        public Ideas LikedIdea { get; set; }

        public int UserId { get; set; }
        public User LikedUser { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}