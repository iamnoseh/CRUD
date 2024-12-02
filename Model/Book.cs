namespace Books;

public class Book
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
    public DateTime PublishedYear { get; set; }
    public string? Genre { get; set; }
    public Book(){}

    //kanstruktor baroi update kardan
    public Book(int id, string title, string author, string genre, DateTime publication_date)
    {
        Id = id;
        Title = title;
        Author = author;
        Genre = genre;
        PublishedYear = publication_date;
    }
}