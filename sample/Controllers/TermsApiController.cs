using sample.Models;
using sample.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace sample.Controllers
{
    [RoutePrefix("api/terms")]
    [AllowAnonymous]
    public class TermsApiController : ApiController
    {

        [Route("SaveTerm")]
        [HttpPost]
        public HttpResponseMessage SaveTerm(TermRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            else
            {
                Guid externalId = TermService.EditTerm(model);

                TermResponseModel respModel = new TermResponseModel();

                if (externalId != Guid.Empty)
                {
                    respModel.Success = true;
                    respModel.ExternalId = externalId;
                    return Request.CreateResponse(HttpStatusCode.OK, respModel);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, respModel);
                }
            }
        }

        [Route("DeleteTerm")]
        [HttpPost]
        public HttpResponseMessage DeleteTerm(TermRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            else
            {
                TermService.DeleteTerm(model);

                return Request.CreateResponse(HttpStatusCode.OK);
            }

        }

        [Route("GetTermsList")]
        [HttpGet]
        public HttpResponseMessage GetTermsList(TermResponseModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            else
            {
                List<TermResponseModel> respList = TermService.GetTerms();

                return Request.CreateResponse(HttpStatusCode.OK, respList);
            }

        }
    }
}