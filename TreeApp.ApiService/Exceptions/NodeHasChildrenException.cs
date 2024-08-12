namespace TreeApp.ApiService.Exceptions;

public class NodeHasChildrenException(string message) : Exception(message)
{
    public string Type => "NodeHasChildren";
}
