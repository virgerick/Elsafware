namespace Elsaftware.Shared.Extensions;
public static class ExceptionExtension
{
    public static List<string> GetMessages(this Exception exception)
    {
        var error = new List<string>();
        if(exception==null)
            return error;
        error.Add(exception.Message);
        if (exception.InnerException != null)
        {
            error.AddRange(exception.InnerException.GetMessages());
        }
        return error;
    }
}