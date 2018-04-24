using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace C_Sharp_Belt_II.Models
{
    public class Ideas
    {
        [Key]
        public int IdeaId { get; set; }

        public int IdeaCreatorId { get; set; }
        public User IdeaCreator { get; set; }

        [Required(ErrorMessage = "Ideas are required!")]
        [MinLength(2, ErrorMessage = "Ideas must contain at least 2 characters!")]
        public string Content { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public List<Likes> Likes { get; set; }
 
        public Ideas()
        {
            Likes = new List<Likes>();
        }
    }
}