using ShopInventory.ViewModels;

namespace ShopInventory.Views
{
    [QueryProperty(nameof(ItemId), "ItemId")]
    public partial class AddEditSoldItemPage : ContentPage
    {
        public string ItemId { get; set; } = null!;

        public AddEditSoldItemPage(AddEditSoldItemViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            if (BindingContext is AddEditSoldItemViewModel viewModel)
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

        private void OnEntryFocused(object sender, FocusEventArgs e)
        {
            if (BindingContext is AddEditSoldItemViewModel viewModel)
            {
                viewModel.HideSuggestionsCommand.Execute(null);
            }
        }
    }
}