using Logger;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client
{
    public class CustomErrorLogger : ICustomErrorLogger
    {
        private CloudQueue logQueue;
        private CloudQueueClient queueClient;

        public CustomErrorLogger()
        {
            var storageAccount = CloudStorageAccount.Parse(Program.Configuration["Azure:ConnectionString"]);

            var blobClient = storageAccount.CreateCloudBlobClient();
            queueClient = storageAccount.CreateCloudQueueClient();


            logQueue = queueClient.GetQueueReference(Program.Configuration["Azure:Webjobs:Log"]);
            logQueue.CreateIfNotExists();
        }
        

        public async Task ErrorLogger(ErrorLogViewModel errorLogViewModel)
        {
            var queueMessage = new CloudQueueMessage(JsonConvert.SerializeObject(errorLogViewModel));
            await logQueue.AddMessageAsync(queueMessage);
        }
    }
}
