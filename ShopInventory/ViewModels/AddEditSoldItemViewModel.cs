using System.Windows.Input;
using ShopInventory.Models;
using ShopInventory.Services;

namespace ShopInventory.ViewModels
{
    public class AddEditSoldItemViewModel : BaseViewModel
    {
        private readonly DatabaseService _databaseService;
        private SoldItem _currentItem;
        private string _itemName = string.Empty;
        private int _quantity = 1;
        private decimal _price = 0;
        private DateTime _saleDate = DateTime.Today;

        public AddEditSoldItemViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            _currentItem = new SoldItem();

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

        public DateTime SaleDate
        {
            get => _saleDate;
            set => SetProperty(ref _saleDate, value);
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public async Task LoadItem(int itemId)
        {
            if (itemId == 0)
            {
                _currentItem = new SoldItem();
                Title = "Add Sold Item";
            }
            else
            {
                var items = await _databaseService.GetSoldItemsAsync();
                _currentItem = items.FirstOrDefault(x => x.Id == itemId) ?? new SoldItem();
                Title = "Edit Sold Item";

                ItemName = _currentItem.ItemName;
                Quantity = _currentItem.Quantity;
                Price = _currentItem.Price;
                SaleDate = _currentItem.SaleDate;
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
                _currentItem.SaleDate = SaleDate;

                await _databaseService.SaveSoldItemAsync(_currentItem);
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