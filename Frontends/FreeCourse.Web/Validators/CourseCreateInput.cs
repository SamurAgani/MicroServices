using FluentValidation;
using FreeCourse.Web.Models.Catalog;

namespace FreeCourse.Web.Validators
{
    public class CourseCreateInputValidator : AbstractValidator<CourseCreateInput>
    {
        public CourseCreateInputValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Can't empty");
            RuleFor(c => c.Description).NotEmpty().WithMessage("Can't empty");
            RuleFor(c => c.CategoryId).NotEmpty().WithMessage("Can't empty");
            RuleFor(c => c.Feature.Duration).InclusiveBetween(1,int.MaxValue).WithMessage("Can't empty");
            RuleFor(c => c.Price).NotEmpty().WithMessage("Can't empty").ScalePrecision(2,6).WithMessage("Format is not correct");
        }
    }
}
