using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirestoreSample.Model
{
    [FirestoreData]
    public class FsRegister
    {
        [FirestoreProperty]
        public string RegisterId { get; set; }
        [FirestoreProperty]
        public string MeasureUnit { get; set; }
        [FirestoreProperty]
        public float Reading { get; set; }
        [FirestoreProperty]
        public DateTime ReadingDatetime { get; set; }
    }
}
