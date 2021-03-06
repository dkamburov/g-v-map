﻿using GVMap.Core.Repository.Markers;
using GVMap.Repository.Repository.Markers;

namespace GVMap.Infrastructure.Ninject
{
    using GVMap.Models;
    using global::Ninject.Activation;
    using global::Ninject.Modules;
    using MongoDB.Driver;
    using GVMap.Infrastructure.Ninject.Providers;

    public class AppModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMarkerRepository>().To<MarkerRepository>();
            Bind<MongoCollection<MarkerModel>>().ToProvider(new MarkerCollectionProvider()).InSingletonScope();
        }
    }
}