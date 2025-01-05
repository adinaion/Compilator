using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using MiniLang;
using System.Globalization;
using Antlr4.Runtime;

namespace MiniLang
{
    public class LanguageVisitor : MiniLangBaseVisitor<object>
    {
        private readonly ProgramData _programData;

        public LanguageVisitor(ProgramData programData)
        {
            _programData = programData;
        }

        public override object VisitDeclaration([NotNull] MiniLangParser.DeclarationContext context)
        {
            string variableName = context.IDENTIFIER().GetText();
            string variableTypeText = context.type().GetText();

            var variableType = ParseVariableType(variableTypeText);

            dynamic? value = null;

            if (_programData.CurrentFunction != null)
            {
                var currentFunction = _programData.CurrentFunction;
                if (currentFunction.Parameters.Any(p => p.Name == variableName))
                {
                    throw CreateSemanticError($"Variable '{variableName}' cannot be declared inside function '{currentFunction.Name}' because it is already a parameter.");
                }
            }

            if (context.expression() != null)
            {
                string expressionText = context.expression().GetText();
                value = EvaluateExpression(variableType, expressionText);
            }

            if (_programData.CurrentFunction == null)
            {
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

            }

            _programData.AddLexicalUnit("VARIABLE", variableName, context.Start.Line);
            return null;
        }

        public override object VisitFunctionDeclaration([NotNull] MiniLangParser.FunctionDeclarationContext context)
        {
            string returnType = context.type()?.GetText() ?? "void";
            string functionName = context.IDENTIFIER().GetText();

            if (string.IsNullOrWhiteSpace(functionName))
            {
                throw CreateSemanticError("Function name cannot be empty.");
            }

            if (_programData.Functions.Any(f => f.Name == functionName))
            {
                throw CreateSemanticError($"Function '{functionName}' is already declared.");
            }

            var function = new ProgramData.Function
            {
                Name = functionName,
                ReturnType = ParseVariableType(returnType),
                Body = context.block()
            };

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
            }

            _programData.CurrentFunction = function;

            function.IsRecursive = IsRecursive(functionName, context.block());

            Visit(context.block());
            _programData.CurrentFunction = null;

            _programData.Functions.Add(function);
            Console.WriteLine($"Function added: {functionName}, ReturnType: {function.ReturnType}");
            return null;
        }

        private bool IsRecursive(string functionName, MiniLangParser.BlockContext body)
        {
            return body.GetText().Contains(functionName);
        }

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

        public override object VisitFunctionCall([NotNull] MiniLangParser.FunctionCallContext context)
        {
            string functionName = context.IDENTIFIER().GetText();
            Console.WriteLine($"Visiting function call: {functionName}");

            var function = _programData.Functions.FirstOrDefault(f => f.Name == functionName);
            if (function == null)
            {
                throw CreateSemanticError($"Function '{functionName}' is not defined.");
            }

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
            return type switch
            {
                ProgramData.Variable.Type.Int => int.TryParse(expression, out var intValue) ? intValue : EvaluateArithmeticExpression(type,expression),
                ProgramData.Variable.Type.Float => float.TryParse(expression, NumberStyles.Float, CultureInfo.InvariantCulture, out var floatValue) ? floatValue : EvaluateArithmeticExpression(type,expression),
                ProgramData.Variable.Type.Double => double.TryParse(expression, NumberStyles.Float, CultureInfo.InvariantCulture, out var doubleValue) ? doubleValue : EvaluateArithmeticExpression(type,expression),
                ProgramData.Variable.Type.String => expression.StartsWith("\"") && expression.EndsWith("\"") ? expression.Trim('"') : null,
                _ => throw CreateSemanticError($"Unsupported type '{type}' for expression.")
            };
        }

        private dynamic? EvaluateArithmeticExpression(ProgramData.Variable.Type type,string expression)
        {
            try
            {
                var operands = expression.Split(new[] { '+', '-', '*', '/', '%' }, StringSplitOptions.RemoveEmptyEntries);

                var leftOperand = EvaluateOperand(operands[0].Trim(), type);
                var rightOperand = EvaluateOperand(operands[1].Trim(), type);

                if (expression.Contains("+")) return leftOperand + rightOperand;
                if (expression.Contains("-")) return leftOperand - rightOperand;
                if (expression.Contains("*")) return leftOperand * rightOperand;
                if (expression.Contains("/")) return rightOperand != 0 ? leftOperand / rightOperand : throw new DivideByZeroException();
                if (expression.Contains("%"))
                {
                    if (rightOperand == 0)
                    {
                        throw new DivideByZeroException("Modulo by zero is not allowed.");
                    }

                    if (leftOperand is int && rightOperand is int)
                    {
                        return leftOperand % rightOperand;
                    }
                    else
                    {
                        throw new InvalidOperationException("Modulo operation is only supported for integers.");
                    }
                }
            }
            catch
            {
                return null;
            }

            return null;

        }

        private dynamic EvaluateOperand(string operand, ProgramData.Variable.Type expectedType)
        {
            var variable = _programData.GlobalVariables.FirstOrDefault(v => v.Name == operand)
                ?? _programData.CurrentFunction?.LocalVariables.FirstOrDefault(v => v.Name == operand);

            if (variable != null)
            {
                if (variable.Value == null)
                {
                    throw CreateSemanticError($"Variable '{operand}' is uninitialized.");
                }

                if (variable.VariableType != expectedType)
                {
                    throw CreateSemanticError($"Type mismatch: Variable '{operand}' is of type '{variable.VariableType}', expected '{expectedType}'.");
                }

                return variable.Value;
            }

            return ParseNumber(operand);
        }

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
