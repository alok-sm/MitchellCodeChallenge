using System;
using System.Collections.Generic;
using System.Linq;
using MitchellApi.Models;

namespace MitchellApi.Storage
{
    /// <summary>
    /// Implementation of the IObjectStore which stored objects on memory
    /// </summary>
    public class InMemoryObjectStore : IObjectStore
    {
        /// <summary>
        /// Constructor which creates a new InMemoryObjectStore class
        /// </summary>
        public InMemoryObjectStore()
        {
            _storage = new Dictionary<Type, List<CrudModelBase>>();
        }

        /// <summary>
        /// Storage area of the objects
        /// </summary>
        private readonly Dictionary<Type, List<CrudModelBase>> _storage;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCrudModel"></typeparam>
        /// <returns></returns>
        public List<TCrudModel> ListItems<TCrudModel>() where TCrudModel : CrudModelBase
        {
            if (!_storage.ContainsKey(typeof(TCrudModel)))
            {
                _storage[typeof(TCrudModel)] = new List<CrudModelBase>();
            }
            return _storage[typeof(TCrudModel)].Cast<TCrudModel>().ToList();
        }

        public TCrudModel GetItem<TCrudModel>(int id) where TCrudModel : CrudModelBase
        {
            TCrudModel model = ListItems<TCrudModel>().FirstOrDefault(modelItr => modelItr.Id == id);
            if (model == default(TCrudModel))
            {
                throw new AccessViolationException("Object with ID not found");
            }
            return model;
        }

        public void AddItem<TCrudModel>(TCrudModel model) where TCrudModel : CrudModelBase
        {
            List<TCrudModel> list = ListItems<TCrudModel>();
            TCrudModel oldModel = list.FirstOrDefault(modelItr => modelItr.Id == model.Id);
            if (oldModel != default(TCrudModel))
            {
                throw new AccessViolationException("Item with ID already exists");
            }
            
            _storage[typeof(TCrudModel)].Add(model);
        }

        public void UpdateItem<TCrudModel>(int id, TCrudModel newModel) where TCrudModel : CrudModelBase
        {
            List<TCrudModel> list = ListItems<TCrudModel>();
            int listIndex = list.FindIndex(iteratorModel => iteratorModel.Id == id);
            if (newModel == null)
            {
                _storage[typeof(TCrudModel)].RemoveAt(listIndex);
            }
            else
            {
                _storage[typeof(TCrudModel)][listIndex] = newModel;
            }
        }

        public void DeleteItem<TCrudModel>(int id) where TCrudModel : CrudModelBase
        {
            UpdateItem<TCrudModel>(id, null);
        }
    }
}