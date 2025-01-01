using System;

namespace Compilator
{
    public class LexicalError : CompilerError
    {
        public LexicalError(string message, int line, int column)
            : base(message, line, column)
        {
        }

        public override string ToString()
        {
            return $"Lexical Error at line {Line}, column {Column}: {Message}";
        }
    }
}
