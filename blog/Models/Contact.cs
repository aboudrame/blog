using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models
{
    public class Contact
    {
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body {get; set;}
        [Required]
        public DateTime Posted { get; set; }
        
        public string User { get; set; }
    }
}
