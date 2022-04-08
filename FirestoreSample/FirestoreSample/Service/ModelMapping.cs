using FirestoreSample.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirestoreSample.Service
{
    internal class ModelMapping
    {
        public static string datePatt = @"M/d/yyyy hh:mm:ss tt";
        public static List<FsReadingInfo> MgToFs(List<ReadingInt> from)
        {
            List<FsReadingInfo> to = new List<FsReadingInfo>();
            foreach(var f in from)
            {
                to.Add(MgToFs(f));
            }
            return to;
        }
        //maping model from mongodb to firestore
        public static FsReadingInfo MgToFs(ReadingInt from)
        {
            var to = new FsReadingInfo();
            to.EAN = from.eanId;
            to.EnergyMeterId = from.energyMeterId;
            to.ExternalReference = from.externalReference;
            to.QueryDate = from.queryDate;
            to.QueryReason = from.queryReason;
            to.CreatedAt = DateTime.SpecifyKind(from.createdAt, DateTimeKind.Utc);
            to.UpdatedAt = DateTime.SpecifyKind(from.updatedAt, DateTimeKind.Utc);
            List<FsRegister> readings = JsonConvert.DeserializeObject<List<FsRegister>>(from.readings);
            foreach(var reading in readings)
            {
                reading.ReadingDatetime = reading.ReadingDatetime.ToUniversalTime();// DateTime.SpecifyKind(reading.ReadingDatetime, DateTimeKind.Local);
            }
            //DisplayNow($"UTC {to.EAN}, {to.QueryDate}", readings[0].ReadingDatetime);
            to.Readings = readings.OrderBy(x =>x.RegisterId).ThenBy(x => x.ReadingDatetime).ToList();
            return to;
        }

        public static void DisplayNow(string title, DateTime inputDt)
        {
            string dtString = inputDt.ToString(datePatt);
            Console.WriteLine("{0} {1}, Kind = {2}",
                              title, dtString, inputDt.Kind);
        }
    }
}
