using ShopInventory.ViewModels;

namespace ShopInventory.Views;

public partial class PurchasedItemsPage : ContentPage
{
	public PurchasedItemsPage(PurchasedItemsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		
		if (BindingContext is PurchasedItemsViewModel viewModel)
		{
			await viewModel.LoadItems();
		}
	}
}