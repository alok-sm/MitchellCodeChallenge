namespace MitchellApi.Models
{
    /// <summary>
    /// Base class that can be used to create a CrudController
    /// </summary>
    public abstract class CrudModelBase
    {
        /// <summary>
        /// Id is the default property of all CrudModelBase objects
        /// </summary>
        public int Id { get; set; }
    }
}