namespace CameraBazaar.Web.Infrastructure.Validations
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class ModelAttribute : ValidationAttribute
    {
        public ModelAttribute()
        {
            ErrorMessage = @"Model can contain only uppercase letters, digits and dash (“-“).";
        }

        public override bool IsValid(object value)
        {
            var model = value as string;

            if (model == null)
            {
                return true;
            }

            return model.All(s => (char.IsLetter(s) && char.IsUpper(s)) || s.Equals('-') || char.IsDigit(s));
        }
    }
}
