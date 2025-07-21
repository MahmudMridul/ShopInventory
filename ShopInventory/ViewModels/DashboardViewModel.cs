using ShopInventory.Services;
using System.Windows.Input;

namespace ShopInventory.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        private readonly DatabaseService _databaseService;
        private int _totalSoldThisMonth;
        private int _totalPurchasedThisMonth;
        private decimal _totalAmountPurchasedThisMonth;
        private decimal _totalAmountSoldThisMonth;
        private decimal _profitThisMonth;

        public DashboardViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            Title = "Dashboard";
            
            LoadDashboardDataCommand = new Command(async () => await LoadDashboardData());
        }

        public int TotalSoldThisMonth
        {
            get => _totalSoldThisMonth;
            set => SetProperty(ref _totalSoldThisMonth, value);
        }

        public int TotalPurchasedThisMonth
        {
            get => _totalPurchasedThisMonth;
            set => SetProperty(ref _totalPurchasedThisMonth, value);
        }

        public decimal TotalAmountPurchasedThisMonth
        {
            get => _totalAmountPurchasedThisMonth;
            set => SetProperty(ref _totalAmountPurchasedThisMonth, value);
        }

        public decimal TotalAmountSoldThisMonth
        {
            get => _totalAmountSoldThisMonth;
            set => SetProperty(ref _totalAmountSoldThisMonth, value);
        }

        public decimal ProfitThisMonth
        {
            get => _profitThisMonth;
            set => SetProperty(ref _profitThisMonth, value);
        }

        public ICommand LoadDashboardDataCommand { get; }

        public async Task LoadDashboardData()
        {
            if (IsBusy) return;

            IsBusy = true;
            try
            {
                var startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

                var soldItems = await _databaseService.GetSoldItemsAsync();
                var purchasedItems = await _databaseService.GetPurchasedItemsAsync();

                // Filter items for current month
                var soldItemsThisMonth = soldItems.Where(item => item.SaleDate >= startOfMonth && item.SaleDate <= endOfMonth).ToList();
                var purchasedItemsThisMonth = purchasedItems.Where(item => item.PurchaseDate >= startOfMonth && item.PurchaseDate <= endOfMonth).ToList();

                // Count of items
                TotalSoldThisMonth = soldItemsThisMonth.Count;
                TotalPurchasedThisMonth = purchasedItemsThisMonth.Count;

                // Total amounts
                TotalAmountSoldThisMonth = soldItemsThisMonth.Sum(item => item.Price);
                TotalAmountPurchasedThisMonth = purchasedItemsThisMonth.Sum(item => item.Price);

                // Calculate profit (Sold Amount - Purchased Amount)
                ProfitThisMonth = TotalAmountSoldThisMonth - TotalAmountPurchasedThisMonth;
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
    }
}