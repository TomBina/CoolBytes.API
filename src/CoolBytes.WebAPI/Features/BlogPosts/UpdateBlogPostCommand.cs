﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoolBytes.Core.Models;
using CoolBytes.WebAPI.Features.BlogPosts.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CoolBytes.WebAPI.Features.BlogPosts
{
    public class UpdateBlogPostCommand : IRequest<BlogPostSummaryViewModel>
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string ContentIntro { get; set; }
        public string Content { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public IFormFile File { get; set; }
    }
}
