using App.View;
namespace App;


public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(PersonDetailsView), typeof(PersonDetailsView));
        Routing.RegisterRoute(nameof(PeopleListView), typeof(PeopleListView));
        Routing.RegisterRoute(nameof(AddPersonPage), typeof(AddPersonPage));
        Routing.RegisterRoute(nameof(ProductsView), typeof(ProductsView));
        Routing.RegisterRoute(nameof(ProductDetailsView), typeof(ProductDetailsView));
        Routing.RegisterRoute(nameof(CartView), typeof(CartView));
        Routing.RegisterRoute(nameof(OrdersView), typeof(OrdersView));
        Routing.RegisterRoute(nameof(SettingsView), typeof(SettingsView));
        Routing.RegisterRoute(nameof(LoginView), typeof(LoginView));
        Routing.RegisterRoute(nameof(OrderDetailsView), typeof(OrderDetailsView));
        Routing.RegisterRoute(nameof(UserOrders), typeof(UserOrders));


    }
}

