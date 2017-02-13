using System;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Couchbase;
using Couchbase.Core;
using Couchbase.N1QL;
using workshop_dotnet.Models;

namespace workshop_dotnet.Controllers
{
    [RoutePrefix("api")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PersonController : ApiController
    {
        // TODO: get the couchbaseBucket using ConfigurationManager 
        private static readonly string BucketName = "TODO";

        // TODO: get a bucket object using the ClusterHelper and the bucket name
        private readonly IBucket _bucket = null; // TODO

        [HttpGet]
        [Route("get/{id?}")]
        public async Task<IHttpActionResult> Get(string id = null)
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
        public async Task<IHttpActionResult> Getall()
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
                return Content(HttpStatusCode.InternalServerError, result.Exception?.Message ?? result.Message);
            }

            return Ok(result.Rows);
        }

        [HttpPost]
        [Route("save")]
        public async Task<IHttpActionResult> Save([FromBody] Person person)
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
                return Content(HttpStatusCode.InternalServerError, result.Exception?.Message ?? result.Message);
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IHttpActionResult> Delete([FromBody] Person person)
        {
            if (string.IsNullOrEmpty(person.Id))
            {
                return BadRequest("Missing or invalid 'document_id' body parameter");
            }

            // TODO: use the Remove bucket operation to delete a document using its id
            IOperationResult result = null; // TODO
            if (!result.Success)
            {
                return Content(HttpStatusCode.InternalServerError, result.Exception?.Message ?? result.Message);
            }

            return Ok(result);
        }
    }
}
