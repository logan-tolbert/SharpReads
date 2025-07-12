namespace App.Application.DTOs.Responses;

public class BooksCatalogResponse
{
    public string Message { get; set; } = string.Empty;
    public List<BookResponse> Books { get; set; } = new();
    public PricingInfo Pricing { get; set; } = new();
    public int TotalCount { get; set; }
}

public class PricingInfo
{
    public string Currency { get; set; } = "USD";
    public string Discount { get; set; } = "10%";
} 
