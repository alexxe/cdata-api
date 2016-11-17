// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepository.cs" company="">
//   
// </copyright>
// <summary>
//   The Repository interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.Repo
{
    using System.Collections;
    using System.Collections.Generic;

    using Covis.Data.Common;
    using Covis.Data.SqlProvider.Contracts;

    /// <summary>
    ///     The Repository interface.
    /// </summary>
    /// <typeparam name="TModelEntity">
    /// </typeparam>
    public interface IRepository<TModelEntity>
        where TModelEntity : class, IModelEntity
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The find.
        /// </summary>
        /// <param name="param">
        ///     The param.
        /// </param>
        /// <returns>
        ///     The <see cref="IEnumerable" />.
        /// </returns>
        IEnumerable<TModelEntity> Find(QueryDescriptor param);

        List<object> Project(QueryDescriptor param);

        IEnumerable<TModelEntity> ProjectToModel(QueryDescriptor descriptor);
        void Test();

        #endregion
    }
}