﻿using blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.ViewModels
{
    public class BlogCommentViewModel
    {
        public Blog Blog { get; set; }
        public Comment Comment { get; set; }
    }
}
