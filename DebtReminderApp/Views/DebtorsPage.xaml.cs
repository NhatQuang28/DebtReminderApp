using DebtReminderApp.Models;
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
	public async void OnEditButtonClicked(object sender, EventArgs e)
	{
		if (sender is ImageButton button && button.CommandParameter is int debtorId)
		{
			await _viewModel.NavEditDebtorPage(debtorId);
		}
	}

	public async void OnRemoveButtonClicked(object sender, EventArgs e)
	{
		if (sender is ImageButton button && button.CommandParameter is int debtorId)
		{
			await _viewModel.RemoveDebtor(debtorId);
		}
	}

	public async void OnItemTapped(object sender, EventArgs e)
	{
		if (sender is Frame frame && frame.BindingContext is Debtor debtor)
		{
			await _viewModel.NavViewDebtorPage(debtor.Id);
		}
	}
}