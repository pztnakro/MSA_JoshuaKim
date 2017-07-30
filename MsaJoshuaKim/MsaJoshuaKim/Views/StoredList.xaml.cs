using MsaJoshuaKim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MsaJoshuaKim
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoredList : ContentPage
    {

        public StoredList()
        {
            InitializeComponent();
        }

        async void Retrieve_Data(object sender, System.EventArgs e)
        {
            List<Analyze_faces> aFacesList = await AzureManager.AzureManagerInstance.AnalyzeFaces();

            FaceList.ItemsSource = aFacesList;
        }

    }
}