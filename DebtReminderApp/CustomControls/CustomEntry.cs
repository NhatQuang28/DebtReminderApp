namespace DebtReminderApp.CustomControls
{
	//Pending customer UI
	public sealed class CustomEntry : Entry
	{
		public static BindableProperty CorrnerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(CustomEntry), 0);

		public int CornerRadius
		{
			get => (int)GetValue(CorrnerRadiusProperty);
			set => SetValue(CorrnerRadiusProperty, value);
		}
	}
}
