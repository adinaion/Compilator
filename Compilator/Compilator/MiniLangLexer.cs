//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from c:/Users/Adina/Desktop/LFC/Compilator/Compilator/Compilator/MiniLang.g4 by ANTLR 4.13.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using System;
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.1")]
[System.CLSCompliant(false)]
public partial class MiniLangLexer : Lexer {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1, T__1=2, T__2=3, T__3=4, KEYWORD=5, IDENTIFIER=6, NUMBER=7, STRING=8, 
		COMMENT=9, WS=10, ADD=11, SUB=12, MUL=13, DIV=14, MOD=15, LT=16, GT=17, 
		LE=18, GE=19, EQ=20, NE=21, AND=22, OR=23, NOT=24, ASSIGN=25, ADD_ASSIGN=26, 
		SUB_ASSIGN=27, MUL_ASSIGN=28, DIV_ASSIGN=29, MOD_ASSIGN=30, INCREMENT=31, 
		DECREMENT=32, SEMICOLON=33, LPAREN=34, RPAREN=35, LBRACE=36, RBRACE=37, 
		COMMA=38;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"T__0", "T__1", "T__2", "T__3", "KEYWORD", "IDENTIFIER", "NUMBER", "STRING", 
		"COMMENT", "WS", "ADD", "SUB", "MUL", "DIV", "MOD", "LT", "GT", "LE", 
		"GE", "EQ", "NE", "AND", "OR", "NOT", "ASSIGN", "ADD_ASSIGN", "SUB_ASSIGN", 
		"MUL_ASSIGN", "DIV_ASSIGN", "MOD_ASSIGN", "INCREMENT", "DECREMENT", "SEMICOLON", 
		"LPAREN", "RPAREN", "LBRACE", "RBRACE", "COMMA"
	};


	public MiniLangLexer(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public MiniLangLexer(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, "'if'", "'else'", "'for'", "'while'", null, null, null, null, null, 
		null, "'+'", "'-'", "'*'", "'/'", "'%'", "'<'", "'>'", "'<='", "'>='", 
		"'=='", "'!='", "'&&'", "'||'", "'!'", "'='", "'+='", "'-='", "'*='", 
		"'/='", "'%='", "'++'", "'--'", "';'", "'('", "')'", "'{'", "'}'", "','"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, null, null, "KEYWORD", "IDENTIFIER", "NUMBER", "STRING", 
		"COMMENT", "WS", "ADD", "SUB", "MUL", "DIV", "MOD", "LT", "GT", "LE", 
		"GE", "EQ", "NE", "AND", "OR", "NOT", "ASSIGN", "ADD_ASSIGN", "SUB_ASSIGN", 
		"MUL_ASSIGN", "DIV_ASSIGN", "MOD_ASSIGN", "INCREMENT", "DECREMENT", "SEMICOLON", 
		"LPAREN", "RPAREN", "LBRACE", "RBRACE", "COMMA"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "MiniLang.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ChannelNames { get { return channelNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override int[] SerializedAtn { get { return _serializedATN; } }

	static MiniLangLexer() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}
	private static int[] _serializedATN = {
		4,0,38,251,6,-1,2,0,7,0,2,1,7,1,2,2,7,2,2,3,7,3,2,4,7,4,2,5,7,5,2,6,7,
		6,2,7,7,7,2,8,7,8,2,9,7,9,2,10,7,10,2,11,7,11,2,12,7,12,2,13,7,13,2,14,
		7,14,2,15,7,15,2,16,7,16,2,17,7,17,2,18,7,18,2,19,7,19,2,20,7,20,2,21,
		7,21,2,22,7,22,2,23,7,23,2,24,7,24,2,25,7,25,2,26,7,26,2,27,7,27,2,28,
		7,28,2,29,7,29,2,30,7,30,2,31,7,31,2,32,7,32,2,33,7,33,2,34,7,34,2,35,
		7,35,2,36,7,36,2,37,7,37,1,0,1,0,1,0,1,1,1,1,1,1,1,1,1,1,1,2,1,2,1,2,1,
		2,1,3,1,3,1,3,1,3,1,3,1,3,1,4,1,4,1,4,1,4,1,4,1,4,1,4,1,4,1,4,1,4,1,4,
		1,4,1,4,1,4,1,4,1,4,1,4,1,4,1,4,1,4,1,4,1,4,1,4,1,4,1,4,1,4,1,4,1,4,1,
		4,1,4,1,4,1,4,1,4,1,4,1,4,1,4,1,4,1,4,3,4,134,8,4,1,5,1,5,5,5,138,8,5,
		10,5,12,5,141,9,5,1,6,4,6,144,8,6,11,6,12,6,145,1,6,1,6,4,6,150,8,6,11,
		6,12,6,151,3,6,154,8,6,1,7,1,7,5,7,158,8,7,10,7,12,7,161,9,7,1,7,1,7,1,
		8,1,8,1,8,1,8,5,8,169,8,8,10,8,12,8,172,9,8,1,8,1,8,1,9,4,9,177,8,9,11,
		9,12,9,178,1,9,1,9,1,10,1,10,1,11,1,11,1,12,1,12,1,13,1,13,1,14,1,14,1,
		15,1,15,1,16,1,16,1,17,1,17,1,17,1,18,1,18,1,18,1,19,1,19,1,19,1,20,1,
		20,1,20,1,21,1,21,1,21,1,22,1,22,1,22,1,23,1,23,1,24,1,24,1,25,1,25,1,
		25,1,26,1,26,1,26,1,27,1,27,1,27,1,28,1,28,1,28,1,29,1,29,1,29,1,30,1,
		30,1,30,1,31,1,31,1,31,1,32,1,32,1,33,1,33,1,34,1,34,1,35,1,35,1,36,1,
		36,1,37,1,37,1,159,0,38,1,1,3,2,5,3,7,4,9,5,11,6,13,7,15,8,17,9,19,10,
		21,11,23,12,25,13,27,14,29,15,31,16,33,17,35,18,37,19,39,20,41,21,43,22,
		45,23,47,24,49,25,51,26,53,27,55,28,57,29,59,30,61,31,63,32,65,33,67,34,
		69,35,71,36,73,37,75,38,1,0,5,3,0,65,90,95,95,97,122,4,0,48,57,65,90,95,
		95,97,122,1,0,48,57,2,0,10,10,13,13,3,0,9,10,13,13,32,32,265,0,1,1,0,0,
		0,0,3,1,0,0,0,0,5,1,0,0,0,0,7,1,0,0,0,0,9,1,0,0,0,0,11,1,0,0,0,0,13,1,
		0,0,0,0,15,1,0,0,0,0,17,1,0,0,0,0,19,1,0,0,0,0,21,1,0,0,0,0,23,1,0,0,0,
		0,25,1,0,0,0,0,27,1,0,0,0,0,29,1,0,0,0,0,31,1,0,0,0,0,33,1,0,0,0,0,35,
		1,0,0,0,0,37,1,0,0,0,0,39,1,0,0,0,0,41,1,0,0,0,0,43,1,0,0,0,0,45,1,0,0,
		0,0,47,1,0,0,0,0,49,1,0,0,0,0,51,1,0,0,0,0,53,1,0,0,0,0,55,1,0,0,0,0,57,
		1,0,0,0,0,59,1,0,0,0,0,61,1,0,0,0,0,63,1,0,0,0,0,65,1,0,0,0,0,67,1,0,0,
		0,0,69,1,0,0,0,0,71,1,0,0,0,0,73,1,0,0,0,0,75,1,0,0,0,1,77,1,0,0,0,3,80,
		1,0,0,0,5,85,1,0,0,0,7,89,1,0,0,0,9,133,1,0,0,0,11,135,1,0,0,0,13,143,
		1,0,0,0,15,155,1,0,0,0,17,164,1,0,0,0,19,176,1,0,0,0,21,182,1,0,0,0,23,
		184,1,0,0,0,25,186,1,0,0,0,27,188,1,0,0,0,29,190,1,0,0,0,31,192,1,0,0,
		0,33,194,1,0,0,0,35,196,1,0,0,0,37,199,1,0,0,0,39,202,1,0,0,0,41,205,1,
		0,0,0,43,208,1,0,0,0,45,211,1,0,0,0,47,214,1,0,0,0,49,216,1,0,0,0,51,218,
		1,0,0,0,53,221,1,0,0,0,55,224,1,0,0,0,57,227,1,0,0,0,59,230,1,0,0,0,61,
		233,1,0,0,0,63,236,1,0,0,0,65,239,1,0,0,0,67,241,1,0,0,0,69,243,1,0,0,
		0,71,245,1,0,0,0,73,247,1,0,0,0,75,249,1,0,0,0,77,78,5,105,0,0,78,79,5,
		102,0,0,79,2,1,0,0,0,80,81,5,101,0,0,81,82,5,108,0,0,82,83,5,115,0,0,83,
		84,5,101,0,0,84,4,1,0,0,0,85,86,5,102,0,0,86,87,5,111,0,0,87,88,5,114,
		0,0,88,6,1,0,0,0,89,90,5,119,0,0,90,91,5,104,0,0,91,92,5,105,0,0,92,93,
		5,108,0,0,93,94,5,101,0,0,94,8,1,0,0,0,95,96,5,105,0,0,96,97,5,110,0,0,
		97,134,5,116,0,0,98,99,5,102,0,0,99,100,5,108,0,0,100,101,5,111,0,0,101,
		102,5,97,0,0,102,134,5,116,0,0,103,104,5,115,0,0,104,105,5,116,0,0,105,
		106,5,114,0,0,106,107,5,105,0,0,107,108,5,110,0,0,108,134,5,103,0,0,109,
		110,5,118,0,0,110,111,5,111,0,0,111,112,5,105,0,0,112,134,5,100,0,0,113,
		114,5,105,0,0,114,134,5,102,0,0,115,116,5,101,0,0,116,117,5,108,0,0,117,
		118,5,115,0,0,118,134,5,101,0,0,119,120,5,102,0,0,120,121,5,111,0,0,121,
		134,5,114,0,0,122,123,5,119,0,0,123,124,5,104,0,0,124,125,5,105,0,0,125,
		126,5,108,0,0,126,134,5,101,0,0,127,128,5,114,0,0,128,129,5,101,0,0,129,
		130,5,116,0,0,130,131,5,117,0,0,131,132,5,114,0,0,132,134,5,110,0,0,133,
		95,1,0,0,0,133,98,1,0,0,0,133,103,1,0,0,0,133,109,1,0,0,0,133,113,1,0,
		0,0,133,115,1,0,0,0,133,119,1,0,0,0,133,122,1,0,0,0,133,127,1,0,0,0,134,
		10,1,0,0,0,135,139,7,0,0,0,136,138,7,1,0,0,137,136,1,0,0,0,138,141,1,0,
		0,0,139,137,1,0,0,0,139,140,1,0,0,0,140,12,1,0,0,0,141,139,1,0,0,0,142,
		144,7,2,0,0,143,142,1,0,0,0,144,145,1,0,0,0,145,143,1,0,0,0,145,146,1,
		0,0,0,146,153,1,0,0,0,147,149,5,46,0,0,148,150,7,2,0,0,149,148,1,0,0,0,
		150,151,1,0,0,0,151,149,1,0,0,0,151,152,1,0,0,0,152,154,1,0,0,0,153,147,
		1,0,0,0,153,154,1,0,0,0,154,14,1,0,0,0,155,159,5,34,0,0,156,158,9,0,0,
		0,157,156,1,0,0,0,158,161,1,0,0,0,159,160,1,0,0,0,159,157,1,0,0,0,160,
		162,1,0,0,0,161,159,1,0,0,0,162,163,5,34,0,0,163,16,1,0,0,0,164,165,5,
		47,0,0,165,166,5,47,0,0,166,170,1,0,0,0,167,169,8,3,0,0,168,167,1,0,0,
		0,169,172,1,0,0,0,170,168,1,0,0,0,170,171,1,0,0,0,171,173,1,0,0,0,172,
		170,1,0,0,0,173,174,6,8,0,0,174,18,1,0,0,0,175,177,7,4,0,0,176,175,1,0,
		0,0,177,178,1,0,0,0,178,176,1,0,0,0,178,179,1,0,0,0,179,180,1,0,0,0,180,
		181,6,9,0,0,181,20,1,0,0,0,182,183,5,43,0,0,183,22,1,0,0,0,184,185,5,45,
		0,0,185,24,1,0,0,0,186,187,5,42,0,0,187,26,1,0,0,0,188,189,5,47,0,0,189,
		28,1,0,0,0,190,191,5,37,0,0,191,30,1,0,0,0,192,193,5,60,0,0,193,32,1,0,
		0,0,194,195,5,62,0,0,195,34,1,0,0,0,196,197,5,60,0,0,197,198,5,61,0,0,
		198,36,1,0,0,0,199,200,5,62,0,0,200,201,5,61,0,0,201,38,1,0,0,0,202,203,
		5,61,0,0,203,204,5,61,0,0,204,40,1,0,0,0,205,206,5,33,0,0,206,207,5,61,
		0,0,207,42,1,0,0,0,208,209,5,38,0,0,209,210,5,38,0,0,210,44,1,0,0,0,211,
		212,5,124,0,0,212,213,5,124,0,0,213,46,1,0,0,0,214,215,5,33,0,0,215,48,
		1,0,0,0,216,217,5,61,0,0,217,50,1,0,0,0,218,219,5,43,0,0,219,220,5,61,
		0,0,220,52,1,0,0,0,221,222,5,45,0,0,222,223,5,61,0,0,223,54,1,0,0,0,224,
		225,5,42,0,0,225,226,5,61,0,0,226,56,1,0,0,0,227,228,5,47,0,0,228,229,
		5,61,0,0,229,58,1,0,0,0,230,231,5,37,0,0,231,232,5,61,0,0,232,60,1,0,0,
		0,233,234,5,43,0,0,234,235,5,43,0,0,235,62,1,0,0,0,236,237,5,45,0,0,237,
		238,5,45,0,0,238,64,1,0,0,0,239,240,5,59,0,0,240,66,1,0,0,0,241,242,5,
		40,0,0,242,68,1,0,0,0,243,244,5,41,0,0,244,70,1,0,0,0,245,246,5,123,0,
		0,246,72,1,0,0,0,247,248,5,125,0,0,248,74,1,0,0,0,249,250,5,44,0,0,250,
		76,1,0,0,0,9,0,133,139,145,151,153,159,170,178,1,6,0,0
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
