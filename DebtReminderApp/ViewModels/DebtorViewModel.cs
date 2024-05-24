using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DebtReminderApp.Data;
using DebtReminderApp.Models;
using DebtReminderApp.Validation;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using static DebtReminderApp.Constants.AppConstants;

namespace DebtReminderApp.ViewModels
{
	public partial class DebtorViewModel : ObservableObject, IQueryAttributable
	{
		private readonly DatabaseContext _context;

		[ObservableProperty]
		private int _idDebter = 0;

		[ObservableProperty]
		private Debtor _debtor = new();

		public DebtorViewModel(DatabaseContext context)
		{
			this._context = context;
			SaveCommand = new AsyncRelayCommand(SaveDebtorAsync);
		}

		[ObservableProperty]
		private bool _isNew;

		[ObservableProperty]
		private bool _isEdit;

		[ObservableProperty]
		private bool _isView;

		public ICommand SaveCommand { get; }

		public async Task LoadDebtorAsync()
		{
			if (IdDebter != 0)
			{
				await ExecuteAsync(async () =>
				{
					var debtor = await _context.GetItemByKeyAsync<Debtor>(IdDebter);
					Debtor = debtor;
				});
			}
		}

		[RelayCommand]
		private async Task SaveDebtorAsync()
		{
			//Validation infor
			var validationResults = ValidationHelper.Validate(Debtor);

			if (validationResults.Any())
			{
				DisplayValidationErrors(validationResults);
				return;
			}

			await ExecuteAsync(async () =>
			{
				if (IdDebter == 0)
				{
					if (await _context.AddItemAsync<Debtor>(Debtor))
					{
						await Shell.Current.DisplayAlert("Success", "Debtor information updation successfully.", "Ok");
					}
					else
					{
						await Shell.Current.DisplayAlert("Error", "Debtor updation error", "Ok");
					}
				}
				else
				{
					if (await _context.UpdateItemAsync<Debtor>(Debtor))
					{
						await Shell.Current.DisplayAlert("Success", "Debtor information updation successfully.", "Ok");
					}
					else
					{
						await Shell.Current.DisplayAlert("Error", "Debtor updation error", "Ok");

					}
				}
			});
		}

		private async void DisplayValidationErrors(IEnumerable<ValidationResult> validationResults)
		{
			var errorMessage = string.Join(Environment.NewLine, validationResults.Select(r => r.ErrorMessage));
			await Shell.Current.DisplayAlert("Validation Error", errorMessage, "Ok");
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

		public void ApplyQueryAttributes(IDictionary<string, object> query)
		{
			if (query.ContainsKey(EDIT_EVENT))
			{
				IdDebter = int.Parse(query[EDIT_EVENT].ToString());
			}
			else if (query.ContainsKey(VIEW_EVENT))
			{
				IdDebter = int.Parse(query[VIEW_EVENT].ToString());
			}
		}
	}
}
