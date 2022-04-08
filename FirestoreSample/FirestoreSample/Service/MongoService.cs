using FirestoreSample.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirestoreSample.Service
{
    public class MongoService
    {
        private MongoContext _context = new MongoContext();
        public List<ReadingInt> GetReadingInt(long lastId)
        {
            var list = _context.ReadingInt
            .Find(r => r.meteringPointId > lastId)
            .SortBy(e => e.meteringPointId)
            .Limit(10)
            .ToList();
            return list;
        }
    }
}
