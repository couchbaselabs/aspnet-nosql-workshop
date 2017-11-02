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
        private readonly IBucket _bucket;

        public PersonController()
        {
            var bucketName = ConfigurationManager.AppSettings.Get("couchbaseBucket");
            _bucket = CouchbaseConfig.Cluster.OpenBucket(bucketName);
        }

        [HttpGet]
        [Route("get/{id?}")]
        public async Task<IHttpActionResult> Get(string id = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Missing or empty 'id' query string parameter");
            }

            var result = await _bucket.GetAsync<Person>(id);
            if (!result.Success)
            {
                return Content(HttpStatusCode.InternalServerError, result.Exception?.Message ?? result.Message);
            }

            result.Value.Id = id;

            return Ok(result.Value);
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IHttpActionResult> Getall()
        {
            var query = new QueryRequest()
                .Statement(@"SELECT META().id, b.* 
                           FROM `workshop` b
                           WHERE type = $1")
                .AddPositionalParameter(typeof(Person).Name.ToLower())
                .ScanConsistency(ScanConsistency.RequestPlus);

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

            var result = await _bucket.UpsertAsync(person.Id, person);
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

            var result = await _bucket.RemoveAsync(person.Id);
            if (!result.Success)
            {
                return Content(HttpStatusCode.InternalServerError, result.Exception?.Message ?? result.Message);
            }

            return Ok(result);
        }
    }
}
