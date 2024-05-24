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
			NewCommand = new AsyncRelayCommand(OnAddClick);
		}

		[ObservableProperty]
		private ObservableCollection<Debtor> _debtors = new();

		public ICommand NewCommand { get; }

		public async Task LoadDebtorsAsync()
		{
			await ExecuteAsync(async () =>
			{
				var debtors = await _context.GetAllAsync<Debtor>();
				if (debtors is not null && debtors.Any())
				{
					Debtors ??= new ObservableCollection<Debtor>();

					foreach (var debtor in debtors)
					{
						Debtors.Add(debtor);
					}
				}
			});
		}

		[RelayCommand]
		private async Task SelectDebtorAsync(int id)
		{
			await Shell.Current.GoToAsync($"{nameof(DebtorPage)}?{VIEW_EVENT}={id}");
		}

		private async Task OnAddClick()
		{
			await Shell.Current.GoToAsync(nameof(DebtorPage));
		}

		[RelayCommand]
		public async Task OnEditClick(int id)
		{
			await Shell.Current.GoToAsync($"{nameof(DebtorPage)}?{EDIT_EVENT}={id}");
		}

		[RelayCommand]
		public async Task OnRemoveClick(int id)
		{
			await ExecuteAsync(async () =>
			{
				if (await _context.DeleteItemByKeyAsync<Debtor>(id))
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
