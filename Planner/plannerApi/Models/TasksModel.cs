namespace plannerApi.Models;

public class taskModel
{
    public int id { get; set; }
    public string? title { get; set; }
    public string? taskDescription { get; set; }
    public bool isCompleted { get; set; }

}