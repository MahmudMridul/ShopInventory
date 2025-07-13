using System.Collections.ObjectModel;
using System.Windows.Input;
using ShopInventory.Models;
using ShopInventory.Services;
using ShopInventory.Views;

namespace ShopInventory.ViewModels
{
    public class PurchasedItemsViewModel : BaseViewModel
    {
        private readonly DatabaseService _databaseService;
        private ObservableCollection<PurchasedItem> _purchasedItems;

        public PurchasedItemsViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            _purchasedItems = new ObservableCollection<PurchasedItem>();

            Title = "Purchased Items";

            LoadItemsCommand = new Command(async () => await LoadItems());
            AddItemCommand = new Command(async () => await AddItem());
            EditItemCommand = new Command<PurchasedItem>(async (item) => await EditItem(item));
            DeleteItemCommand = new Command<PurchasedItem>(async (item) => await DeleteItem(item));
        }

        public ObservableCollection<PurchasedItem> PurchasedItems
        {
            get => _purchasedItems;
            set => SetProperty(ref _purchasedItems, value);
        }

        public ICommand LoadItemsCommand { get; }
        public ICommand AddItemCommand { get; }
        public ICommand EditItemCommand { get; }
        public ICommand DeleteItemCommand { get; }

        public async Task LoadItems()
        {
            if (IsBusy) return;

            IsBusy = true;
            try
            {
                var items = await _databaseService.GetPurchasedItemsAsync();
                PurchasedItems.Clear();
                foreach (var item in items)
                {
                    PurchasedItems.Add(item);
                }
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

        private async Task AddItem()
        {
            //await Shell.Current.GoToAsync(nameof(AddEditPurchasedItemPage));
        }

        private async Task EditItem(PurchasedItem item)
        {
            if (item == null) return;

            //await Shell.Current.GoToAsync($"{nameof(AddEditPurchasedItemPage)}?ItemId={item.Id}");
        }

        private async Task DeleteItem(PurchasedItem item)
        {
            if (item == null) return;

            var result = await Shell.Current.DisplayAlert("Confirm Delete",
                $"Are you sure you want to delete '{item.ItemName}'?", "Yes", "No");

            if (result)
            {
                await _databaseService.DeletePurchasedItemAsync(item);
                PurchasedItems.Remove(item);
            }
        }
    }
}