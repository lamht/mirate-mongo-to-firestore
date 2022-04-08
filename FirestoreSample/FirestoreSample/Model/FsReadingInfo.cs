using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirestoreSample.Model
{
    [FirestoreData]
    public class FsReadingInfo
    {
        [FirestoreProperty]
        public string EAN { get; set; }
        [FirestoreProperty]
        public string ExternalReference { get; set; }
        [FirestoreProperty]
        public DateTime QueryDate { get; set; }
        [FirestoreProperty]
        public string QueryReason { get; set; }
        [FirestoreProperty]
        public string EnergyMeterId { get; set; }
        [FirestoreProperty]
        public DateTime CreatedAt { get; set; }
        [FirestoreProperty]
        public DateTime UpdatedAt { get; set; }

        [FirestoreProperty]
        public List<FsRegister> Readings { get; set; }
    }
}
