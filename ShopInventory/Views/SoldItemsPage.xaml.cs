using ShopInventory.ViewModels;

namespace ShopInventory.Views
{
    public partial class SoldItemsPage : ContentPage
    {
        public SoldItemsPage(SoldItemsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            if (BindingContext is SoldItemsViewModel viewModel)
            {
                await viewModel.LoadItems();
            }
        }
    }
}