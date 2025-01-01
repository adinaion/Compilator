public class CompilerError
{
    public string Message { get; set; }
    public int Line { get; set; }
    public int Column { get; set; }

    public CompilerError(string message, int line, int column)
    {
        Message = message;
        Line = line;
        Column = column;
    }

    public override string ToString()
    {
        return $"Error at line {Line}, column {Column}: {Message}";
    }
}
