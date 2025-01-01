using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using MiniLang;

namespace MiniLang
{
    public class LanguageVisitor : MiniLangBaseVisitor<object>
    {
        private readonly ProgramData _programData;

        public LanguageVisitor(ProgramData programData)
        {
            _programData = programData;
        }

        public override object VisitVarDeclaration(MiniLangParser.VarDeclarationContext context)
        {
            var variableType = ParseVariableType(context.type().GetText());
            var variableName = context.IDENTIFIER().GetText();

            // Check for variable duplication
            if (_programData.GlobalVariables.Any(v => v.Name == variableName))
            {
                throw CreateSemanticError($"Variable '{variableName}' is already declared.");
            }

            // Add variable to global variables
            _programData.GlobalVariables.Add(new ProgramData.Variable
            {
                VariableType = variableType,
                Name = variableName,
                Value = null // Initial value
            });

            // Add lexical unit
            _programData.AddLexicalUnit("VARIABLE", variableName, context.Start.Line);

            return base.VisitVarDeclaration(context);
        }

        // Centralized error handling
        private Exception CreateSemanticError(string message)
        {
            return new Exception($"Semantic Error: {message}");
        }

        // Simplified variable lookup
        private ProgramData.Variable GetVariable(string name)
        {
            var variable = _programData.GlobalVariables.FirstOrDefault(v => v.Name == name)
                ?? _programData.CurrentFunction?.LocalVariables.FirstOrDefault(v => v.Name == name);

            if (variable == null)
                throw CreateSemanticError($"Variable '{name}' is not declared.");

            return variable;
        }

        // Helper to parse variable types
        private ProgramData.Variable.Type ParseVariableType(string type)
        {
            return type switch
            {
                "int" => ProgramData.Variable.Type.Int,
                "float" => ProgramData.Variable.Type.Float,
                "string" => ProgramData.Variable.Type.String,
                "void" => ProgramData.Variable.Type.Void,
                _ => throw CreateSemanticError($"Unknown type '{type}'")
            };
        }

        // Evaluate expressions for different types
        private dynamic? EvaluateExpression(ProgramData.Variable.Type type, string expression)
        {
            return type switch
            {
                ProgramData.Variable.Type.Int => int.TryParse(expression, out var intValue) ? intValue : EvaluateArithmeticExpression(expression),
                ProgramData.Variable.Type.Float => float.TryParse(expression, out var floatValue) ? floatValue : null,
                ProgramData.Variable.Type.String => expression.StartsWith("\"") && expression.EndsWith("\"") ? expression.Trim('"') : null,
                _ => null
            };
        }

        // Simple arithmetic expression evaluation (addition, subtraction, multiplication, division)
        private int? EvaluateArithmeticExpression(string expression)
        {
            try
            {
                var operands = expression.Split(new[] { '+', '-', '*', '/' }, StringSplitOptions.RemoveEmptyEntries);
                var leftOperand = int.Parse(operands[0]);
                var rightOperand = int.Parse(operands[1]);

                if (expression.Contains("+")) return leftOperand + rightOperand;
                if (expression.Contains("-")) return leftOperand - rightOperand;
                if (expression.Contains("*")) return leftOperand * rightOperand;
                if (expression.Contains("/")) return leftOperand / rightOperand;
            }
            catch
            {
                return null; // If parsing or operation fails, return null
            }

            return null;
        }

        // Validate boolean expressions
        private bool IsBooleanExpression(string expression) =>
            expression == "true" || expression == "false" || expression.Contains("==") || expression.Contains("!=") ||
            expression.Contains("&&") || expression.Contains("||");

