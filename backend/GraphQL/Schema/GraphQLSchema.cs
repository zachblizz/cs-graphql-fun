using System;
using System.IO;
using System.Linq;
using EntityGraphQL.Schema;
using EntityGraphQL.Extensions;

using backend.Context;
using backend.Models;

namespace backend
{
    public class GraphQLSchema
    {
        public static MappedSchemaProvider<ApplicationCtx> MakeSchema()
        {
            // build our schema directly from the DB Context
            var schema = SchemaBuilder.FromObject<ApplicationCtx>();

            schema.AddCustomScalarType(typeof(DateTime), "Date");
            schema.AddCustomScalarType(typeof(DateTime?), "Date");

            // Add custom root fields
            schema.ReplaceField("authors", new
            {
                filter = ArgumentHelper.EntityQuery<Author>()
            }, (db, param) => db.Author.Where(p => p.Books.Any()).WhereWhen(param.filter, param.filter.HasValue), "List of books");

            schema.Type<Author>().ReplaceField("books", m => m.Books.Select(a => a.Name), "Books they wrote");

            File.WriteAllText("schema.graphql", schema.GetGraphQLSchema());
            return schema;
        }
    }
}
