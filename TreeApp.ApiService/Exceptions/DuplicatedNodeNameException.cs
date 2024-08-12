namespace TreeApp.ApiService.Exceptions;

public class DuplicatedNodeNameException(string message) : Exception(message)
{
    public string Type => "DuplicatedNodeName";
}
