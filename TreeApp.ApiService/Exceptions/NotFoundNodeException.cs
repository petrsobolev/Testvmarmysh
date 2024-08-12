namespace TreeApp.ApiService.Exceptions;

public class NotFoundNodeException(string name) : Exception(name)
{
    public string Type => "NotFoundNodeWithId";
}