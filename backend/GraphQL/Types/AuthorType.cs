using backend.Models;
using GraphQL.Types;

namespace backend.GraphQL
{
    public class AuthorType : ObjectGraphType<Author>
    {
        public AuthorType()
        {
            // Name of the quer
            Name = "Author";

            // The quieriable fields
            Field(x => x.Id, type: typeof(IdGraphType)).Description("The ID of the Author.");
            Field(x => x.Name).Description("The name of the Author");
            Field(x => x.Books, type: typeof(ListGraphType<BookType>)).Description("Author's books");
        }
    }
}