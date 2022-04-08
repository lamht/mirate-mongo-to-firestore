using FirestoreSample.Model;
using Google.Cloud.Firestore;
using System.Threading.Tasks;

namespace FirestoreSample.Service
{
    public class FirestoreService
    {
        private static FirestoreDb db = FirestoreDb.Create(Setting.ProjectId);
        public async Task<long> GetLastMeterReadingId()
        {
            CollectionReference colRef = db.Collection(Setting.FirestorePrefix + "ReadingProcess");
            DocumentReference docRef = colRef.Document("1");

            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                FsReadingProcess readingProcess = snapshot.ConvertTo<FsReadingProcess>();
                return readingProcess.MeteringId;
            }
            else
            {
                return 0;
            }            
        }
        public async Task SetLastMeterReadingId(long id)
        {
            CollectionReference colRef = db.Collection(Setting.FirestorePrefix + "ReadingProcess");
            var data = new FsReadingProcess()
            {
                MeteringId = id
            };
            await colRef.Document("1").SetAsync(data, SetOptions.Overwrite);
        }

        public async Task AddReadingInt(FsReadingInfo documentData)
        {
            string ean = documentData.EAN;
            CollectionReference colRef = db.Collection(Setting.FirestorePrefix + "ReadingInt");
            string date = documentData.QueryDate.ToString("yyyyMMdd");
            await colRef.Document("INT").Collection(ean).Document(date)
                .SetAsync(documentData, SetOptions.Overwrite);
        }
    }
}
