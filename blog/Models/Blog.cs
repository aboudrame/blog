using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models
{
    public class Blog
    {
        public long BlogId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime Posted { get; set; }
        public string Body { get; set; }
        public string HTML { get; set; }
        public string CSS { get; set; }
        public string JavaScript { get; set; }
        public string Result { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }

    public class Category
    {
        public int CategoryId { get; set; }
        public  string Name { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }
    }

    public class Comment
    {
        public long CommentId { get; set; }
        public string Body { get; set; }
        public string Commenter { get; set; }
        public DateTime DateCommented { get; set; } 
        public long BlogId { get; set; }
        public virtual Blog blog { get; set; }

        
    }
}


