// using System.Linq;
// using GraphQL.Types;
// using Microsoft.EntityFrameworkCore;

// using backend.Context;

// namespace backend.GraphQL
// {
//     public class AuthorQuery : ObjectGraphType
//     {
//         public AuthorQuery(ApplicationCtx db)
//         {
//             Field<AuthorType>(
//                 "Author",
//                 arguments: new QueryArguments(
//                     new QueryArgument<IdGraphType> { Name = "id", Description = "The ID of the Author." }
//                 ),
//                 resolve: context =>
//                 {
//                     var id = context.GetArgument<int>("id");
//                     var author = db
//                         .Author
//                         .Include(a => a.Books)
//                         .FirstOrDefault(i => i.Id == id);

//                     return author;
//                 }
//             );

//             Field<AuthorType>(
//                 "Authors",
//                 resolve: context =>
//                 {
//                     var authors = db.Author.Include(a => a.Books);
//                     return authors;
//                 }
//             );
//         }
//     }
// }