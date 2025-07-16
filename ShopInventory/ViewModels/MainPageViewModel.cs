using System.Windows.Input;
using ShopInventory.Views;

namespace ShopInventory.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel()
        {
            Title = "Shop Inventory";

            NavigateToDashboardCommand = new Command(async () => await NavigateToDashboard());
            NavigateToPurchasedItemsCommand = new Command(async () => await NavigateToPurchasedItems());
            NavigateToSoldItemsCommand = new Command(async () => await NavigateToSoldItems());
        }

        public ICommand NavigateToDashboardCommand { get; }
        public ICommand NavigateToPurchasedItemsCommand { get; }
        public ICommand NavigateToSoldItemsCommand { get; }

        private async Task NavigateToDashboard()
        {
            await Shell.Current.GoToAsync(nameof(DashboardPage));
        }

        private async Task NavigateToPurchasedItems()
        {
            await Shell.Current.GoToAsync(nameof(PurchasedItemsPage));
        }

        private async Task NavigateToSoldItems()
        {
            await Shell.Current.GoToAsync(nameof(SoldItemsPage));
        }
    }
}