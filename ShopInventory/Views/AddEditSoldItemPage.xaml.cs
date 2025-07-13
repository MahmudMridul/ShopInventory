using ShopInventory.ViewModels;

namespace ShopInventory.Views
{
    public partial class AddEditSoldItemPage : ContentPage
    {
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
                // Get ItemId from query parameters if editing
                if (int.TryParse(GetQueryParameter("ItemId"), out int itemId))
                {
                    await viewModel.LoadItem(itemId);
                }
                else
                {
                    await viewModel.LoadItem(0); // New item
                }
            }
        }

        private string GetQueryParameter(string key)
        {
            var query = Shell.Current.CurrentState.Location.OriginalString;
            var queryParams = query.Split('?', '&');
            
            foreach (var param in queryParams)
            {
                if (param.StartsWith($"{key}="))
                {
                    return param.Substring(key.Length + 1);
                }
            }
            return null;
        }
    }
}