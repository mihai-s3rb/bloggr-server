using Bloggr.Domain.Entities;
using Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Post : BaseEntity
    {
        [NotMapped]
        public int RANK { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Caption { get; set; }

        public int Views { get; set; }

        public string? CaptionImageUrl { get; set; }

        public ICollection<InterestPost> InterestPosts { get; set; }

        public ICollection<Bookmark> Bookmarks { get; set; }

        public ICollection<Like>? Likes { get; init; }

        public ICollection<Comment>? Comments { get; init; }

        public int? UserId { get; set; }

        public User? User { get; set; }

        [NotMapped]
        public int? NumberOfLikes { get; set; }

        [NotMapped]
        public int? NumberOfComments { get; set; }

        [NotMapped]
        public bool? IsLikedByUser { get; set; }

        [NotMapped]
        public bool? IsBookmarkedByUser { get; set; }
    }
}
