using ShopInventory.Views;

namespace ShopInventory
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register routes for navigation
            Routing.RegisterRoute(nameof(PurchasedItemsPage), typeof(PurchasedItemsPage));
            Routing.RegisterRoute(nameof(SoldItemsPage), typeof(SoldItemsPage));
            Routing.RegisterRoute(nameof(AddEditPurchasedItemPage), typeof(AddEditPurchasedItemPage));
            Routing.RegisterRoute(nameof(AddEditSoldItemPage), typeof(AddEditSoldItemPage));
        }
    }
}