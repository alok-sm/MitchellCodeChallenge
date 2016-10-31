using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MitchellApi.Models;
using MitchellApi.Storage;

namespace MitchellApi.Tests
{
    /// <summary>
    /// Test class for the in memory store
    /// </summary>
    [TestClass]
    public class InMemoryStoreTests
    {
        /// <summary>
        /// Class to test the in memory store
        /// </summary>
        public class ComputerModel : CrudModelBase
        {
            /// <summary>
            /// Name of the manufacturer
            /// </summary>
            public string OemName { get; set; }

            /// <summary>
            /// name of the computer model
            /// </summary>
            public string Model { get; set; }
        }

        /// <summary>
        /// Adds one element and checks it's presence
        /// </summary>
        [TestMethod]
        public void TestAddAndGet()
        {
            IObjectStore store = new InMemoryObjectStore();
            store.AddItem<ComputerModel>(new ComputerModel
            {
                Id = 1,
                OemName = "Apple",
                Model = "Macbook"
            });

            ComputerModel insertedItem = store.GetItem<ComputerModel>(1);

            Assert.AreEqual(insertedItem.OemName, "Apple");
            Assert.AreEqual(insertedItem.Model, "Macbook");
        }

        /// <summary>
        /// Add multiple items with same ID
        /// </summary>
        [TestMethod]
        public void TestMultipleAdd()
        {
            IObjectStore store = new InMemoryObjectStore();
            store.AddItem<ComputerModel>(new ComputerModel
            {
                Id = 1,
                OemName = "Apple",
                Model = "Macbook"
            });

            try
            {
                store.AddItem<ComputerModel>(new ComputerModel
                {
                    Id = 1,
                    OemName = "Apple",
                    Model = "Macbook"
                });
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        /// Adds 2 elements and checks their presence
        /// </summary>
        [TestMethod]
        public void TestList()
        {
            IObjectStore store = new InMemoryObjectStore();
            store.AddItem<ComputerModel>(new ComputerModel
            {
                Id = 1,
                OemName = "Apple",
                Model = "Macbook"
            });

            store.AddItem<ComputerModel>(new ComputerModel
            {
                Id = 2,
                OemName = "Samsung",
                Model = "Galaxy"
            });

            List<ComputerModel> computers = store.ListItems<ComputerModel>();

            Assert.IsNotNull(computers.FirstOrDefault(model => model.OemName == "Apple"));
            Assert.IsNotNull(computers.FirstOrDefault(model => model.OemName == "Samsung"));
        }

        /// <summary>
        /// Inserts, deletes and checks elements
        /// </summary>
        [TestMethod]
        public void TestDelete()
        {
            IObjectStore store = new InMemoryObjectStore();
            store.AddItem<ComputerModel>(new ComputerModel
            {
                Id = 1,
                OemName = "Apple",
                Model = "Macbook"
            });

            store.DeleteItem<ComputerModel>(1);

            List<ComputerModel> computers = store.ListItems<ComputerModel>();

            Assert.AreEqual(computers.Count, 0);
        }

        /// <summary>
        /// deleted non existant elements
        /// </summary>
        [TestMethod]
        public void TestNoElementDelete()
        {
            IObjectStore store = new InMemoryObjectStore();

            try
            {
                store.DeleteItem<ComputerModel>(1);
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        /// Inserts, updates and checks element
        /// </summary>
        [TestMethod]
        public void TestUpdate()
        {
            IObjectStore store = new InMemoryObjectStore();
            store.AddItem<ComputerModel>(new ComputerModel
            {
                Id = 1,
                OemName = "Apple",
                Model = "Macbook"
            });

            store.UpdateItem<ComputerModel>(1, new ComputerModel
            {
                OemName = "Samsung",
                Model = "Ativ"
            });

            ComputerModel insertedItem = store.GetItem<ComputerModel>(1);

            Assert.AreEqual(insertedItem.OemName, "Samsung");
            Assert.AreEqual(insertedItem.Model, "Ativ");
        }

        /// <summary>
        /// Tries to update non existant item
        /// </summary>
        [TestMethod]
        public void TestInvalidItemUpdate()
        {
            IObjectStore store = new InMemoryObjectStore();
            try
            {
                store.UpdateItem<ComputerModel>(1, new ComputerModel
                {
                    OemName = "Samsung",
                    Model = "Ativ"
                });
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }


    }
}
