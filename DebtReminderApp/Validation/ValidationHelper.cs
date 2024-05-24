using System.ComponentModel.DataAnnotations;

namespace DebtReminderApp.Validation
{
	public class ValidationHelper
	{
		public static IList<ValidationResult> Validate(object obj)
		{
			var validationResults = new List<ValidationResult>();
			var validationContext = new ValidationContext(obj, null, null);
			Validator.TryValidateObject(obj, validationContext, validationResults, true);
			return validationResults;
		}
	}
}
