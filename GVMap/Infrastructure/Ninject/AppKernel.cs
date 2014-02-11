namespace GVMap.Infrastructure.Ninject
{
    using global::Ninject;

    public class AppKernel
    {
        private static IKernel container;

        public static IKernel Container
        {
            get
            {
                if (container == null)
                {
                    container = new StandardKernel();
                }

                return container;
            }
        }
    }
}