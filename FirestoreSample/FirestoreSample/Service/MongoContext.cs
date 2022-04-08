using FirestoreSample.Model;
using MongoDB.Driver;
using Polly;
using Polly.Retry;
using System;

namespace FirestoreSample.Service
{
    public class MongoContext
    {
        private IMongoDatabase _database = null;

        public MongoContext()
        {
            var _retryCount = 5;
            var policy = RetryPolicy.Handle<Exception>()
                    .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                    {
                        Console.WriteLine($"Can not init Mongo database after {time.TotalSeconds}s ({ex.Message})");
                    }
                );

            policy.Execute(() =>
            {
                string mongoConnection = Setting.MongoDbConnectionString;
                MongoClientSettings mongoSettings =
                MongoClientSettings.FromUrl(new MongoUrl(mongoConnection));
                
                mongoSettings.WriteConcern = WriteConcern.WMajority;
                var client = new MongoClient(mongoSettings);

                this._database = client.GetDatabase(Setting.MongoDb);
                if (this._database == null)
                {
                    throw new Exception("Can not init Mongo database");
                }

            });
            //Console.WriteLine("======= Init MongoContext");
        }


        public IMongoCollection<ReadingInt> ReadingInt
        {
            get
            {
                return _database.GetCollection<ReadingInt>("ReadingInt");
            }
        }
    }
}
