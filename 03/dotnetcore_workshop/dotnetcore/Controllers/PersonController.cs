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
            BucketName = settings.Value.CouchbaseBucket;
            _bucket = ClusterHelper.GetBucket(BucketName);
        }

        [HttpGet]
        [Route("get/{id?}")]
        public async Task<IActionResult> Get(string id = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Missing or empty 'id' query string parameter");
            }

            var result = await _bucket.GetAsync<Person>(id);
            if (!result.Success)
            {
                return StatusCode((int)HttpStatusCode.OK, result.Exception?.Message ?? result.Message);
            }

            result.Value.Id = id;

            return Ok(result.Value);
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> Getall()
        {
            var query = new QueryRequest()
                .Statement("SELECT META().id, `default`.* FROM `default` WHERE type = $1")
                .AddPositionalParameter(typeof(Person).Name.ToLower())
                .ScanConsistency(ScanConsistency.RequestPlus);

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

            var result = await _bucket.UpsertAsync(person.Id, person);
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

            var result = await _bucket.RemoveAsync(person.Id);
            if (!result.Success)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, result.Exception?.Message ?? result.Message);
            }

            return Ok(result);
        }
    }
}
