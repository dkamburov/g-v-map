namespace GVMap.Core.Repository.Markers
{
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using Models;
    using MongoDB.Bson;

    public interface IMarkerRepository
    {
        MarkerModel GetMarker(ObjectId id);

        List<MarkerModel> GetAllMarkers();

        void InsertMarker(MarkerModel model);

        void UpdateMarker(MarkerModel model);
    }
}
