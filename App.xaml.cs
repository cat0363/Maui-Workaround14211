namespace Maui_Workaround14211;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new NavigationPage(new Page2());
		// MainPage = new AppShell();
	}
}
