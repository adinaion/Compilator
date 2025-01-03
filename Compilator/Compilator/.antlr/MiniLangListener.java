// Generated from d:/Facultate/Anul 2/Semestrul 1/LFC/Teme Obligatorii/Tema2/Compilator/Compilator/Compilator/MiniLang.g4 by ANTLR 4.13.1
import org.antlr.v4.runtime.tree.ParseTreeListener;

/**
 * This interface defines a complete listener for a parse tree produced by
 * {@link MiniLangParser}.
 */
public interface MiniLangListener extends ParseTreeListener {
	/**
	 * Enter a parse tree produced by {@link MiniLangParser#program}.
	 * @param ctx the parse tree
	 */
	void enterProgram(MiniLangParser.ProgramContext ctx);
	/**
	 * Exit a parse tree produced by {@link MiniLangParser#program}.
	 * @param ctx the parse tree
	 */
	void exitProgram(MiniLangParser.ProgramContext ctx);
	/**
	 * Enter a parse tree produced by {@link MiniLangParser#globalDeclarations}.
	 * @param ctx the parse tree
	 */
	void enterGlobalDeclarations(MiniLangParser.GlobalDeclarationsContext ctx);
	/**
	 * Exit a parse tree produced by {@link MiniLangParser#globalDeclarations}.
	 * @param ctx the parse tree
	 */
	void exitGlobalDeclarations(MiniLangParser.GlobalDeclarationsContext ctx);
	/**
	 * Enter a parse tree produced by {@link MiniLangParser#varDeclaration}.
	 * @param ctx the parse tree
	 */
	void enterVarDeclaration(MiniLangParser.VarDeclarationContext ctx);
	/**
	 * Exit a parse tree produced by {@link MiniLangParser#varDeclaration}.
	 * @param ctx the parse tree
	 */
	void exitVarDeclaration(MiniLangParser.VarDeclarationContext ctx);
	/**
	 * Enter a parse tree produced by {@link MiniLangParser#functionDeclaration}.
	 * @param ctx the parse tree
	 */
	void enterFunctionDeclaration(MiniLangParser.FunctionDeclarationContext ctx);
	/**
	 * Exit a parse tree produced by {@link MiniLangParser#functionDeclaration}.
	 * @param ctx the parse tree
	 */
	void exitFunctionDeclaration(MiniLangParser.FunctionDeclarationContext ctx);
	/**
	 * Enter a parse tree produced by {@link MiniLangParser#parameterList}.
	 * @param ctx the parse tree
	 */
	void enterParameterList(MiniLangParser.ParameterListContext ctx);
	/**
	 * Exit a parse tree produced by {@link MiniLangParser#parameterList}.
	 * @param ctx the parse tree
	 */
	void exitParameterList(MiniLangParser.ParameterListContext ctx);
	/**
	 * Enter a parse tree produced by {@link MiniLangParser#parameter}.
	 * @param ctx the parse tree
	 */
	void enterParameter(MiniLangParser.ParameterContext ctx);
	/**
	 * Exit a parse tree produced by {@link MiniLangParser#parameter}.
	 * @param ctx the parse tree
	 */
	void exitParameter(MiniLangParser.ParameterContext ctx);
	/**
	 * Enter a parse tree produced by {@link MiniLangParser#block}.
	 * @param ctx the parse tree
	 */
	void enterBlock(MiniLangParser.BlockContext ctx);
	/**
	 * Exit a parse tree produced by {@link MiniLangParser#block}.
	 * @param ctx the parse tree
	 */
	void exitBlock(MiniLangParser.BlockContext ctx);
	/**
	 * Enter a parse tree produced by {@link MiniLangParser#statement}.
	 * @param ctx the parse tree
	 */
	void enterStatement(MiniLangParser.StatementContext ctx);
	/**
	 * Exit a parse tree produced by {@link MiniLangParser#statement}.
	 * @param ctx the parse tree
	 */
	void exitStatement(MiniLangParser.StatementContext ctx);
	/**
	 * Enter a parse tree produced by {@link MiniLangParser#returnStatement}.
	 * @param ctx the parse tree
	 */
	void enterReturnStatement(MiniLangParser.ReturnStatementContext ctx);
	/**
	 * Exit a parse tree produced by {@link MiniLangParser#returnStatement}.
	 * @param ctx the parse tree
	 */
	void exitReturnStatement(MiniLangParser.ReturnStatementContext ctx);
	/**
	 * Enter a parse tree produced by {@link MiniLangParser#declaration}.
	 * @param ctx the parse tree
	 */
	void enterDeclaration(MiniLangParser.DeclarationContext ctx);
	/**
	 * Exit a parse tree produced by {@link MiniLangParser#declaration}.
	 * @param ctx the parse tree
	 */
	void exitDeclaration(MiniLangParser.DeclarationContext ctx);
	/**
	 * Enter a parse tree produced by {@link MiniLangParser#assignment}.
	 * @param ctx the parse tree
	 */
	void enterAssignment(MiniLangParser.AssignmentContext ctx);
	/**
	 * Exit a parse tree produced by {@link MiniLangParser#assignment}.
	 * @param ctx the parse tree
	 */
	void exitAssignment(MiniLangParser.AssignmentContext ctx);
	/**
	 * Enter a parse tree produced by {@link MiniLangParser#incrementDecrementWithoutSemicolon}.
	 * @param ctx the parse tree
	 */
	void enterIncrementDecrementWithoutSemicolon(MiniLangParser.IncrementDecrementWithoutSemicolonContext ctx);
	/**
	 * Exit a parse tree produced by {@link MiniLangParser#incrementDecrementWithoutSemicolon}.
	 * @param ctx the parse tree
	 */
	void exitIncrementDecrementWithoutSemicolon(MiniLangParser.IncrementDecrementWithoutSemicolonContext ctx);
	/**
	 * Enter a parse tree produced by {@link MiniLangParser#incrementDecrementWithSemicolon}.
	 * @param ctx the parse tree
	 */
	void enterIncrementDecrementWithSemicolon(MiniLangParser.IncrementDecrementWithSemicolonContext ctx);
	/**
	 * Exit a parse tree produced by {@link MiniLangParser#incrementDecrementWithSemicolon}.
	 * @param ctx the parse tree
	 */
	void exitIncrementDecrementWithSemicolon(MiniLangParser.IncrementDecrementWithSemicolonContext ctx);
	/**
	 * Enter a parse tree produced by {@link MiniLangParser#ifStatement}.
	 * @param ctx the parse tree
	 */
	void enterIfStatement(MiniLangParser.IfStatementContext ctx);
	/**
	 * Exit a parse tree produced by {@link MiniLangParser#ifStatement}.
	 * @param ctx the parse tree
	 */
	void exitIfStatement(MiniLangParser.IfStatementContext ctx);
	/**
	 * Enter a parse tree produced by {@link MiniLangParser#forStatement}.
	 * @param ctx the parse tree
	 */
	void enterForStatement(MiniLangParser.ForStatementContext ctx);
	/**
	 * Exit a parse tree produced by {@link MiniLangParser#forStatement}.
	 * @param ctx the parse tree
	 */
	void exitForStatement(MiniLangParser.ForStatementContext ctx);
	/**
	 * Enter a parse tree produced by {@link MiniLangParser#whileStatement}.
	 * @param ctx the parse tree
	 */
	void enterWhileStatement(MiniLangParser.WhileStatementContext ctx);
	/**
	 * Exit a parse tree produced by {@link MiniLangParser#whileStatement}.
	 * @param ctx the parse tree
	 */
	void exitWhileStatement(MiniLangParser.WhileStatementContext ctx);
	/**
	 * Enter a parse tree produced by the {@code MulExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterMulExpr(MiniLangParser.MulExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code MulExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitMulExpr(MiniLangParser.MulExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code AndExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterAndExpr(MiniLangParser.AndExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code AndExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitAndExpr(MiniLangParser.AndExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code StringExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterStringExpr(MiniLangParser.StringExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code StringExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitStringExpr(MiniLangParser.StringExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code EqualExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterEqualExpr(MiniLangParser.EqualExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code EqualExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitEqualExpr(MiniLangParser.EqualExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code GreaterThanExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterGreaterThanExpr(MiniLangParser.GreaterThanExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code GreaterThanExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitGreaterThanExpr(MiniLangParser.GreaterThanExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code SubExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterSubExpr(MiniLangParser.SubExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code SubExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitSubExpr(MiniLangParser.SubExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code GreaterEqualExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterGreaterEqualExpr(MiniLangParser.GreaterEqualExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code GreaterEqualExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitGreaterEqualExpr(MiniLangParser.GreaterEqualExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code AddExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterAddExpr(MiniLangParser.AddExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code AddExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitAddExpr(MiniLangParser.AddExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code NotEqualExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterNotEqualExpr(MiniLangParser.NotEqualExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code NotEqualExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitNotEqualExpr(MiniLangParser.NotEqualExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code OrExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterOrExpr(MiniLangParser.OrExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code OrExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitOrExpr(MiniLangParser.OrExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code AssignExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterAssignExpr(MiniLangParser.AssignExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code AssignExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitAssignExpr(MiniLangParser.AssignExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code LessEqualExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterLessEqualExpr(MiniLangParser.LessEqualExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code LessEqualExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitLessEqualExpr(MiniLangParser.LessEqualExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code DivExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterDivExpr(MiniLangParser.DivExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code DivExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitDivExpr(MiniLangParser.DivExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code FunctionCallExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterFunctionCallExpr(MiniLangParser.FunctionCallExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code FunctionCallExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitFunctionCallExpr(MiniLangParser.FunctionCallExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code NumberExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterNumberExpr(MiniLangParser.NumberExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code NumberExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitNumberExpr(MiniLangParser.NumberExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code IdentifierExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterIdentifierExpr(MiniLangParser.IdentifierExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code IdentifierExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitIdentifierExpr(MiniLangParser.IdentifierExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code NotExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterNotExpr(MiniLangParser.NotExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code NotExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitNotExpr(MiniLangParser.NotExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code ModExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterModExpr(MiniLangParser.ModExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code ModExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitModExpr(MiniLangParser.ModExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code ParenExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterParenExpr(MiniLangParser.ParenExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code ParenExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitParenExpr(MiniLangParser.ParenExprContext ctx);
	/**
	 * Enter a parse tree produced by the {@code LessThanExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void enterLessThanExpr(MiniLangParser.LessThanExprContext ctx);
	/**
	 * Exit a parse tree produced by the {@code LessThanExpr}
	 * labeled alternative in {@link MiniLangParser#expression}.
	 * @param ctx the parse tree
	 */
	void exitLessThanExpr(MiniLangParser.LessThanExprContext ctx);
	/**
	 * Enter a parse tree produced by {@link MiniLangParser#functionCall}.
	 * @param ctx the parse tree
	 */
	void enterFunctionCall(MiniLangParser.FunctionCallContext ctx);
	/**
	 * Exit a parse tree produced by {@link MiniLangParser#functionCall}.
	 * @param ctx the parse tree
	 */
	void exitFunctionCall(MiniLangParser.FunctionCallContext ctx);
	/**
	 * Enter a parse tree produced by {@link MiniLangParser#argumentList}.
	 * @param ctx the parse tree
	 */
	void enterArgumentList(MiniLangParser.ArgumentListContext ctx);
	/**
	 * Exit a parse tree produced by {@link MiniLangParser#argumentList}.
	 * @param ctx the parse tree
	 */
	void exitArgumentList(MiniLangParser.ArgumentListContext ctx);
	/**
	 * Enter a parse tree produced by {@link MiniLangParser#type}.
	 * @param ctx the parse tree
	 */
	void enterType(MiniLangParser.TypeContext ctx);
	/**
	 * Exit a parse tree produced by {@link MiniLangParser#type}.
	 * @param ctx the parse tree
	 */
	void exitType(MiniLangParser.TypeContext ctx);
}