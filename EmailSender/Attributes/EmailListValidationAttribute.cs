using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace EmailSender.Attributes;
public class EmailListValidationAttribute : ValidationAttribute
{
    private const string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
    public override bool IsValid(object value)
    {
        var emailList = value as List<string>;

        if (emailList == null || emailList.Count == 0)
            return false;

        return emailList.All(email => Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase));
    }
}
