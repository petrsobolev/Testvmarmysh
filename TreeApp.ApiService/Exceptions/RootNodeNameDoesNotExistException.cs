namespace TreeApp.ApiService.Exceptions;

public class RootNodeNameDoesNotExistException(string message) : Exception(message)
{
    public string Type => "RootNodeNameDoesNotExist";
}