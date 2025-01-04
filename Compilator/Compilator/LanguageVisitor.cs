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

        // Visit variable declarations (global and local)
        public override object VisitDeclaration([NotNull] MiniLangParser.DeclarationContext context)
        {
            string variableName = context.IDENTIFIER().GetText();
            string variableTypeText = context.type().GetText();

            var variableType = ParseVariableType(variableTypeText);

            dynamic? value = null;

            if (context.expression() != null)
            {
                string expressionText = context.expression().GetText();
                value = EvaluateExpression(variableType, expressionText);
            }

            if (_programData.CurrentFunction == null)
            {
                // Global variable
                if (_programData.GlobalVariables.Any(v => v.Name == variableName))
                {
                    throw CreateSemanticError($"Global variable '{variableName}' is already declared.");
                }

                _programData.GlobalVariables.Add(new ProgramData.Variable
                {
                    Name = variableName,
                    VariableType = variableType,
                    Value = value
                });
            }
            else
            {
                // Local variable
                var currentFunction = _programData.CurrentFunction;

                if (currentFunction.LocalVariables.Any(v => v.Name == variableName))
                {
                    throw CreateSemanticError($"Local variable '{variableName}' is already declared in function '{currentFunction.Name}'.");
                }

                currentFunction.LocalVariables.Add(new ProgramData.Variable
                {
                    Name = variableName,
                    VariableType = variableType,
                    Value = value
                });

                Console.WriteLine($"Debug: Declared local variable '{variableName}' of type '{variableType}' in function '{currentFunction.Name}'.");
            }

            _programData.AddLexicalUnit("VARIABLE", variableName, context.Start.Line);
            return null;
        }

        // Visit function declarations
        public override object VisitFunctionDeclaration([NotNull] MiniLangParser.FunctionDeclarationContext context)
        {
            string returnType = context.type()?.GetText() ?? "void";
            string functionName = context.IDENTIFIER().GetText();

            if (_programData.Functions.Any(f => f.Name == functionName))
                throw CreateSemanticError($"Function '{functionName}' is already declared.");

            var function = new ProgramData.Function
            {
                Name = functionName,
                ReturnType = ParseVariableType(returnType),
                Body = context.block()
            };

            Console.WriteLine($"Debug: Defining function '{functionName}' with return type '{returnType}'.");

            foreach (var parameter in context.parameterList()?.parameter() ?? Enumerable.Empty<MiniLangParser.ParameterContext>())
            {
                string paramType = parameter.type().GetText();
                string paramName = parameter.IDENTIFIER().GetText();

                if (function.Parameters.Any(p => p.Name == paramName))
                {
                    throw CreateSemanticError($"Parameter '{paramName}' is already declared in function '{functionName}'.");
                }

                function.Parameters.Add(new ProgramData.Variable
                {
                    Name = paramName,
                    VariableType = ParseVariableType(paramType)
                });

                Console.WriteLine($"Debug: Parameter '{paramName}' of type '{paramType}' added to function '{functionName}'.");
            }

            _programData.CurrentFunction = function;

            // Check for recursivity
            function.IsRecursive = IsRecursive(functionName, context.block());

            Visit(context.block());
            _programData.CurrentFunction = null;

            _programData.Functions.Add(function);
            return null;
        }

        // Check if a function is recursive
        private bool IsRecursive(string functionName, MiniLangParser.BlockContext body)
        {
            return body.GetText().Contains(functionName);
        }

        // Visit If statements
        public override object VisitIfStatement([NotNull] MiniLangParser.IfStatementContext context)
        {
            string condition = context.expression().GetText();

            if (!IsBooleanExpression(condition))
            {
                throw CreateSemanticError($"Condition '{condition}' is not valid for an if statement.");
            }

            _programData.CurrentFunction?.ControlStructures.Add($"if (Condition: {condition}) Line {context.Start.Line}");
            Visit(context.block(0));

            if (context.block().Length > 1)
            {
                _programData.CurrentFunction?.ControlStructures.Add($"else Line {context.block(1).Start.Line}");
                Visit(context.block(1));
            }

            return null;
        }

        // Visit While statements
        public override object VisitWhileStatement([NotNull] MiniLangParser.WhileStatementContext context)
        {
            string condition = context.expression().GetText();

            if (!IsBooleanExpression(condition))
            {
                throw CreateSemanticError($"Condition '{condition}' is not valid for a while statement.");
            }

            _programData.CurrentFunction?.ControlStructures.Add($"while (Condition: {condition}) Line {context.Start.Line}");
            Visit(context.block());
            return null;
        }

        // Visit For statements
        public override object VisitForStatement([NotNull] MiniLangParser.ForStatementContext context)
        {
            string condition = context.expression()?.GetText() ?? "Unknown";

            _programData.CurrentFunction?.ControlStructures.Add($"for (Condition: {condition}) Line {context.Start.Line}");
            Visit(context.declaration());
            Visit(context.expression());

            if (context.assignment() != null)
            {
                Visit(context.assignment());
            }
            else if (context.incrementDecrementWithoutSemicolon() != null)
            {
                Visit(context.incrementDecrementWithoutSemicolon());
            }

            Visit(context.block());
            return null;
        }

        // Visit Function Call
        public override object VisitFunctionCall([NotNull] MiniLangParser.FunctionCallContext context)
        {
            string functionName = context.IDENTIFIER().GetText();

            var function = _programData.Functions.FirstOrDefault(f => f.Name == functionName);
            if (function == null)
            {
                throw CreateSemanticError($"Function '{functionName}' is not defined.");
            }

            Console.WriteLine($"Debug: Calling function '{functionName}'.");

            return null;
        }

        private bool IsBooleanExpression(string expression)
        {
            return expression.Contains("==") || expression.Contains("!=") || expression.Contains(">") || expression.Contains("<") || expression.Contains(">=") || expression.Contains("<=");
        }

        private ProgramData.Variable.Type ParseVariableType(string type)
        {
            return type switch
            {
                "int" => ProgramData.Variable.Type.Int,
                "float" => ProgramData.Variable.Type.Float,
                "double" => ProgramData.Variable.Type.Double,
                "string" => ProgramData.Variable.Type.String,
                "void" => ProgramData.Variable.Type.Void,
                _ => throw CreateSemanticError($"Unknown type '{type}'")
            };
        }

        private dynamic? EvaluateExpression(ProgramData.Variable.Type type, string expression)
        {
            var variable = _programData.GlobalVariables.FirstOrDefault(v => v.Name == expression)
                ?? _programData.CurrentFunction?.LocalVariables.FirstOrDefault(v => v.Name == expression);

            if (variable != null)
            {
                if (variable.VariableType != type)
                    throw CreateSemanticError($"Type mismatch: Variable '{expression}' is of type '{variable.VariableType}', expected '{type}'.");

                return variable.Value;
            }

            return type switch
            {
                ProgramData.Variable.Type.Int => int.TryParse(expression, out var intValue) ? intValue : EvaluateArithmeticExpression(expression),
                ProgramData.Variable.Type.Float => float.TryParse(expression, out var floatValue) ? floatValue : null,
                ProgramData.Variable.Type.Double => double.TryParse(expression, out var doubleValue) ? doubleValue : EvaluateArithmeticExpression(expression),
                ProgramData.Variable.Type.String => expression.StartsWith("\"") && expression.EndsWith("\"") ? expression.Trim('"') : null,
                _ => null
            };
        }

        // Simple arithmetic expression evaluation (addition, subtraction, multiplication, division)
        private dynamic? EvaluateArithmeticExpression(string expression)
        {
            try
            {
                var operands = expression.Split(new[] { '+', '-', '*', '/' }, StringSplitOptions.RemoveEmptyEntries);

                var leftOperand = ParseNumber(operands[0]);
                var rightOperand = ParseNumber(operands[1]);

                if (expression.Contains("+")) return leftOperand + rightOperand;
                if (expression.Contains("-")) return leftOperand - rightOperand;
                if (expression.Contains("*")) return leftOperand * rightOperand;
                if (expression.Contains("/")) return rightOperand != 0 ? leftOperand / rightOperand : throw new DivideByZeroException();
            }
            catch
            {
                return null;
            }

            return null;
        }

        // Helper method to parse numbers as int, float, or double
        private dynamic ParseNumber(string value)
        {
            if (int.TryParse(value, out var intValue)) return intValue;
            if (float.TryParse(value, out var floatValue)) return floatValue;
            if (double.TryParse(value, out var doubleValue)) return doubleValue;

            throw new FormatException($"Invalid number format: {value}");
        }
      
        private Exception CreateSemanticError(string message)
        {
            return new Exception($"Semantic Error: {message}");
        }
    }
}
