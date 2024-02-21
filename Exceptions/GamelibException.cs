namespace gamelib.Exceptions;

public class GamelibException : Exception
{
    public string Caption { get; }

    public GamelibException(string message, string caption) : base(message)
    {
        Caption = caption;
    }

    public GamelibException(string message, Exception innerException, string caption) :
        base(message, innerException)
    {
        Caption = caption;
    }
}