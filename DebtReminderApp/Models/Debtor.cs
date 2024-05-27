using SQLite;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DebtReminderApp.Models
{
	public class Debtor
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		[Required(ErrorMessage = "Name is required.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Phone Number is required.")]
		[RegularExpression("^(0|84)(2(0[3-9]|1[0-689]|2[0-25-9]|3[2-9]|4[0-9]|5[124-9]|6[0369]|7[0-7]|8[0-9]|9[012346789])|3[2-9]|5[25689]|7[06-9]|8[0-9]|9[012346789])([0-9]{7})$", ErrorMessage = "Invalid phone number.")]
		public string PhoneNumber { get; set; }

		[Range(0, double.MaxValue, ErrorMessage = "Total Debt must be a positive number.")]
		public double TotalDebt { get; set; }

		public string? Description { get; set; }

		[DefaultValue(typeof(DateTime), "")]
		public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;

		[Required(ErrorMessage = "Status is required.")]
		public string Status { get; set; }
	}
}
