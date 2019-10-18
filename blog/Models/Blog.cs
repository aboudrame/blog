﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models
{

    public class Blog
    {
        public long BlogId { get; set; }
        [Required]
        public string Title {get;  set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public DateTime Posted { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public int ContentTypeId { get; set; } = 2;
        [NotMapped]
        public List<ContentType> ContentTypes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }

    public class ContentType
    {
        public int ContentTypeId { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }
    }

    public class Comment
    {
        public long CommentId { get; set; }
        [Required]
        public string Body { get; set; }
        public string HTML { get; set; }
        public string CSS { get; set; }
        public string JavaScript { get; set; }
        [Required]
        public string Commenter { get; set; }
        [Required]
        public DateTime DateCommented { get; set; } 
        [Required]
        public long BlogId { get; set; }
        public virtual Blog blog { get; set; }
    }

  
}


