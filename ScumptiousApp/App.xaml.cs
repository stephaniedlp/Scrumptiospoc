using Application = Microsoft.Maui.Controls.Application;

namespace ScrumptiousApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Check if there's a saved URL in Preferences
            var lastUrl = Preferences.Get("LastOpenedUrl", string.Empty);

            // Always start with BarcodePage as the root of the navigation stack
            if (!string.IsNullOrEmpty(lastUrl))
            {
                MainPage = new NavigationPage(new BarcodePage());
                MainPage.Navigation.PushAsync(new WebViewPage(lastUrl));
            }
            else
            {
                MainPage = new NavigationPage(new BarcodePage());
            }
        }
    }


}
