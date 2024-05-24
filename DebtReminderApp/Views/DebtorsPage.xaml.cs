using DebtReminderApp.ViewModels;

namespace DebtReminderApp.Views;

public partial class DebtorsPage : ContentPage
{
	private readonly DebtorsViewModel _viewModel;
	public DebtorsPage(DebtorsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		_viewModel = viewModel;
	}

	protected async override void OnAppearing()
	{
		base.OnAppearing();
		await _viewModel.LoadDebtorsAsync();
	}
}