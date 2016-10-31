using Microsoft.Practices.Unity;
using MitchellApi.Models;
using MitchellApi.Storage;

namespace MitchellApi.Unity
{
    internal class Container
    {
        private static IUnityContainer _instance;
        private static readonly object _syncRoot = new object();

        internal static IUnityContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                        {
                            InitializeContainer();
                        }
                    }
                }

                return _instance;
            }
        }

        private static void InitializeContainer()
        {
            _instance = new UnityContainer();
            _instance.RegisterInstance<IObjectStore>(new InMemoryObjectStore());
        }
    }
}