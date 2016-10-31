using System.Collections.Generic;
using MitchellApi.Models;

namespace MitchellApi.Storage
{
    /// <summary>
    /// Interface for the object store. 
    /// </summary>
    public interface IObjectStore
    {
        /// <summary>
        /// List all TCrudModel items present
        /// </summary>
        /// <typeparam name="TCrudModel">Type of model</typeparam>
        /// <returns>list of all TCrudModel objects</returns>
        List<TCrudModel> ListItems<TCrudModel>() where TCrudModel : CrudModelBase;

        /// <summary>
        /// Gets TCrudModel object with ID
        /// </summary>
        /// <typeparam name="TCrudModel"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        TCrudModel GetItem<TCrudModel>(int id) where TCrudModel : CrudModelBase;

        /// <summary>
        /// Creates a new TCrudModel entry in the storage
        /// </summary>
        /// <typeparam name="TCrudModel">Type of model</typeparam>
        /// <param name="model"></param>
        void AddItem<TCrudModel>(TCrudModel model) where TCrudModel : CrudModelBase;

        /// <summary>
        /// Updates a TCrudModel object in storage with ID to the new object
        /// </summary>
        /// <typeparam name="TCrudModel">Type of model</typeparam>
        /// <param name="id">ID of the object to be updated</param>
        /// <param name="newModel">new model that replaces the old one</param>
        void UpdateItem<TCrudModel>(int id, TCrudModel newModel) where TCrudModel : CrudModelBase;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCrudModel">Type of model</typeparam>
        /// <param name="id"></param>
        void DeleteItem<TCrudModel>(int id) where TCrudModel : CrudModelBase;
    }
}