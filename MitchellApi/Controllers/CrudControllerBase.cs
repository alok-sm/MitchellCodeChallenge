using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MitchellApi.Models;
using MitchellApi.Storage;
using MitchellApi.Unity;
using Microsoft.Practices.Unity;

namespace MitchellApi.Controllers
{
    /// <summary>
    /// Base controller for any CRUD endpoint. Saves time writing multiple CRUD classes if more needed in the future.
    /// </summary>
    /// <typeparam name="TCrudModel">The Class of the object whose CRUD endpoint this is</typeparam>
    public abstract class CrudControllerBase<TCrudModel> : ApiController where TCrudModel : CrudModelBase
    {
        private IObjectStore ObjectStore => Container.Instance.Resolve<IObjectStore>();

        /// <summary>
        /// Get request with no ID. i.e, in index() call
        /// </summary>
        /// <returns>List of TCrudModel objects or error message</returns>
        public virtual HttpResponseMessage Get()
        {
            try
            {
                List<TCrudModel> list = ObjectStore.ListItems<TCrudModel>();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch (Exception err)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, err.Message);
            }
        }

        /// <summary>
        /// GET request with ID, i.e gets the obj with id
        /// </summary>
        /// <param name="id">id of the object</param>
        /// <returns>TCrudModel object of error message</returns>
        public virtual HttpResponseMessage Get(int id)
        {
            try
            {
                TCrudModel result = ObjectStore.GetItem<TCrudModel>(id);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception err)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, err.Message);
            }
        }

        /// <summary>
        /// Function to create a new TCrudModel  object
        /// </summary>
        /// <param name="model">the TCrudModel object</param>
        /// <returns>HTTP message to show success or failure</returns>
        public virtual HttpResponseMessage Post([FromBody] TCrudModel model)
        {
            try
            {
                ObjectStore.AddItem<TCrudModel>(model);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception err)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, err.Message);
            }
        }

        /// <summary>
        /// Function to update a TCrudModel onject with "id"
        /// </summary>
        /// <param name="id">the id of the object to be updated</param>
        /// <param name="model">the updated object</param>
        /// <returns>HTTP message to show success or failure</returns>
        public virtual HttpResponseMessage Put(int id, [FromBody] TCrudModel model)
        {
            try
            {
                ObjectStore.UpdateItem<TCrudModel>(id, model);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception err)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, err.Message);
            }
        }

        /// <summary>
        /// Function to delete a TCrudModel object with "id"
        /// </summary>
        /// <param name="id">id of the object to be deleted</param>
        /// <returns>HTTP message to show success or failure</returns>
        public virtual HttpResponseMessage Delete(int id)
        {
            try
            {
                ObjectStore.DeleteItem<TCrudModel>(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception err)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, err.Message);
            }
        }
    }
}