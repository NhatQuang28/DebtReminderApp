using DebtReminderApp.Views;

namespace DebtReminderApp
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();
			Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
			Routing.RegisterRoute(nameof(DebtorsPage), typeof(DebtorsPage));
			Routing.RegisterRoute(nameof(DebtorPage), typeof(DebtorPage));
			Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
		}
	}
}
