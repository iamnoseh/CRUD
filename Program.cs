using Books;
using BooksService.Services;
using NpgsqlService;
using IBookservice;
using Npgsql;
using System.Data.Common;
// System.Console.WriteLine("Nomi database - ro vorid kuned !");
// string database = Console.ReadLine();
// NpgsqlServices.CreateDatabase(database);
// System.Console.WriteLine("Nomi database - i ki dar on table - ro mesozed vorid kunad !");
string database1 = Console.ReadLine();
// System.Console.WriteLine("Nomi Table - ro vorid kunaed!");
string tableName = Console.ReadLine();
// System.Console.WriteLine("Miqdori Collumnhoro vorid kuned!");
// int count = int.Parse(Console.ReadLine());
// string[] columns = new string[count];
// for (int i = 0; i < count; i++)
// {
//     Console.WriteLine($"Collumn: {i + 1} - ro vorid kuned !(\n!!! FORMAT: <Nomi collumn> <Type>, : ID SERIAL PRIMARY KEY ):");
//     columns[i] = Console.ReadLine();
// }
// try
// {
//     NpgsqlServices.CreateTable(database1, tableName, columns);
//     Console.WriteLine($"Table - i {tableName} dar DB-i {database1} sokhta shud!");
// }
// catch (NpgsqlException ex)
// {
//     Console.WriteLine($" !!! ERROR !!! {ex.Message}");
// }
// catch(Exception ex)
// {
//     Console.WriteLine($"!!! ERROR!!! {ex.Message}");
// }

Book book = new Book();
// System.Console.Write("Title: " );
// book.Title = Console.ReadLine();
// System.Console.Write("Author: " );
// book.Author = Console.ReadLine();
// System.Console.Write("Genre: " );
// book.Genre = Console.ReadLine();
// System.Console.Write("PublishedYaer: " );
// book.PublishedYear = DateTime.Parse(Console.ReadLine());
string connectionString = @"Server=localhost;Port = 5432;Database=" + database1+ ";Username=postgres;Password=12345;";
BookService book1 = new BookService(connectionString);
// book1.AddBook(book,tableName);

// book1.DeleteBook(1,tableName);

book1.UpdateBook(new Book(4,"new title","new author","new genre",new DateTime(2020, 5, 1)),tableName);
var res = book1.GetBookById(4,tableName);

foreach (var item in res)
{
    Console.WriteLine($"{item.Id} - {item.Title} - {item.Author} - {item.Genre} - {item.PublishedYear}");
}
