using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Couchbase.Core;
using Couchbase;
using Couchbase.N1QL;
using System.Net;
using dotnetcore.Models;
using Microsoft.Extensions.Options;

namespace dotnetcore.Controllers
{
    [Route("api/")]
    public class PersonController : Controller
    {
        private readonly string BucketName;
        private readonly IBucket _bucket;

        public PersonController(IOptions<MySettings> settings)
        {
            BucketName = "TODO: get bucket name from settings";
            _bucket = null; // TODO: Use ClusterHelper to get bucket
        }

        [HttpGet]
        [Route("get/{id?}")]
        public async Task<IActionResult> Get(string id = null)
        {
            // TODO: if id is null, then return a BadRequest (400) with an error message

            // TODO: use the GetAsync method of the bucket to get a Person document with key 'id'
            IOperationResult<Person> result = null; // TODO

            // TODO: if the result is not successful, then return an Internal Server Error (500) with an error message

            // set the id on the result so it can be used by angular
            result.Value.Id = id;

            // TODO: return the document as an Ok (200) response
            return Ok("TODO");
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> Getall()
        {
            // TODO: create a N1QL query that returns the key using META().id, and the rest of the document
            // where the type of the document is 'person'
            // and use RequestPlus scan consistency
            var query = new QueryRequest()
                .Statement("TODO")
                .AddPositionalParameter("TODO")
                .ScanConsistency(ScanConsistency.NotBounded); // TODO

            var result = await _bucket.QueryAsync<Person>(query);
            if (!result.Success)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, result.Exception?.Message ?? result.Message);
            }

            return Ok(result.Rows);
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Save([FromBody] Person person)
        {
            if (person == null || !person.IsValid())
            {
                return BadRequest("Missing or invalid body content");
            }

            if (string.IsNullOrEmpty(person.Id))
            {
                person.Id = Guid.NewGuid().ToString();
            }

            // TODO: use the Upsert bucket operation to save the updated document or insert a new document
            IOperationResult<Person> result = null; // TODO

            if (!result.Success)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, result.Exception?.Message ?? result.Message);
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody] Person person)
        {
            if (string.IsNullOrEmpty(person.Id))
            {
                return BadRequest("Missing or invalid 'document_id' body parameter");
            }

            // TODO: use the Remove bucket operation to delete a document using its id
            IOperationResult result = null; // TODO
            if (!result.Success)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, result.Exception?.Message ?? result.Message);
            }

            return Ok(result);
        }
    }
}
