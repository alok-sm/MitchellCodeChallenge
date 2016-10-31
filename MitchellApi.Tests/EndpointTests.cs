using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MitchellApi.Models;
using Newtonsoft.Json;

namespace MitchellApi.Tests
{
    /// <summary>
    /// Tests for the API endpoints
    /// </summary>
    [TestClass]
    public class EndpointTests
    {
        /// <summary>
        /// Tests add, get and delete functions
        /// </summary>
        [TestMethod]
        public void TestAddAndGetAndDelete()
        {
            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                VehicleModel prius = new VehicleModel
                {
                    Id = 1,
                    Make = "Toyota",
                    Model = "Prius",
                    Year = 2012
                };

                client.UploadString("http://localhost:62801/vehicles",
                    JsonConvert.SerializeObject(prius));

                string storedData = client.DownloadString("http://localhost:62801/vehicles/1");
                VehicleModel storedModel = JsonConvert.DeserializeObject<VehicleModel>(storedData);

                Assert.AreEqual(storedModel.Make, "Toyota");

                client.UploadValues("http://localhost:62801/vehicles/1", "DELETE", new NameValueCollection());
            }
        }

        /// <summary>
        /// tests inserting an object with a bad year (1900)
        /// </summary>
        [TestMethod]
        public void TestBadYear()
        {
            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                VehicleModel prius = new VehicleModel
                {
                    Id = 1,
                    Make = "Toyota",
                    Model = "Prius",
                    Year = 1900
                };

                try
                {
                    client.UploadString("http://localhost:62801/vehicles",
                    JsonConvert.SerializeObject(prius));
                }
                catch (WebException e)
                {
                    Assert.IsTrue(true);
                }
            }
        }

        /// <summary>
        /// Tests getting a list of all vehicles
        /// </summary>
        [TestMethod]
        public void TestList()
        {
            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                VehicleModel prius = new VehicleModel
                {
                    Id = 1,
                    Make = "Toyota",
                    Model = "Prius",
                    Year = 2012
                };

                client.UploadString("http://localhost:62801/vehicles",
                    JsonConvert.SerializeObject(prius));

                string storedData = client.DownloadString("http://localhost:62801/vehicles");
                List<VehicleModel> storedModel = JsonConvert.DeserializeObject<List<VehicleModel>>(storedData);

                Assert.AreEqual(storedModel[0].Make, "Toyota");

                client.UploadValues("http://localhost:62801/vehicles/1", "DELETE", new NameValueCollection());
            }
        }

        /// <summary>
        /// Tests deleting a non-existent object
        /// </summary>
        [TestMethod]
        public void TestBadDelete()
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    client.UploadValues("http://localhost:62801/vehicles/1111", "DELETE", new NameValueCollection());
                }
                catch (WebException e)
                {
                    Assert.IsTrue(true);
                }
            }
        }

        /// <summary>
        /// tests updating an object
        /// </summary>
        [TestMethod]
        public void TestUpdate()
        {
            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                VehicleModel prius = new VehicleModel
                {
                    Id = 1,
                    Make = "Toyota",
                    Model = "Prius",
                    Year = 2012
                };

                client.UploadString("http://localhost:62801/vehicles", JsonConvert.SerializeObject(prius));

                VehicleModel civic = new VehicleModel
                {
                    Id = 1,
                    Make = "Honda",
                    Model = "Civic",
                    Year = 2000
                };

                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                client.UploadString("http://localhost:62801/vehicles/1", "PUT", JsonConvert.SerializeObject(civic));

                string storedData = client.DownloadString("http://localhost:62801/vehicles");
                List<VehicleModel> storedModel = JsonConvert.DeserializeObject<List<VehicleModel>>(storedData);

                Assert.AreEqual(storedModel[0].Make, "Honda");

                client.UploadValues("http://localhost:62801/vehicles/1", "DELETE", new NameValueCollection());
            }
        }

        /// <summary>
        /// Tests updating a non-existent object
        /// </summary>
        [TestMethod]
        public void TestBadUpdate()
        {
            using (WebClient client = new WebClient())
            {
                VehicleModel civic = new VehicleModel
                {
                    Id = 1,
                    Make = "Honda",
                    Model = "Civic",
                    Year = 2000
                };

                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                try
                {
                    client.UploadString("http://localhost:62801/vehicles/1", "PUT", JsonConvert.SerializeObject(civic));
                }
                catch (WebException e)
                {
                    Assert.IsTrue(true);
                }
            }
        }
    }
}
