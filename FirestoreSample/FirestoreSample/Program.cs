using FirestoreSample.Model;
using FirestoreSample.Service;
using Google.Cloud.Firestore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FirestoreSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start!");
           
            bool hasData = true;
            while(hasData)
            {
                hasData = Process().GetAwaiter().GetResult();
                Thread.Sleep(1);
            }

            Console.WriteLine("End.");

        }

        private static async Task<bool> Process()
        {
            MongoService mongoSevice = new MongoService();
            FirestoreService firestoreService = new FirestoreService();
            long lastMeteringId = await firestoreService.GetLastMeterReadingId();
            List<ReadingInt> mgData = mongoSevice.GetReadingInt(lastMeteringId);
            if (mgData == null || mgData.Count == 0)
            {
                return false;
            }
            long maxId = mgData.Max(m => m.meteringPointId);
            List<FsReadingInfo> fsData = ModelMapping.MgToFs(mgData);
            List<Task> tasks = new List<Task>();
            foreach (FsReadingInfo fsReadingInfo in fsData)
            {
                var task = firestoreService.AddReadingInt(fsReadingInfo);
                tasks.Add(task);
            }
            Task.WaitAll(tasks.ToArray());
            await firestoreService.SetLastMeterReadingId(maxId);
            Console.WriteLine($"Process {maxId}");
            return true;
        }

    }
}
