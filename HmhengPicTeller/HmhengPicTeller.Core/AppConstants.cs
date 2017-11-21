using System;
using System.Collections.Generic;
using System.Text;

namespace HmhengPicTeller.Core
{
    public static class AppConstants
    {
        public const string OcpApimSubscriptionKey = "Ocp-Apim-Subscription-Key";
        /// <summary>
        /// Url of the Computer Vision API OCR method for printed text
        /// [language=en] Text in image is in English. 
        /// [detectOrientation=true] Improve results by detecting orientation
        /// </summary>
        public static string ComputerVisionApiDescriberUrl = "";


        public static void SetLocation(string location)
        {
            ComputerVisionApiDescriberUrl = $"https://{location}.api.cognitive.microsoft.com/vision/v1.0/analyze?visualFeatures=Description&language=en";
        }

        /// <summary>
        /// User's API Key for the Computer Vision API. Not a constant because it can get set in the app 
        /// if a user enters a key on the screen that allows key input.
        /// </summary>
		public static string ComputerVisionApiKey = "";
    }
}
