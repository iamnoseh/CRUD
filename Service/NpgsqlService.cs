using Npgsql;
using Books;
namespace NpgsqlService;

public static class NpgsqlServices
{
    public static void CreateDatabase(string databaseName)
    {
        string connectionString = @"Server=localhost;Port=5432;Database=postgres;Username=postgres;Password=12345;";
        using (var conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand($"CREATE DATABASE {databaseName};", conn))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
    public static void CreateTable(string databaseName, string tableName, string[] columnDefinitions)
{
    string connectionString = $@"Server=localhost;Port=5432;Database={databaseName};Username=postgres;Password=12345;";
    using (var conn = new NpgsqlConnection(connectionString))
    {
        conn.Open();
        string columns = string.Join(", ", columnDefinitions); 
        string createTableQuery = $@"
            CREATE TABLE IF NOT EXISTS {tableName} 
                                        (
                                        {columns}
                                         );";
        using (var cmd = new NpgsqlCommand(createTableQuery, conn))
        {
            cmd.ExecuteNonQuery();
        }
    }
}
    public static void InsertBook(Book book, string databaseName)
    {
         string connectionString = @"Server=localhost;Port = 5432;Database=" + databaseName + ";Username=postgres;Password=12345;";
        using (var conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand("INSERT INTO books (title, author, genre, publication_date) VALUES (@Title, @Author, @Genre, @PublicationDate);"))
            {
                cmd.Parameters.AddWithValue("Title", book.Title);
                cmd.Parameters.AddWithValue("Author", book.Author);
                cmd.Parameters.AddWithValue("Genre", book.Genre);
                cmd.Parameters.AddWithValue("PublicationDate", book.PublishedYear);
                cmd.ExecuteNonQuery();
            }
        }
    }
    public static List<Book> GetBooks(string databaseName)
    {
        List<Book> books = new List<Book>();
        string connectionString = @"Server=localhost;Port = 5432;Database=" + databaseName + ";Username=postgres;Password=12345;";
        using (var conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand("SELECT * FROM books;", conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    books.Add(new Book
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Author = reader.GetString(2),
                        Genre = reader.GetString(3),
                        PublishedYear = reader.GetDateTime(4)
                    });
                }
            }
        }
        return books;
    }
    public static void UpdateBook(Book book, string databaseName)
    {
        string connectionString = @"Server=localhost;Port = 5432;Database=" + databaseName + ";Username=postgres;Password=12345;";
        using (var conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand("UPDATE books SET title=@Title, author=@Author, genre=@Genre, publication_date=@PublicationDate WHERE id=@Id;", conn))
            {
                cmd.Parameters.AddWithValue("Title", book.Title);
                cmd.Parameters.AddWithValue("Author", book.Author);
                cmd.Parameters.AddWithValue("Genre", book.Genre);
                cmd.Parameters.AddWithValue("PublicationDate", book.PublishedYear);
                cmd.Parameters.AddWithValue("Id", book.Id);
                cmd.ExecuteNonQuery();
            }
        }
    }
    public static void DeleteBook(int id, string databaseName)
    {
        string connectionString = @"Server=localhost;Port = 5432;Database=" + databaseName + ";Username=postgres;Password=12345;";
        using (var conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand("DELETE FROM books WHERE id=@Id;", conn))
            {
                cmd.Parameters.AddWithValue("Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
