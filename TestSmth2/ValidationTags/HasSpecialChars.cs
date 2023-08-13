using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TestSmth2.ValidationTags
{
    public class HasSpecialChars : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is string stringValue)
            {
                var regex = new Regex("[^a-zA-Z0-9]");
                return regex.IsMatch(stringValue);
            }
            return true;
        }
    }
}
