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
        private int _quantity = 1;
        private decimal _price = 0;
        private DateTime _purchaseDate = DateTime.Today;

        public AddEditPurchasedItemViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            _currentItem = new PurchasedItem();

            Title = "Add Item";

            SaveCommand = new Command(async () => await SaveItem(), () => CanSave());
            CancelCommand = new Command(async () => await CancelEdit());
        }

        public string ItemName
        {
            get => _itemName;
            set
            {
                SetProperty(ref _itemName, value);
                ((Command)SaveCommand).ChangeCanExecute();
            }
        }

        public int Quantity
        {
            get => _quantity;
            set
            {
                SetProperty(ref _quantity, value);
                ((Command)SaveCommand).ChangeCanExecute();
            }
        }

        public decimal Price
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

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public async Task LoadItem(int itemId)
        {
            if (itemId == 0)
            {
                _currentItem = new PurchasedItem();
                Title = "Add Purchased Item";
            }
            else
            {
                var items = await _databaseService.GetPurchasedItemsAsync();
                _currentItem = items.FirstOrDefault(x => x.Id == itemId) ?? new PurchasedItem();
                Title = "Edit Purchased Item";

                ItemName = _currentItem.ItemName;
                Quantity = _currentItem.Quantity;
                Price = _currentItem.Price;
                PurchaseDate = _currentItem.PurchaseDate;
            }
        }

        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(ItemName) && Quantity > 0 && Price > 0;
        }

        private async Task SaveItem()
        {
            if (IsBusy) return;

            IsBusy = true;
            try
            {
                _currentItem.ItemName = ItemName;
                _currentItem.Quantity = Quantity;
                _currentItem.Price = Price;
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