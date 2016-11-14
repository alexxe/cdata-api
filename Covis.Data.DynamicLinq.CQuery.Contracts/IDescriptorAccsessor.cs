namespace Covis.Data.DynamicLinq.CQuery.Contracts
{
    public interface IDescriptorAccsessor
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the root.
        /// </summary>
        QueryDescriptor Descriptor { get; }

        #endregion
    }
}