namespace CustomerRepository.Entities;

public class Note
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string Text { get; set; }=String.Empty;
}