using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DebtReminderApp.Data;
using DebtReminderApp.Models;
using DebtReminderApp.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;
using static DebtReminderApp.Constants.AppConstants;

namespace DebtReminderApp.ViewModels
{
	public partial class DebtorsViewModel : ObservableObject, IQueryAttributable
	{
		private readonly DatabaseContext _context;

		public DebtorsViewModel(DatabaseContext context)
		{
			this._context = context;
			NewDebtorCommand = new AsyncRelayCommand(NavAddDebtorPage);
			//EditDebtorPageCommand = new AsyncRelayCommand<int>(NavEditDebtorPage);
			RemoveDebtorCommand = new AsyncRelayCommand<int>(RemoveDebtor);
			//ViewDebtorCommand = new AsyncRelayCommand<int>(NavViewDebtorPage);
		}

		[ObservableProperty]
		private ObservableCollection<Debtor> _debtors = new();

		public ICommand NewDebtorCommand { get; }
		//public ICommand EditDebtorPageCommand { get; }
		public ICommand RemoveDebtorCommand { get; }
		//public ICommand ViewDebtorCommand { get; }

		public async Task LoadDebtorsAsync()
		{
			await ExecuteAsync(async () =>
			{
				var debtors = await _context.GetFileteredAsync<Debtor>(d => d.IsDeleted == false);
				if (debtors != null && debtors.Any())
				{
					var sortedDebtors = debtors.OrderByDescending(d => d.ModifiedDate).ToList();

					if (Debtors == null)
					{
						Debtors = new ObservableCollection<Debtor>(sortedDebtors);
					}
					else
					{
						Debtors.Clear();
						foreach (var debtor in sortedDebtors)
						{
							Debtors.Add(debtor);
						}
					}
				}
			});
		}
		private async Task NavAddDebtorPage()
		{
			await Shell.Current.GoToAsync(nameof(DebtorPage));
		}

		public async Task NavEditDebtorPage(int id)
		{
			await Shell.Current.GoToAsync($"{nameof(DebtorPage)}?{EDIT_EVENT}={id}");
		}

		public async Task NavViewDebtorPage(int id)
		{
			await Shell.Current.GoToAsync($"{nameof(DebtorPage)}?{VIEW_EVENT}={id}");
		}

		public async Task RemoveDebtor(int id)
		{
			var debtor = await _context.GetItemByKeyAsync<Debtor>(id);

			bool result = await Shell.Current.DisplayAlert("Confirm Delete", "Are you sure you want to delete this debtor?", "Yes", "No");

			if (debtor != null && result)
			{
				await ExecuteAsync(async () =>
				{
					debtor.IsDeleted = true;

					if (await _context.UpdateItemAsync<Debtor>(debtor))
					{
						var debtor = Debtors.FirstOrDefault(p => p.Id == id);
						Debtors.Remove(debtor);
					}
					else
					{
						await Shell.Current.DisplayAlert("Delete Error", "Debtor was not deleted", "Ok");
					}
				});
			}
		}

		private async Task ExecuteAsync(Func<Task> operation)
		{
			try
			{
				await operation?.Invoke();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			finally
			{
				//do something
			}
		}

		void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
		{
			//TODO: Handle reload grid on add or edit data
		}
	}
}
