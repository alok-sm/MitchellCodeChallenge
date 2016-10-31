using System.Net;
using System.Net.Http;
using MitchellApi.Models;

namespace MitchellApi.Controllers
{
    /// <summary>
    /// Implementation of the CrudControllerBase for storing vehicles.
    /// </summary>
    public class VehiclesController : CrudControllerBase<VehicleModel>
    {
        /// <summary>
        /// Validates the model
        /// </summary>
        /// <param name="model">the model to be validated</param>
        /// <returns>If the models are invalid, returns an appropriate HttpResponseMessage. If valid, returns null</returns>
        private HttpResponseMessage ValidateModel(VehicleModel model)
        {
            if (model.Year < 1950 || model.Year > 2050)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Year must be between 1950 and 2050");
            }

            if (
                model.Id == default(int) ||
                model.Make == default(string) ||
                model.Model == default(string))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Mandatory parameters not sent");
            }

            return null;
        }

        /// <summary>
        /// Overriding the base Post function in order to add year of make validation
        /// Also validates presence of all necessary params
        /// </summary>
        /// <param name="model">the VehicleModel object sent in the request</param>
        /// <returns>The HTTP response message</returns>
        public override HttpResponseMessage Post(VehicleModel model)
        {
            return ValidateModel(model) ?? base.Post(model);
        }

        /// <summary>
        /// Overriding the base Post function in order to add year of make validation
        /// Also validates presence of all necessary params
        /// </summary>
        /// <param name="id">ID of the object to be updated</param>
        /// <param name="model">the VehicleModel object sent in the request</param>
        /// <returns>The HTTP response message</returns>
        public override HttpResponseMessage Put(int id, VehicleModel model)
        {
            return ValidateModel(model) ?? base.Put(model);
        }
    }
}
