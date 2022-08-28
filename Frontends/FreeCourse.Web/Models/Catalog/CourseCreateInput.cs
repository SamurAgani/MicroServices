﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models.Catalog
{
    public class CourseCreateInput
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Picture { get; set; }
        public string UserId { get; set; }
        public FeatureVM Feature { get; set; }
        [Display(Name="Category")]
        public string CategoryId { get; set; }
        [Display(Name = "Photo")]
        public IFormFile PhotoFormFile { get; set; }
    }
}
