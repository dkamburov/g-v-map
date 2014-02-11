namespace GVMap.Infrastructure.Ninject
 using System.Reflection;
    using Core.Infrastructure.Logging.Interfaces;
    using Core.Infrastructure.Membership.Identity.Implementation;
    using Core.Infrastructure.Membership.Identity.Interface;
    using Core.Models;
    using Core.Repository;
    using Repository.MongoDB.Repository.Newsfeed;
    using global::Ninject.Activation;
    using global::Ninject.Modules;
    using Logging.Implementation;
    using MongoDB.Driver;
    using Providers;
    using Repository.MongoDB.Repository.Account;

    public class AppModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogger>().To<NLogLogger>().WithConstructorArgument("currentClassName", GetCurrentClassName);
            Bind<ICustomPrincipal>().To<CustomPrincipal>();

            Bind<IAccountRepository>().To<AccountRepository>().InSingletonScope();
            Bind<MongoCollection<UserPropertiesModel>>().ToProvider(new UserPropertiesCollectionProvider()).InSingletonScope();


            Bind<INewsFeedRepository>().To<NewsFeedRepository>().InSingletonScope();
            Bind<MongoCollection<NewsFeedModel>>().ToProvider(new NewsFeedCollectionProvider()).InSingletonScope();
        }

        private static object GetCurrentClassName(IContext r)
        {
            if (r.Request.ParentContext == null)
                return Assembly.GetCallingAssembly().GetName().Name; //"Mockingbird";
            else
                return r.Request.ParentContext.Request.Service.FullName;
        }
    }