using System;
using System.IO;

using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System.Collections.Generic;

public class SpeakLine
{
    public string Person { get; set; }
    public string Text { get; set; }
}

public class BasicSpeakVisitor : SpeakBaseVisitor<object>
{
    public List<SpeakLine> SpeakLines = new List<SpeakLine>();

    public override object VisitLine([NotNull] SpeakParser.LineContext context)
    {
        var name = context.name();
        var opinion = context.opinion();

        SpeakLine line = new SpeakLine() { Person = name.GetText(), Text = opinion.GetText().Trim('"') };

        SpeakLines.Add(line);

        return line;
    }
}

public class Program
{
    private static SpeakParser Setup(string text)
    {
        AntlrInputStream inputStream = new AntlrInputStream(text);
        SpeakLexer speakLexer = new SpeakLexer(inputStream);
        CommonTokenStream commonTokenStream = new CommonTokenStream(speakLexer);
        SpeakParser speakParser = new SpeakParser(commonTokenStream);
        return speakParser;
    }

    public static void Main()
    {
        string text = File.ReadAllText("../../../text.in");

        SpeakParser parser = Setup(text);
        SpeakParser.ChatContext context = parser.chat();
        BasicSpeakVisitor visitor = new BasicSpeakVisitor();
        visitor.Visit(context);

        foreach (var line in visitor.SpeakLines)
        {
            Console.WriteLine("{0} has said {1}", line.Person, line.Text);
        }
    }
}