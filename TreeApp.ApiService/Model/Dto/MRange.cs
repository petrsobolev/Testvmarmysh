namespace TreeApp.ApiService.Model.Dto;

public class MRange<T>
{
    public int Skip { get; set; }
    public int Count { get; set; }
    public List<T> Items { get; set; }
}