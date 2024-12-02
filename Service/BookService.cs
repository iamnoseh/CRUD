namespace BooksService.Services;
using IBookservice.Services;
using Books;
using NpgsqlService;
using Npgsql;
using System.Data.Common;

public class BookService(string connectionString) : IBookService
{

    public void AddBook(Book book, string table_name)
{
    using (var connection = new NpgsqlConnection(connectionString))
    {
        connection.Open();
        var command = new NpgsqlCommand($"INSERT INTO {table_name} (title, author, publication_date, genre) VALUES (@title, @author, @publication_date, @genre)", connection);
        command.Parameters.AddWithValue("title", book.Title);
        command.Parameters.AddWithValue("author", book.Author);
        command.Parameters.AddWithValue("publication_date", book.PublishedYear); 
        command.Parameters.AddWithValue("genre", book.Genre); 
        command.ExecuteNonQuery(); 
    }
}



public void DeleteBook(int id, string tableName)
{
    using (var connection = new NpgsqlConnection(connectionString))
    {
        connection.Open();
        var command = new NpgsqlCommand($"DELETE FROM {tableName} WHERE id = @id", connection);
        command.Parameters.AddWithValue("id", id); 
        command.ExecuteNonQuery();
    }
}

    public List<Book> GetBookById(int id, string tableName)
    {
        List<Book> books = new ();
        
        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            var command = new NpgsqlCommand($"SELECT * FROM {tableName} WHERE id = @id", connection);
             command.Parameters.AddWithValue("id", id);
            using (var reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    { Book book = new Book()
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Author = reader.GetString(2),
                        PublishedYear = reader.GetDateTime(3),
                        Genre = reader.GetString(4)
                        };
                        books.Add(book);
                    }
                }
                    return books;
            }
            
          
        }
    }
public void UpdateBook(Book book, string tableName)
{
    using (var connection = new NpgsqlConnection(connectionString))
    {
        connection.Open(); 
        var command = new NpgsqlCommand();
        command.Connection = connection; 
        
        command.CommandText = $"UPDATE {tableName} SET title = @title, author = @author, publication_date = @publication_date, genre = @genre WHERE id = @id";

        command.Parameters.AddWithValue("title", book.Title);
        command.Parameters.AddWithValue("author", book.Author);
        command.Parameters.AddWithValue("publication_date", book.PublishedYear);
        command.Parameters.AddWithValue("genre", book.Genre);
        command.Parameters.AddWithValue("id", book.Id);
        command.ExecuteNonQuery(); 
    }
}

}


