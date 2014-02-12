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

        public void InsertMarker(MarkerModel model)
        {            
            markerCollection.Insert(model);
        }

        public void UpdateMarker(MarkerModel model)
        {
            var query = Query<MarkerModel>.EQ(u => u.Id, model.Id);

            var update = Update.Set("Description", model.Description).Set("Image", model.Image).Set("Coordinates", model.Coordinates);

            markerCollection.Update(query, update);
        }
    }
}
