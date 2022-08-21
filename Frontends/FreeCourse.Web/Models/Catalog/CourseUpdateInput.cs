using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models.Catalog
{
    public class CourseUpdateInput
    {
        public string Id { get; set; }
        public string Picture { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public FeatureVM Feature { get; set; }
        [Required]
        public string CategoryId { get; set; }
        [Display(Name = "Photo")]
        public IFormFile PhotoFormFile { get; set; }
    }
}
