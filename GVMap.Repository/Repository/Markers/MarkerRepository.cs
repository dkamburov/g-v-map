namespace GVMap.Repository.Repository.Markers
{
    using System.Collections.Generic;
    using System.Linq;
    using GVMap.Models;
    using global::MongoDB.Bson;
    using global::MongoDB.Driver;
    using global::MongoDB.Driver.Builders;
    using GVMap.Core.Repository.Markers;

    public class MarkerRepository : IMarkerRepository
    {
        private MongoCollection<MarkerModel> markerCollection;

        public MarkerRepository(MongoCollection<MarkerModel> markerCollection)
        {
            this.markerCollection = markerCollection;
        }

        public MarkerModel GetMarker(ObjectId id)
        {
            var query = Query<MarkerModel>.EQ(u => u.Id, id);

            return markerCollection.FindOne(query);
        }

        public List<MarkerModel> GetAllMarkers()
        {
            return markerCollection.FindAll().ToList();
        }

        public void InsertMarker(string text, string imageUrl)
        {
            MarkerModel model = new MarkerModel
                                        {
                                            //Id = new ObjectId(),
                                            Text = text,
                                            ImageUrl = imageUrl
                                        };
            markerCollection.Insert(model);
        }

        public void UpdateMarker(ObjectId id, string text, string imageUrl)
        {
            var query = Query<MarkerModel>.EQ(u => u.Id, id);

            //var update = new UpdateDocument {
            //            { "$set", "Text" = text, "ImageUrl" = imageUrl }
            //                            };
            var update = Update.Set("Text", text).Set("ImageUrl", imageUrl);

            markerCollection.Update(query, update);
        }
    }
}
