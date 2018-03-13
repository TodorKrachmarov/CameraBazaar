namespace CameraBazaar.Services.Validations
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class MaxISOAttribute : ValidationAttribute
    {
        public MaxISOAttribute()
        {
            ErrorMessage = @"Max ISO must be a number in range 200 to 409600 and must be dividable by 100";
        }

        public override bool IsValid(object value)
        {
            var valueAsString = value as string;

            if (valueAsString == null)
            {
                return true;
            }

            var maxISO = double.Parse(valueAsString);

            return maxISO % 100.0 == 0;
        }
    }
}
