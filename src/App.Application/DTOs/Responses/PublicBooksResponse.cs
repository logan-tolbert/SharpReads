namespace App.Application.DTOs.Responses;

public class PublicBooksResponse
{
    public string Message { get; set; } = string.Empty;
    public List<PublicBookResponse> Books { get; set; } = new();
    public string Note { get; set; } = string.Empty;
}

public class PublicBookResponse
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public int PublishedYear { get; set; }
} 
