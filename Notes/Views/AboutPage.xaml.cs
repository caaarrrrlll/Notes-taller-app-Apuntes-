namespace Notes.Views;

public partial class AboutPage : ContentPage
{
	public AboutPage()
	{
		InitializeComponent();
	}

    private async void Videojuegofavorita_Clicked(object sender, EventArgs e)
    {
        var uri = new Uri("https://deathstranding.fandom.com/wiki/Death_Stranding");
        await Launcher.Default.OpenAsync(uri);
    }

    private async void Videojuegofavoritapatt_Clicked(object sender, EventArgs e)
    {
        var uri = new Uri("https://discoelysium.fandom.com/wiki/Disco_Elysium_Wiki");
        await Launcher.Default.OpenAsync(uri);
    }

    private async void Videojuegofavoritapserg_Clicked(object sender, EventArgs e)
    {
        var uri = new Uri("https://megamitensei.fandom.com/wiki/Persona_5");
        await Launcher.Default.OpenAsync(uri);
    }

    private async void U_Clicked(object sender, EventArgs e)
    {
        var uri = new Uri("https://www.udla.edu.ec/");
        await Launcher.Default.OpenAsync(uri);

    }
}