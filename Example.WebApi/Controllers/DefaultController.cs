// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectController.cs" company="">
//   
// </copyright>
// <summary>
//   The project controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Example.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using Covis.Data.Json.Contracts;

    using Example.Data.Contract.Model;
    using Example.Repo;
    //using System.Linq;

    /// <summary>
    ///     The project controller.
    /// </summary>
    [RoutePrefix("api/Model")]
    public class DefaultController : ApiController
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The get.
        /// </summary>
        /// <returns>
        ///     The <see cref="HttpResponseMessage" />.
        /// </returns>
        [HttpGet]
        [Route("Metadata")]
        public object Get()
        {
            return this.Request.CreateResponse(
                HttpStatusCode.OK,
                new CustomerDto() { Contacts = new List<ContactDto>() { new ContactDto() } });
        }

        [HttpPost]
        [Route("Default")]
        public HttpResponseMessage Post([FromBody] QDescriptor param)
        {
            var repository = (DefaultRepository)DefaultRepository.GetInstance();
            var result = repository.Find(param);
            return this.Request.CreateResponse(HttpStatusCode.OK, result);
        }

        #endregion
    }
}