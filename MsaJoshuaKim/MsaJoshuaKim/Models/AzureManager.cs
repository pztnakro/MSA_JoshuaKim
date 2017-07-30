using System.Collections.Generic;
using System.Threading.Tasks;
using MsaJoshuaKim.Models;
using Microsoft.WindowsAzure.MobileServices;

namespace MsaJoshuaKim
{
    public class AzureManager
    {

        private static AzureManager instance;
        private MobileServiceClient mServiceClient;
        private IMobileServiceTable<Analyze_faces> EdatabaseTable;

        public AzureManager()
        {
            this.mServiceClient = new MobileServiceClient("http://msajoshua.azurewebsites.net/");
            this.EdatabaseTable = this.mServiceClient.GetTable<Analyze_faces>();
        }

        public MobileServiceClient AzureClient
        {
            get { return mServiceClient; }
        }

        public static AzureManager AzureManagerInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AzureManager();
                }

                return instance;
            }
        }

        public async Task<List<Analyze_faces>> AnalyzeFaces()
        {

            List<Analyze_faces> AnalyzeMotion = new List<Analyze_faces>();

            AnalyzeMotion = await EdatabaseTable.ToListAsync();

            return AnalyzeMotion;

        }

        public async Task<Analyze_faces> TakeEmotion(Analyze_faces Afaces)
        {

            await EdatabaseTable.InsertAsync(Afaces);

            return Afaces;

        }

    }
}