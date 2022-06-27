// <auto-generated> - Template:BaseAPIController, Version:2022.06.21, Id:af56140d-4926-4e6a-addb-49f3cfcd4a53
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using ArtistSite.Repository.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.AspNetCore.Http.Extensions;
using ArtistSite.Shared.DataService;
using Enums = ArtistSite.Shared.Constants.Enums;

namespace ArtistSite.Api.Controllers
{
	// [AutoInvalidateCacheOutput]
	[AllowAnonymous]
	[ApiController]
	[Route("api/AS/[controller]")]
	public abstract partial class ASBaseApiController : Controller
{
		private readonly ILogger _logger;
		private readonly IASRepository _repository;
		protected IHttpContextAccessor HttpContextAccessor { get; private set; }
		protected LinkGenerator LinkGenerator { get; private set; }

		protected IASRepository Repo { get { return _repository; } }
		protected ILogger Log { get { return _logger; } }
		protected IServiceProvider ServiceProvider { get; set; }

		public ASBaseApiController()
		{
				_logger = NullLogger.Instance;
		}

		public ASBaseApiController(ILogger logger,
			IServiceProvider serviceProvider,
			IHttpContextAccessor httpContextAccessor,
			LinkGenerator linkGenerator,
			IASRepository repository)
		{
				_logger = logger ?? NullLogger.Instance;
				ServiceProvider = serviceProvider;
				HttpContextAccessor = httpContextAccessor;
				LinkGenerator = linkGenerator;
				_repository = repository;
		}

                protected PageData BuildPaginationHeader(string action, int page, int totalCount, int pageSize, string sort)
                {   // calculate data for metadata
                    var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                    var prevLink = page > 1 ? LinkGenerator.GetUriByAction(
                        httpContext: HttpContextAccessor.HttpContext,
                        action: action,
                        values: new { page = page - 1, pageSize = pageSize, sort = sort }) : "";

                    var nextLink = page < totalPages ? LinkGenerator.GetUriByAction(
                        httpContext: HttpContextAccessor.HttpContext,
                        action: action,
                        values: new { page = page + 1, pageSize = pageSize, sort = sort }) : "";

                    return new PageData(currentPage: page, nextPageLink: nextLink, pageSize: pageSize, previousPageLink: prevLink, totalCount: totalCount, totalPages: totalPages);
                }

                protected string GetClientIpAddress()
                {
                    return HttpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                }

                protected List<string> GetListByDelimiter(string fields, char delimiter = ',')
                {
                    List<string> retVal = new List<string>();

                    if (!string.IsNullOrEmpty(fields))
                    {
                        retVal = fields.ToLower().Split(delimiter).ToList();
                    }

                    return retVal;
                }

                protected string GetUrl(HttpRequest request = null)
                {
                    request = request ?? base.Request;
                    var path = request.Path;
                    var query = request.QueryString;
                    var pathAndQuery = path + query;

                    return pathAndQuery;
                }

                protected IActionResult PrepareExpectationFailedResponse(Exception ex)
                {
                    var args = new object[] {
                        (int)StatusCodes.Status417ExpectationFailed,
                        HttpContext.Request.GetEncodedUrl() };

                    Log.LogWarning(eventId: (int)Enums.EventId.Warn_WebApi,
                        exception: ex,
                        message: "Web API action failed. {httpResponseStatusCode}:{url}",
                        args: args);

                    var retVal = StatusCode(StatusCodes.Status417ExpectationFailed, ex);
                    return retVal;
                }

                protected IActionResult PrepareInternalServerErrorResponse(Exception ex)
                {
                    var args = new object[] {
                        (int)StatusCodes.Status500InternalServerError,
                        HttpContext.Request.GetEncodedUrl() };
                    Log.LogError(eventId: (int)Enums.EventId.Exception_WebApi,
                        exception: ex,
                        message: $"{ex.Message} {{httpResponseStatusCode}}:{{url}}",
                        args: args);

                    var retVal = StatusCode(StatusCodes.Status500InternalServerError,
                        value: System.Diagnostics.Debugger.IsAttached ? ex : null);
                    return retVal;
                }

                protected IActionResult PrepareNotFoundResponse()
                {
                    var args = new object[] {
                        "httpResponseStatusCode", (int)StatusCodes.Status404NotFound ,
                        "url", HttpContext.Request.GetEncodedUrl() };
                Log.LogWarning(eventId: (int)Enums.EventId.Warn_WebApi,
                    exception: null,
                    message: "Unable to find requested object via Web API. {httpResponseStatusCode}:{url}",
                    args: args);

                    return NotFound();
                }

                protected bool OnActionExecuting(out int httpStatusCode, out string message, [CallerMemberName] string methodName = null)
                {
                    httpStatusCode = (int)HttpStatusCode.OK;
                    message = null;
                    RunCustomLogicOnActionExecuting(ref httpStatusCode, ref message, methodName);
                    return (httpStatusCode == (int)HttpStatusCode.OK);
                }
		protected virtual void RunCustomLogicAfterCtor() { }

		protected virtual void RunCustomLogicOnActionExecuting(ref int httpStatusCode, ref string message, string methodName) { }

	}
}
