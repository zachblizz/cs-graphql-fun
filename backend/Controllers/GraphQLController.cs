using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;

using backend.Context;
using backend.GraphQL;

namespace backend.Controllers
{
    [Route("graphql")]
    [ApiController]
    public class GraphQLController : Controller
    {
        private readonly ApplicationCtx _db;
        public GraphQLController(ApplicationCtx db) => _db = db;

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
        {
            var inputs = query.Variables.ToInputs();
            var schema = new Schema
            {
                Query = new AuthorQuery(_db)
            };

            var result = await new DocumentExecuter().ExecuteAsync(_ =>
            {
                _.Schema = schema;
                _.Query = query.Query;
                _.OperationName = query.OperationName;
                _.Inputs = inputs;
            });

            if (result.Errors?.Count > 0)
            {
                return BadRequest(result.Errors.FirstValue(e => e.Data));
            }

            return Ok(result);
        }
    }
}