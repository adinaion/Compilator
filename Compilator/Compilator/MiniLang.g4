grammar MiniLang;

// Lexical rules
KEYWORD:
	'int'
	| 'float'
	| 'string'
	| 'void'
	| 'double'
	| 'if'
	| 'else'
	| 'for'
	| 'while'
	| 'return';

IDENTIFIER: [a-zA-Z_][a-zA-Z0-9_]*;

NUMBER: [0-9]+ ('.' [0-9]+)?;

STRING: '"' .*? '"';

COMMENT: '//' ~[\r\n]* -> skip;
COMMENT_MULTI: '/*' .*? '*/' -> skip;

WS: [ \t\r\n]+ -> skip;

// Arithmetic operators
ADD: '+';
SUB: '-';
MUL: '*';
DIV: '/';
MOD: '%';

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
MOD_ASSIGN: '%=';

// Increment and decrement operators
INCREMENT: '++';
DECREMENT: '--';

// Delimiters
SEMICOLON: ';';
LPAREN: '(';
RPAREN: ')';
LBRACE: '{';
RBRACE: '}';

COMMA: ',';

// Parser rules
program: globalDeclarations* (functionDeclaration | statement)+;

globalDeclarations: varDeclaration+;

varDeclaration: type IDENTIFIER ASSIGN expression SEMICOLON;

functionDeclaration:
	type IDENTIFIER LPAREN parameterList? RPAREN block;

parameterList: parameter (COMMA parameter)*;

parameter: type IDENTIFIER;

block: LBRACE statement* RBRACE;

statement:
	declaration
	| assignment
	| incrementDecrement
	| ifStatement
	| forStatement
	| whileStatement
	| returnStatement
	| functionCall SEMICOLON;

returnStatement: 'return' expression? SEMICOLON;

declaration: type IDENTIFIER (ASSIGN expression)? SEMICOLON;

assignment:
	IDENTIFIER (
		ASSIGN
		| ADD_ASSIGN
		| SUB_ASSIGN
		| MUL_ASSIGN
		| DIV_ASSIGN
		| MOD_ASSIGN
	) expression SEMICOLON;

incrementDecrement:
	IDENTIFIER INCREMENT SEMICOLON
	| IDENTIFIER DECREMENT SEMICOLON
	| INCREMENT IDENTIFIER SEMICOLON
	| DECREMENT IDENTIFIER SEMICOLON;

ifStatement:
	'if' LPAREN expression RPAREN block ('else' block)?;

forStatement:
	'for' LPAREN declaration expression SEMICOLON assignment RPAREN block;

whileStatement: 'while' LPAREN expression RPAREN block;

expression:
	expression ADD expression		# AddExpr
	| expression SUB expression		# SubExpr
	| expression MUL expression		# MulExpr
	| expression DIV expression		# DivExpr
	| expression MOD expression		# ModExpr
	| expression LT expression		# LessThanExpr
	| expression GT expression		# GreaterThanExpr
	| expression LE expression		# LessEqualExpr
	| expression GE expression		# GreaterEqualExpr
	| expression EQ expression		# EqualExpr
	| expression NE expression		# NotEqualExpr
	| expression AND expression		# AndExpr
	| expression OR expression		# OrExpr
	| NOT expression				# NotExpr
	| LPAREN expression RPAREN		# ParenExpr
	| IDENTIFIER					# IdentifierExpr
	| NUMBER						# NumberExpr
	| STRING						# StringExpr
	| IDENTIFIER ASSIGN expression	# AssignExpr
	| functionCall					# FunctionCallExpr;

functionCall: IDENTIFIER LPAREN argumentList? RPAREN;

argumentList: expression (COMMA expression)*;

// Type rule for variable types
type: 'int' | 'float' | 'string' | 'void';