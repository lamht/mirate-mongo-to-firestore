using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirestoreSample.Model
{

    [FirestoreData]
    public class FsReadingProcess
    {
        [FirestoreProperty]
        public long MeteringId { get; set; }
        
    }
}
