﻿using System;

namespace CoolBytes.WebAPI.Features.BlogPosts
{
    public class BlogPostViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Updated { get; set; }
        public string Subject { get; set; }
        public string ContentIntro { get; set; }
        public string Content { get; set; }
        public string AuthorName { get; set; }
    }
}