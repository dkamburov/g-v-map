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

        public string Text { get; set; }

        public string ImageUrl { get; set; }
    }
}