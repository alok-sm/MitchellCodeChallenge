using Microsoft.Practices.Unity;
using MitchellApi.Models;
using MitchellApi.Storage;

namespace MitchellApi.Unity
{
    /// <summary>
    /// Unity Container class
    /// </summary>
    internal class Container
    {
        /// <summary>
        /// internal instance of the unity container
        /// </summary>
        private static IUnityContainer _instance;

        /// <summary>
        /// Used to lock while initializing the unity container
        /// </summary>
        private static readonly object _syncRoot = new object();

        /// <summary>
        /// returns the instance of the unity container
        /// </summary>
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

        /// <summary>
        /// Initializes the unity container
        /// </summary>
        private static void InitializeContainer()
        {
            _instance = new UnityContainer();
            _instance.RegisterInstance<IObjectStore>(new InMemoryObjectStore());
        }
    }
}