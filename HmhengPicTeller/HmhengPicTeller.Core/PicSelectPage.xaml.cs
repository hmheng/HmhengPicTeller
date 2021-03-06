﻿using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HmhengPicTeller.Core
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PicSelectPage : ContentPage
	{
		public PicSelectPage ()
		{
			InitializeComponent ();
            CrossMedia.Current.Initialize();
        }


        /// <summary>
        /// Called when Take Photo is pressed.
        /// </summary>
        async void TakePhotoButtonClickEventHandler(object sender, EventArgs e)
        {
            byte[] photoByteArray = null;

            try
            {
                photoByteArray = await TakePhoto();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }

            if (photoByteArray != null)
            {
                await Navigation.PushAsync(new ResultTellerPage(photoByteArray));
            }
        }

        /// <summary>
        /// Uses the Xamarin Media Plugin to import photos from the native photo library
        /// </summary>
        async void ImportPhotoButtonClickEventHandler(object sender, EventArgs e)
        {
            Boolean error = false;
            MediaFile photoMediaFile = null;
            byte[] photoByteArray = null;

            try
            {
                photoMediaFile = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium,
                });
                photoByteArray = MediaFileToByteArray(photoMediaFile);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"ERROR: {exception.Message}");
                error = true;
            }

            if (error)
            {
                await DisplayAlert("Error", "Error taking photo", "OK");
            }
            else if (photoByteArray != null)
            {
                await Navigation.PushAsync(new ResultTellerPage(photoByteArray));
            }
        }

        /// <summary>
        /// Uses the Xamarin Media Plugin to take photos using the native camera 
        /// application
        /// </summary>
        async Task<byte[]> TakePhoto()
        {
            MediaFile photoMediaFile = null;
            byte[] photoByteArray = null;

            if (CrossMedia.Current.IsCameraAvailable)
            {
                var mediaOptions = new StoreCameraMediaOptions
                {
                    PhotoSize = PhotoSize.Medium,
                    AllowCropping = true,
                    SaveToAlbum = true,
                    Name = $"{DateTime.UtcNow}.jpg"
                };
                photoMediaFile = await CrossMedia.Current.TakePhotoAsync(mediaOptions);
                photoByteArray = MediaFileToByteArray(photoMediaFile);
            }
            else
            {
                await DisplayAlert("Error", "No camera found", "OK");
                Console.WriteLine($"ERROR: No camera found");
            }
            return photoByteArray;
        }

        /// <summary>
        /// Convert the media file to a byte array.
        /// </summary>
		byte[] MediaFileToByteArray(MediaFile photoMediaFile)
        {
            using (var memStream = new MemoryStream())
            {
                photoMediaFile.GetStream().CopyTo(memStream);
                return memStream.ToArray();
            }
        }
    }
}