using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace blog.ViewModels
{
    public class RegisterCommentViewModel
    {
        public long Id { get; set; }
        public string BlogTitle { get; set; }
        public string BlogBody { get; set; }
        [Required]
        public string CommentBody { get; set; }
        public string Commenter { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime LastModifiedDate { get; set; }
        [Required]
        public long BlogId { get; set; }
    }
}
