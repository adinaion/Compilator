grammar MiniLang;

// Lexical rules
KEYWORD:
	'int'
	| 'float'
	| 'string'
	| 'void'
	| 'if'
	| 'else'
	| 'for'
	| 'while'
	| 'return';
IDENTIFIER: [a-zA-Z_][a-zA-Z0-9_]*;
NUMBER: [0-9]+ ('.' [0-9]+)?;
STRING: '"' .*? '"';
COMMENT: '//' ~[\r\n]* -> skip;
WS: [ \t\r\n]+ -> skip;

// Arithmetic operators
ADD: '+';
SUB: '-';
MUL: '*';
DIV: '/';

// Relational operators
LT: '<';
GT: '>';
LE: '<=';
GE: '>=';
EQ: '==';
NE: '!=';

// Logical operators
AND: '&&';
OR: '||';
NOT: '!';

// Assignment operators
ASSIGN: '=';
ADD_ASSIGN: '+=';
SUB_ASSIGN: '-=';
MUL_ASSIGN: '*=';
DIV_ASSIGN: '/=';

// Delimiters
SEMICOLON: ';';
LPAREN: '(';
RPAREN: ')';
LBRACE: '{';
RBRACE: '}';
COMMA: ',';

// Parser rules
program: statement+;

statement:
	declaration
	| assignment
	| ifStatement
	| forStatement
	| whileStatement;

declaration: KEYWORD IDENTIFIER (ASSIGN expression)? SEMICOLON;

assignment:
	IDENTIFIER (
		ASSIGN
		| ADD_ASSIGN
		| SUB_ASSIGN
		| MUL_ASSIGN
		| DIV_ASSIGN
	) expression SEMICOLON;

ifStatement:
	'if' LPAREN expression RPAREN LBRACE statement* RBRACE (
		'else' LBRACE statement* RBRACE
	)?;

forStatement:
	'for' LPAREN declaration expression SEMICOLON assignment RPAREN LBRACE statement* RBRACE;

whileStatement:
	'while' LPAREN expression RPAREN LBRACE statement* RBRACE;

expression:
	expression ADD expression	# AddExpr
	| expression SUB expression	# SubExpr
	| expression MUL expression	# MulExpr
	| expression DIV expression	# DivExpr
	| expression LT expression	# LessThanExpr
	| expression GT expression	# GreaterThanExpr
	| expression LE expression	# LessEqualExpr
	| expression GE expression	# GreaterEqualExpr
	| expression EQ expression	# EqualExpr
	| expression NE expression	# NotEqualExpr
	| expression AND expression	# AndExpr
	| expression OR expression	# OrExpr
	| NOT expression			# NotExpr
	| IDENTIFIER				# IdentifierExpr
	| NUMBER					# NumberExpr
	| STRING					# StringExpr
	| LPAREN expression RPAREN	# ParenExpr;