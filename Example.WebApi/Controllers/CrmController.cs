// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectController.cs" company="">
//   
// </copyright>
// <summary>
//   The project controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Example.Repo;
using Qdata.Json.Contract;


namespace Example.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Example.Data.Contract.Model;
    

    /// <summary>
    ///     The project controller.
    /// </summary>
    [RoutePrefix("api/crm")]
    public class CrmController : ApiController
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
        [Route("customer")]
        public HttpResponseMessage PostCustomer([FromBody] QDescriptor param)
        {
            var model = CrmModel.GetInstance();
            var result = model.Find<CustomerDto>(param);
            return this.Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("contact")]
        public HttpResponseMessage PostContact([FromBody] QDescriptor param)
        {
            var repository = (CrmModel)CrmModel.GetInstance();
            var result = repository.Find<ContactDto>(param);
            return this.Request.CreateResponse(HttpStatusCode.OK, result);
        }
        #endregion
    }
}