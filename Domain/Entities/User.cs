using Bloggr.Domain.Entities;
using Bloggr.Domain.Interfaces;
using Domain.Abstracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : IdentityUser<int>, IBaseEntity
    {
        public DateTime CreationDate { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Bio { get; set; }

        public string? ProfileImageUrl { get; set; }

        public string? BackgroundImageUrl { get; set; }

        public DateTimeOffset BirthDate { get; set; }

        public ICollection<Interest>? CreatedInterests { get; set; }

        public ICollection<InterestUser>? InterestUsers { get; set; }

        public ICollection<Post>? Posts { get; set; }

        public ICollection<Bookmark>? Bookmarks { get; set; }

        public ICollection<Comment>? Comments { get; set; }

        public ICollection<Like>? Likes { get; set; }


    }
}
