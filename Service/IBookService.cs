namespace IBookservice.Services;
using Books;

public interface IBookService
{
    void AddBook(Book book,string table_name);
    void DeleteBook(int id,string tableName);
    void UpdateBook(Book book,string tableName);
    List<Book> GetBookById(int id,string tableName);
}