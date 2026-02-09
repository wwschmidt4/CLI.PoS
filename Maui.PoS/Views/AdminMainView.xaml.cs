using CLI.PoS.Model;
using Library.PoS.Services;

namespace Maui.PoS.Views;

public partial class AdminMainView : ContentPage
{
	public List<Item> Items
	{
		get
		{
			return ItemServiceProxy.Current.Items;
		}
	}
	public AdminMainView()
	{
		InitializeComponent();
		BindingContext = this;
	}

    private void GoBackClicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("//MainPage");
    }
}