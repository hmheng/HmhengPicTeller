using HmhengPicTeller.Core;

using Xamarin.Forms;

namespace HmhengPicTeller
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            AppConstants.ComputerVisionApiKey = "<YOUR_API_KEY_HERE>";

            //Applicable Computer Vision locations (at time of writing) are: westus, eastus2, westcentralus, westeurope, southeastasia 
            AppConstants.SetLocation("<YOUR_SELECTED_REGION_HERE>");

            MainPage = new NavigationPage(new PicSelectPage());
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
