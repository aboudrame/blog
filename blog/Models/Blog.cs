using blog.Data;
using System;
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
        public string Title { get; set; }
        [Required]
        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime LastModifiedDate { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public int ContentTypeId { get; set; }
        [NotMapped]
        public List<ContentType> ContentTypes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }

  

}
