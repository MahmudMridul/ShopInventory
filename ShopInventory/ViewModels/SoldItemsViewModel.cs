using System.Collections.ObjectModel;
using System.Windows.Input;
using ShopInventory.Models;
using ShopInventory.Services;
using ShopInventory.Views;

namespace ShopInventory.ViewModels
{
    public class SoldItemsViewModel : BaseViewModel
    {
        private readonly DatabaseService _databaseService;
        private ObservableCollection<SoldItem> _soldItems;

        public SoldItemsViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            _soldItems = new ObservableCollection<SoldItem>();

            Title = "Sold Items";

            LoadItemsCommand = new Command(async () => await LoadItems());
            AddItemCommand = new Command(async () => await AddItem());
            EditItemCommand = new Command<SoldItem>(async (item) => await EditItem(item));
            DeleteItemCommand = new Command<SoldItem>(async (item) => await DeleteItem(item));
        }

        public ObservableCollection<SoldItem> SoldItems
        {
            get => _soldItems;
            set => SetProperty(ref _soldItems, value);
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
                var items = await _databaseService.GetSoldItemsAsync();
                SoldItems.Clear();
                foreach (var item in items)
                {
                    SoldItems.Add(item);
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
            await Shell.Current.GoToAsync(nameof(AddEditSoldItemPage));
        }

        private async Task EditItem(SoldItem item)
        {
            if (item == null) return;

            await Shell.Current.GoToAsync($"{nameof(AddEditSoldItemPage)}?ItemId={item.Id}");
        }

        private async Task DeleteItem(SoldItem item)
        {
            if (item == null) return;

            var result = await Shell.Current.DisplayAlert("Confirm Delete",
                $"Are you sure you want to delete '{item.ItemName}'?", "Yes", "No");

            if (result)
            {
                await _databaseService.DeleteSoldItemAsync(item);
                SoldItems.Remove(item);
            }
        }
    }
}