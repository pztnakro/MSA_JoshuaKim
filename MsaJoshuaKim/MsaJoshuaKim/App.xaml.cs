using MsaJoshuaKim.Views;
using Xamarin.Forms;

namespace MsaJoshuaKim
{
    // Joshua Taehyun Kim (Auckland University of Technology)
    // Application summary
    // Taking a photo and recognize emotions in images 
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            InitMainPage();
        }

        public static void InitMainPage()
        {
            Current.MainPage = new TabbedPage
            {
                Children =
                {
                    new NavigationPage(new TakePhoto())
                    {
                        Title = "Analyze Faces!",
                        Icon = Device.OnPlatform<string>(null,null,null)                        
                    },
                    new NavigationPage(new StoredList())
                    {
                        Title = "Stored History",
                        Icon = Device.OnPlatform<string>(null,null,null)
                    }
                }
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
