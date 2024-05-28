using DebtReminderApp.ViewModels;

namespace DebtReminderApp.Views;

public partial class DebtorPage : ContentPage
{
	private DebtorViewModel _viewModel;
	public DebtorPage(DebtorViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		_viewModel = viewModel;
	}
	protected async override void OnAppearing()
	{
		base.OnAppearing();
		await _viewModel.LoadDebtorAsync();
	}

	protected override async bool OnBackButtonPressed()
	{
		await _viewModel.ClearDebtorAsync();
		return base.OnBackButtonPressed();
	}
}