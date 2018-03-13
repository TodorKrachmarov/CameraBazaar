namespace CameraBazaar.Services.Validations
{
    using System.ComponentModel.DataAnnotations;

    public class ImageURLAttribute : ValidationAttribute
    {
        public ImageURLAttribute()
        {
            ErrorMessage = @"Image URL must start with http:// or https://";
        }

        public override bool IsValid(object value)
        {
            var model = value as string;

            if (model == null)
            {
                return true;
            }

            return model.StartsWith("http://") || model.StartsWith("https://");
        }
    }
}
