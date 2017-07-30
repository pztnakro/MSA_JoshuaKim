using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using Xamarin.Forms;

using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;
using System.Linq;
using MsaJoshuaKim.Models;
using System.Threading.Tasks;

namespace MsaJoshuaKim.Views
{
    public partial class TakePhoto : ContentPage
    {
        String aResult;
        public Command AddItemsCommand { get; set; }

        private AzureManager azureManager;

        //Emotion Server setup
        const String KEY = "d5bc3bf8214d46deb0576babe0802fb9";
        EmotionServiceClient emotionServiceClient = new EmotionServiceClient(KEY);

        public TakePhoto()
        {
            InitializeComponent();
            azureManager = new AzureManager();
        }

        async void Save_Photo(object sender, EventArgs e)
        {
            await StoreFace();
            return;
        }


        private async Task StoreFace()
        {
            String NewEmotion = aResult;
            if (!String.IsNullOrEmpty(NewEmotion))
            {
                Analyze_faces analyzeEmotion = new Analyze_faces();

                try
                {
                    analyzeEmotion.analyzeFace = NewEmotion;
                    analyzeEmotion.storeDate = DateTime.UtcNow;

                    analyzeEmotion = await azureManager.TakeEmotion(analyzeEmotion);
                    await DisplayAlert("Great!", "Your Emotion has been successfully analyzed.", "OK");
                    return;

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    IsBusy = false;
                }
            }
            else
            {
                await DisplayAlert("Error", "Please take a photo!", "OK");
                return;
            }
        }


        private async void TakePhoto_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Camera is not working", "There is No camera available.", "OK");
                return;
            }

            MediaFile emotionImage = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Medium,
                Directory = "Sample",
                Name = $"{DateTime.UtcNow}.jpg"
            });

            //checks the image is null or not
            if (emotionImage == null)
                return;

            //display the image
            tPhoto.Source = ImageSource.FromStream(() =>
            {
                return emotionImage.GetStream();
            });

            //connects to server and get emotion as string.
            using (var photoStream = emotionImage.GetStream())
            {
                Emotion[] emotionResult = await emotionServiceClient.RecognizeAsync(photoStream);
                if (emotionResult.Any())
                {
                    // Emotions detected are happiness, sadness, surprise, anger, fear, contempt, disgust, or neutral.
                    analyzeFace.Text = emotionResult.FirstOrDefault().Scores.ToRankedList().FirstOrDefault().Key;
                    aResult = analyzeFace.Text;
                }
            }
            emotionImage.Dispose();
        }
    }
}
