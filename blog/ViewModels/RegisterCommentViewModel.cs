using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace blog.ViewModels
{
    public class RegisterCommentViewModel
    {
        public string BlogBody { get; set; }
        public string BlogHTML { get; set; }
        public string BlogCSS { get; set; }
        public string BlogJavaScript { get; set; }
        [Required]
        public string CommentBody { get; set; }
        public string CommentHTML { get; set; }
        public string CommentCSS { get; set; }
        public string CommentJavaScript { get; set; }
        public string Commenter { get; set; }
        [Required]
        public DateTime DateCommented { get; set; }
        [Required]
        public long BlogId { get; set; }
    }
}
