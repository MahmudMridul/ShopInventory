using ShopInventory.ViewModels;

namespace ShopInventory.Views
{
    public partial class DashboardPage : ContentPage
    {
        public DashboardPage(DashboardViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            if (BindingContext is DashboardViewModel viewModel)
            {
                await viewModel.LoadDashboardData();
            }
        }
    }
}