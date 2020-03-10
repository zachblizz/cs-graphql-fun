using backend.Models;
using GraphQL.Types;

namespace backend.GraphQL
{
    public class BookType : ObjectGraphType<Book>
    {
        public BookType()
        {
            // Name of the quer
            Name = "Book";

            // The quieriable fields
            Field(x => x.Id, type: typeof(IdGraphType)).Description("The ID of the Book.");
            Field(x => x.Name).Description("The name of the Book");
            Field(x => x.Genre).Description("Book genre");
            Field(x => x.Published).Description("If the book is published or not");
        }
    }
}