        // Visit function declaration and handle parameters
        public override object VisitFunctionDeclaration([NotNull] MiniLangParser.FunctionDeclarationContext context)
        {
            string returnType = context.type()?.GetText() ?? "void";
            string functionName = context.IDENTIFIER().GetText();

            // Check if function is already declared
            if (_programData.Functions.Any(f => f.Name == functionName))
                throw CreateSemanticError($"Function '{functionName}' is already declared.");

            var function = new ProgramData.Function
            {
                Name = functionName,
                ReturnType = ParseVariableType(returnType)
            };

            // Add function parameters and ensure uniqueness
            foreach (var parameter in context.parameterList()?.parameter() ?? Enumerable.Empty<MiniLangParser.ParameterContext>())
            {
                string paramType = parameter.type().GetText();
                string paramName = parameter.IDENTIFIER().GetText();

                // Ensure parameter name uniqueness within the function
                if (function.Parameters.Any(p => p.Name == paramName))
                {
                    throw CreateSemanticError($"Parameter '{paramName}' is already declared in function '{functionName}'.");
                }

                function.Parameters.Add(new ProgramData.Variable
                {
                    Name = paramName,
                    VariableType = ParseVariableType(paramType)
                });
            }

            // Set current function and visit block
            _programData.Functions.Add(function);
            _programData.CurrentFunction = function;
            Visit(context.block());
            _programData.CurrentFunction = null;

            return null;
        }

        public override object VisitAssignment([NotNull] MiniLangParser.AssignmentContext context)
        {
            string variableName = context.IDENTIFIER().GetText();
            var variable = GetVariable(variableName);

            string expression = context.expression().GetText();
            var evaluatedValue = EvaluateExpression(variable.VariableType, expression);

            if (evaluatedValue == null)
                throw CreateSemanticError($"Type mismatch in assignment to '{variableName}'.");

            variable.Value = evaluatedValue;

            return null;
        }

        // Handle If statement
        public override object VisitIfStatement([NotNull] MiniLangParser.IfStatementContext context)
        {
            string condition = context.expression().GetText();

            if (!IsBooleanExpression(condition))
                throw CreateSemanticError($"Condition '{condition}' is not valid for an if statement.");

            Visit(context.block(0));
            if (context.block().Length > 1)
                Visit(context.block(1));

            return null;
        }

        // Handle While statement
        public override object VisitWhileStatement([NotNull] MiniLangParser.WhileStatementContext context)
        {
            string condition = context.expression().GetText();

            if (!IsBooleanExpression(condition))
                throw CreateSemanticError($"Condition '{condition}' is not valid for a while statement.");

            Visit(context.block());
            return null;
        }

        // Consolidate adding variable logic into one method
        private void AddVariable(string name, string type, string value = null)
        {
            var variableType = ParseVariableType(type);
            if (_programData.GlobalVariables.Any(v => v.Name == name))
                throw CreateSemanticError($"Variable '{name}' is already declared.");

            _programData.GlobalVariables.Add(new ProgramData.Variable
            {
                Name = name,
                VariableType = variableType,
                Value = EvaluateExpression(variableType, value)
            });
        }

        // Override VisitDeclaration to handle both global and local variable declarations
        public override object VisitDeclaration([NotNull] MiniLangParser.DeclarationContext context)
        {
            string name = context.IDENTIFIER().GetText();
            string type = context.type().GetText();
            string value = context.expression()?.GetText();

            if (_programData.CurrentFunction == null)
            {
                // Check for shadowing global variable with local function parameter
                if (_programData.GlobalVariables.Any(v => v.Name == name))
                {
                    throw CreateSemanticError($"Variable '{name}' is already declared in the global scope.");
                }

                AddVariable(name, type, value);
            }
            else
            {
                var currentFunction = _programData.CurrentFunction;

                // Ensure parameter uniqueness in the current function's local scope
                if (currentFunction.LocalVariables.Any(v => v.Name == name))
                    throw CreateSemanticError($"Variable '{name}' is already declared in function '{currentFunction.Name}'.");

                currentFunction.LocalVariables.Add(new ProgramData.Variable
                {
                    Name = name,
                    VariableType = ParseVariableType(type),
                    Value = EvaluateExpression(ParseVariableType(type), value)
                });
            }

            return null;
        }
    }
}
