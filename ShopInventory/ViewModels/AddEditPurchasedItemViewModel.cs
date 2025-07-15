using System.Collections.ObjectModel;
using System.Windows.Input;
using ShopInventory.Models;
using ShopInventory.Services;

namespace ShopInventory.ViewModels
{
    public class AddEditPurchasedItemViewModel : BaseViewModel
    {
        private readonly DatabaseService _databaseService;
        private PurchasedItem _currentItem;
        private string _itemName = string.Empty;
        private string _quantity = "1";
        private string _price = "0";
        private DateTime _purchaseDate = DateTime.Today;
        private ObservableCollection<string> _suggestions;
        private bool _showSuggestions;

        public AddEditPurchasedItemViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            _currentItem = new PurchasedItem();
            _suggestions = new ObservableCollection<string>();

            Title = "Add Purchased Item";

            SaveCommand = new Command(async () => await SaveItem(), () => CanSave());
            CancelCommand = new Command(async () => await CancelEdit());
            SelectSuggestionCommand = new Command<string>(OnSuggestionSelected);
            HideSuggestionsCommand = new Command(HideSuggestions);
        }

        public string ItemName
        {
            get => _itemName;
            set
            {
                SetProperty(ref _itemName, value);
                ((Command)SaveCommand).ChangeCanExecute();
                _ = UpdateSuggestions(value);
            }
        }

        public string Quantity
        {
            get => _quantity;
            set
            {
                SetProperty(ref _quantity, value);
                ((Command)SaveCommand).ChangeCanExecute();
            }
        }

        public string Price
        {
            get => _price;
            set
            {
                SetProperty(ref _price, value);
                ((Command)SaveCommand).ChangeCanExecute();
            }
        }

        public DateTime PurchaseDate
        {
            get => _purchaseDate;
            set => SetProperty(ref _purchaseDate, value);
        }

        public ObservableCollection<string> Suggestions
        {
            get => _suggestions;
            set => SetProperty(ref _suggestions, value);
        }

        public bool ShowSuggestions
        {
            get => _showSuggestions;
            set => SetProperty(ref _showSuggestions, value);
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand SelectSuggestionCommand { get; }
        public ICommand HideSuggestionsCommand { get; }

        public async Task LoadItem(int itemId)
        {
            if (itemId == 0)
            {
                _currentItem = new PurchasedItem();
                Title = "Add Purchased Item";
                
                // Reset to default values for new item
                ItemName = string.Empty;
                Quantity = "1";
                Price = "0";
                PurchaseDate = DateTime.Today;
            }
            else
            {
                var items = await _databaseService.GetPurchasedItemsAsync();
                _currentItem = items.FirstOrDefault(x => x.Id == itemId) ?? new PurchasedItem();
                Title = "Edit Purchased Item";

                ItemName = _currentItem.ItemName;
                Quantity = _currentItem.Quantity.ToString();
                Price = _currentItem.Price.ToString();
                PurchaseDate = _currentItem.PurchaseDate;
            }
        }

        private async Task UpdateSuggestions(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText) || searchText.Length < 2)
            {
                ShowSuggestions = false;
                return;
            }

            try
            {
                var purchasedItems = await _databaseService.GetPurchasedItemsAsync();
                var soldItems = await _databaseService.GetSoldItemsAsync();

                var allItemNames = purchasedItems.Select(x => x.ItemName)
                    .Union(soldItems.Select(x => x.ItemName))
                    .Where(name => !string.IsNullOrEmpty(name))
                    .Distinct()
                    .Where(name => name.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(name => name)
                    .ToList();

                Suggestions.Clear();
                foreach (var name in allItemNames)
                {
                    Suggestions.Add(name);
                }

                ShowSuggestions = Suggestions.Count > 0;
            }
            catch (Exception ex)
            {
                // Handle error silently or log it
                ShowSuggestions = false;
            }
        }

        private void OnSuggestionSelected(string suggestion)
        {
            if (!string.IsNullOrEmpty(suggestion))
            {
                ItemName = suggestion;
                ShowSuggestions = false;
            }
        }

        private void HideSuggestions()
        {
            ShowSuggestions = false;
        }

        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(ItemName) && 
                   int.TryParse(Quantity, out int qty) && qty > 0 &&
                   decimal.TryParse(Price, out decimal price) && price > 0;
        }

        private async Task SaveItem()
        {
            if (IsBusy) return;

            IsBusy = true;
            try
            {
                _currentItem.ItemName = ItemName;
                _currentItem.Quantity = int.Parse(Quantity);
                _currentItem.Price = decimal.Parse(Price);
                _currentItem.PurchaseDate = PurchaseDate;

                await _databaseService.SavePurchasedItemAsync(_currentItem);
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task CancelEdit()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}