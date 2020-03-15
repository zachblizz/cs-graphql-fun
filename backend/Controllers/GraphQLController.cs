using Microsoft.AspNetCore.Mvc;

using backend.Context;
using EntityGraphQL.Schema;
using System.Net;
using EntityGraphQL;

namespace backend.Controllers
{
    [Route("api/graphql")]
    [ApiController]
    public class GraphQLController : Controller
    {
        private readonly ApplicationCtx _db;
        private readonly MappedSchemaProvider<ApplicationCtx> _schemaProvider;

        public GraphQLController(ApplicationCtx db, MappedSchemaProvider<ApplicationCtx> schemaProvider)
        {
            _db = db;
            _schemaProvider = schemaProvider;
        }

        [HttpPost]
        public object Post([FromBody] QueryRequest query)
        {
            try
            {
                var results = _db.QueryObject(query, _schemaProvider);
                return results;
            }
            catch
            {
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}