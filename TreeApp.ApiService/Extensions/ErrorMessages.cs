namespace TreeApp.ApiService.Extensions;

public static class ErrorMessages
{
    public static string GetDuplicateChildErrorMessage(string name) => $"Child with name '{name}' already exists.";

    public static string GetNodeHasChildrenErrorMessage => "You have to delete all children nodes first.";

    public static string GetRootNameDoesNotExistErrorMessage (string name) => $"Tree with '{name}' name does not exist";

    public static string GetNotFoundNodeByIdErrorMessage (int nodeId) => $"Node with '{nodeId}' id does not exist";

    public static string GetNodeWithWrongRootErrorMessage =>
        "Requested node was found, but it doesn't belong your tree";

    public static string GetNotFoundJournalExceptionMessage(int id) => $"Journal with '{id} was not found";
    
}