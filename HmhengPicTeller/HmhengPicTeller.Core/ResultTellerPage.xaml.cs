using HmhengPicTeller.Core.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HmhengPicTeller.Core
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultTellerPage : ContentPage
    {
        HttpClient visionApiClient;
        byte[] photo;
        DescriptionResult values;

        public ResultTellerPage(byte[] photo)
        {
            InitializeComponent();
            this.photo = photo;
            visionApiClient = new HttpClient();
            visionApiClient.DefaultRequestHeaders.Add(AppConstants.OcpApimSubscriptionKey, AppConstants.ComputerVisionApiKey);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (values == null)
            {
                await LoadData();
            }
        }

        async Task LoadData()
        {
            // Try loading the results, show error message if necessary.
            Boolean error = false;
            try
            {
                values = await FetchDescription();
            }
            catch
            {
                error = true;
            }

            // Hide the spinner, show the table
            LoadingIndicator.IsVisible = false;
            LoadingIndicator.IsRunning = false;
            lblDescription.IsVisible = true;

            if (error)
            {
                await ErrorAndPop("Error", "Error fetching description", "OK");
            }
            else if (values != null)
            {
                lblDescription.Text = values.description.captions[0].text;
            }
            else
            {
                await ErrorAndPop("Error", "No description found", "OK"); ;
            }
        }

        async Task<DescriptionResult> FetchDescription()
        {
            DescriptionResult descriptionResult = new DescriptionResult();
            if (photo != null)
            {
                HttpResponseMessage response = null;
                using (var content = new ByteArrayContent(photo))
                {
                    // The media type of the body sent to the API. 
                    // "application/octet-stream" defines an image represented 
                    // as a byte array
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    response = await visionApiClient.PostAsync(AppConstants.ComputerVisionApiDescriberUrl, content);
                }

                string ResponseString = await response.Content.ReadAsStringAsync();

                DescriptionResult _result = JsonConvert.DeserializeObject<DescriptionResult>(ResponseString);

                if (_result != null)
                {
                    descriptionResult = _result;
                }
            }
            return descriptionResult;
        }

        /// <summary>
        /// Shows an error message, navigates back after it is dismissed.
        /// </summary>
		protected async Task ErrorAndPop(string title, string text, string button)
        {
            await DisplayAlert(title, text, button);
            Console.WriteLine($"ERROR: {text}");
            await Task.Delay(TimeSpan.FromSeconds(0.1d));
            await Navigation.PopAsync(true);
        }
        
    }
}