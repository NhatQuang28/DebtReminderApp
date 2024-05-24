namespace DebtReminderApp.Constants
{
	public static class AppConstants
	{
		[Flags]
		public enum DebtStatus
		{
			Pending,
			Paid,
			Overdue,
		}

		public const string NEW_EVENT = "New";
		public const string EDIT_EVENT = "Edit";
		public const string VIEW_EVENT = "View";
		public const string DELETE_EVENT = "Delete";

	}
}
