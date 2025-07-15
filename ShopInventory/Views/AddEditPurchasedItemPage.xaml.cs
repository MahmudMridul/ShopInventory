using ShopInventory.ViewModels;

namespace ShopInventory.Views
{
    [QueryProperty(nameof(ItemId), "ItemId")]
    public partial class AddEditPurchasedItemPage : ContentPage
    {
        public string ItemId { get; set; }

        public AddEditPurchasedItemPage(AddEditPurchasedItemViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            if (BindingContext is AddEditPurchasedItemViewModel viewModel)
            {
                // Parse ItemId from query property
                if (int.TryParse(ItemId, out int itemId))
                {
                    await viewModel.LoadItem(itemId);
                }
                else
                {
                    await viewModel.LoadItem(0); // New item
                }
            }
        }
    }
}