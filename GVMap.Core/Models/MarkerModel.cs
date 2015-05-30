using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace GVMap.Models
{
    public class MarkerModel
    {
        public ObjectId Id { get; set; }

        public string Coordinates { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        public string User { get; set; }
    }
}