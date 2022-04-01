using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Online_Shoping.Models
{
    public class UniqueAttribute : ValidationAttribute
    {
        public string ErrorMsg { get; set; }
        Context context = new Context();
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            if (value != null)
            {
                Category category = context.categories.FirstOrDefault(s => s.name == value.ToString());
                if (category == null)
                    return ValidationResult.Success;
                else
                    return new ValidationResult("This Category Already Exist");
            }
            return new ValidationResult("Category Name Required");
        }
    }
}
