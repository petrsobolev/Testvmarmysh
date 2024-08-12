namespace TreeApp.ApiService.Exceptions;

public class NotFoundJournalException(string message) : Exception(message)
{
    public string Type => "NotFoundJournal";
}