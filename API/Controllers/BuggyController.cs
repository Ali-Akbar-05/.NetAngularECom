using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiExplorerSettings(IgnoreApi =true)]
    public class BuggyController : BaseApiController
    {
        private readonly ILogger<BuggyController> _logger;
        private readonly StoreContext _dbCon;

        public BuggyController(ILogger<BuggyController> logger, StoreContext dbCon)
        {
            _logger = logger;
            _dbCon = dbCon;
        }

        [HttpGet("notfound")]
        public async Task<ActionResult> GetNotFoundRequest()
        {
            var thing =await _dbCon.Products.FindAsync(8555);
            if (thing == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok();
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var thing = _dbCon.Products.Find(8555);
            var thingToReturn = thing.ToString();
             
            return Ok();
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }
        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
             return BadRequest();
        }
    }
}