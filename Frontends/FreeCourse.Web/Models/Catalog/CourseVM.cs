﻿using FreeCourse.Web.Models.Catalog;
using System;

namespace FreeCourse.Web.Models
{
    public class CourseVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; }
        public string StockPictureUrl { get; set; }


        public DateTime CreatedDate { get; set; }

        public FeatureVM Feature { get; set; }


        public string CategoryId { get; set; }


        public CategoryVM Category { get; set; }
    }
}
