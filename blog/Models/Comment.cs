using blog.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models
{
    public class Comment
    {
        public long CommentId { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public string Commenter { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime LastModifiedDate { get; set; }
        [Required]
        public long BlogId { get; set; }
        public virtual Blog blog { get; set; }
    }
}
