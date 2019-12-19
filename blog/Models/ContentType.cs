using blog.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models
{
    public class ContentType
    {
        public int ContentTypeId { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }
    }
}
