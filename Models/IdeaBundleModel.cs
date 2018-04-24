using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace C_Sharp_Belt_II.Models
{
    public class IdeaBundleModel
    {
        public Ideas Idea  { get; set; }
        public List<Ideas> Ideas { get; set; }

        public IdeaBundleModel(BeltExamContext _context){
            Idea = new Ideas();
            Ideas = _context.ideas.Include( lu => lu.Likes ).ThenInclude( l => l.LikedUser ).Include( u => u.IdeaCreator ).OrderByDescending( d => d.Likes.Count() ).ToList(); //.OrderBy( d => d.Likes )
        }
    }
}