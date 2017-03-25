using Logger;
using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Webjob
{
    public class Fuctions
    {
        /// <summary>
        /// INJECT SERVICES HERE
        /// </summary>
        public Fuctions()
        {

        }

        /// <summary>
        /// Log Error in any way you want :: TO EMAIL, AZURE TABLE, TEXT FILE... etc.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public async Task ProcessQueueMessage([QueueTrigger("%Azure:Webjobs:Log%")] ErrorLogViewModel errorLogViewModel, TextWriter log)
        {
            //
            log.WriteLine($"Just trying out the logger {errorLogViewModel}");

        }

    }
}
