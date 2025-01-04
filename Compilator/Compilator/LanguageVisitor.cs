using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using MiniLang;
using System.Xml.Linq;

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

            if (_programData.GlobalVariables.Any(v => v.Name == variableName))
            {
                throw CreateSemanticError($"Variable '{variableName}' is already declared.");
            }

            _programData.GlobalVariables.Add(new ProgramData.Variable
            {
                VariableType = variableType,
                Name = variableName,
                Value = null 
            });

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

            if (variable.Value == null)
            {
                throw CreateSemanticError($"Variable '{name}' is declared but not initialized.");
            }

            return variable;
        }

        // Helper to parse variable types
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

        // Evaluate expressions for different types
        private dynamic? EvaluateExpression(ProgramData.Variable.Type type, string expression)
        {
            Console.WriteLine($"Debug: Evaluating expression '{expression}' for type '{type}'.");

            // Detectăm dacă este un apel de funcție
            if (expression.Contains("(") && expression.Contains(")"))
            {
                return EvaluateFunctionCall(type, expression);
            }

            var variable = _programData.GlobalVariables.FirstOrDefault(v => v.Name == expression)
                ?? _programData.CurrentFunction?.LocalVariables.FirstOrDefault(v => v.Name == expression);

            if (variable != null)
            {
                Console.WriteLine($"Debug: Found variable '{variable.Name}' with value '{variable.Value}' and type '{variable.VariableType}'.");

                if (variable.VariableType != type)
                    throw CreateSemanticError($"Type mismatch: Variable '{expression}' is of type '{variable.VariableType}', expected '{type}'.");

                return variable.Value;
            }

            return type switch
            {
                ProgramData.Variable.Type.Int => int.TryParse(expression, out var intValue) ? intValue : EvaluateArithmeticExpression(expression),
                ProgramData.Variable.Type.Float => float.TryParse(expression, out var floatValue) ? floatValue : null,
                ProgramData.Variable.Type.Double => double.TryParse(expression, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var doubleValue)
                    ? doubleValue
                    : EvaluateArithmeticExpression(expression),
                ProgramData.Variable.Type.String => expression.StartsWith("\"") && expression.EndsWith("\"") ? expression.Trim('"') : null,
                _ => null
            };
        }

        // Evaluation of function calls
        private dynamic? EvaluateFunctionCall(ProgramData.Variable.Type expectedType, string expression)
        {
            // Extragem numele funcției și parametrii
            var openParenIndex = expression.IndexOf('(');
            var closeParenIndex = expression.LastIndexOf(')');
            if (openParenIndex == -1 || closeParenIndex == -1 || openParenIndex > closeParenIndex)
            {
                throw CreateSemanticError($"Invalid function call syntax: '{expression}'");
            }

            var functionName = expression.Substring(0, openParenIndex).Trim();
            var arguments = expression.Substring(openParenIndex + 1, closeParenIndex - openParenIndex - 1)
                .Split(',')
                .Select(arg => arg.Trim())
                .ToList();

            // Găsim funcția
            var function = _programData.Functions.FirstOrDefault(f => f.Name == functionName);
            if (function == null)
            {
                throw CreateSemanticError($"Function '{functionName}' is not defined.");
            }

            if (function.Parameters.Count != arguments.Count)
            {
                throw CreateSemanticError($"Function '{functionName}' expects {function.Parameters.Count} arguments, but {arguments.Count} were provided.");
            }

            // Validăm și evaluăm parametrii
            for (int i = 0; i < arguments.Count; i++)
            {
                var expectedParamType = function.Parameters[i].VariableType;
                var evaluatedArgument = EvaluateExpression(expectedParamType, arguments[i]);

                if (evaluatedArgument == null)
                {
                    throw CreateSemanticError($"Type mismatch for argument {i + 1} in function '{functionName}'. Expected type: '{expectedParamType}'.");
                }
            }

            // Validăm tipul de returnare
            if (function.ReturnType != expectedType)
            {
                throw CreateSemanticError($"Type mismatch in function call to '{functionName}'. Expected return type '{expectedType}', but function returns '{function.ReturnType}'.");
            }

            // Simulăm o valoare de întoarcere (aici trebuie să implementăm logica pentru execuția funcției, dacă este necesar)
            return function.ReturnType switch
            {
                ProgramData.Variable.Type.Int => 42, // Exemplu de valoare întoarsă
                ProgramData.Variable.Type.Float => 42.0f,
                ProgramData.Variable.Type.Double => 42.0,
                ProgramData.Variable.Type.String => "Example",
                _ => null
            };
        }


        // Simple arithmetic expression evaluation (addition, subtraction, multiplication, division)
        private dynamic? EvaluateArithmeticExpression(string expression)
        {
            Console.WriteLine($"Debug: Evaluating arithmetic expression '{expression}'.");

            try
            {
                // Validare: Expresia trebuie să conțină un operator aritmetic
                if (!expression.Contains("+") && !expression.Contains("-") &&
                    !expression.Contains("*") && !expression.Contains("/"))
                {
                    throw new FormatException($"Invalid arithmetic expression: '{expression}'");
                }

                // Split și validare: Expresia trebuie să aibă exact doi operanzi
                var operators = new[] { '+', '-', '*', '/' };
                var operatorIndex = expression.IndexOfAny(operators);

                if (operatorIndex == -1)
                {
                    throw new FormatException($"No operator found in expression: '{expression}'");
                }

                // Extragem operanzii și operatorul
                var leftOperand = expression.Substring(0, operatorIndex).Trim();
                var rightOperand = expression.Substring(operatorIndex + 1).Trim();
                var operatorChar = expression[operatorIndex];

                // Obținem valorile operanzilor
                var leftValue = GetOperandValue(leftOperand);
                var rightValue = GetOperandValue(rightOperand);

                // Evaluăm expresia în funcție de operator
                return operatorChar switch
                {
                    '+' => leftValue + rightValue,
                    '-' => leftValue - rightValue,
                    '*' => leftValue * rightValue,
                    '/' => rightValue != 0 ? leftValue / rightValue : throw new DivideByZeroException(),
                    _ => throw new InvalidOperationException($"Unknown operator '{operatorChar}' in expression '{expression}'")
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: Failed to evaluate arithmetic expression '{expression}': {ex.Message}");
                return null;
            }
        }

        // Helper pentru obținerea valorii unui operand
        private dynamic GetOperandValue(string operand)
        {
            // Dacă operandul este o variabilă, obținem valoarea acesteia
            var variable = _programData.GlobalVariables.FirstOrDefault(v => v.Name == operand)
                ?? _programData.CurrentFunction?.LocalVariables.FirstOrDefault(v => v.Name == operand);

            if (variable != null)
            {
                if (variable.Value == null)
                {
                    throw CreateSemanticError($"Variable '{operand}' is declared but not initialized.");
                }
                return variable.Value;
            }

            // Dacă operandul este un număr, îl parsăm
            return ParseNumber(operand);
        }


        // Helper method to parse numbers as int, float, or double
        private dynamic ParseNumber(string value)
        {
            if (int.TryParse(value, out var intValue)) return intValue;
            if (float.TryParse(value, out var floatValue)) return floatValue;
            if (double.TryParse(value, out var doubleValue)) return doubleValue;

            throw new FormatException($"Invalid number format: {value}");
        }

        // Validate boolean expressions
        private bool IsBooleanExpression(string expression)
        {
            return expression.Contains("==") || expression.Contains("!=") ||
                   expression.Contains(">=") || expression.Contains("<=") ||
                   expression.Contains(">") || expression.Contains("<");
        }


        // Visit function declaration and handle parameters
        public override object VisitFunctionDeclaration([NotNull] MiniLangParser.FunctionDeclarationContext context)
        {
            string returnType = context.type()?.GetText() ?? "void";
            string functionName = context.IDENTIFIER().GetText();

            if (_programData.Functions.Any(f => f.Name == functionName))
                throw CreateSemanticError($"Function '{functionName}' is already declared.");

            var function = new ProgramData.Function
            {
                Name = functionName,
                ReturnType = ParseVariableType(returnType)
            };

            Console.WriteLine($"Debug: Defining function '{functionName}' with return type '{returnType}'.");

            foreach (var parameter in context.parameterList()?.parameter() ?? Enumerable.Empty<MiniLangParser.ParameterContext>())
            {
                string paramType = parameter.type().GetText();
                string paramName = parameter.IDENTIFIER().GetText();

                Console.WriteLine($"Debug: Adding parameter '{paramName}' of type '{paramType}' to function '{functionName}'.");

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

            _programData.Functions.Add(function);
            _programData.CurrentFunction = function;

            Visit(context.block());

            Console.WriteLine($"Debug: Function '{functionName}' analysis completed.");

            _programData.CurrentFunction = null;

            return null;
        }


        public override object VisitAssignment([NotNull] MiniLangParser.AssignmentContext context)
        {
            string variableName = context.IDENTIFIER().GetText();
            var variable = GetVariable(variableName);

            string expression = context.expression().GetText();
            
            Console.WriteLine($"Debug: Assigning to variable '{variableName}' of type '{variable.VariableType}'.");
            Console.WriteLine($"Debug: Expression: '{expression}'.");

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

            _programData.CurrentFunction.ControlStructures.Add($"if Line {context.Start.Line}");

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

            _programData.CurrentFunction.ControlStructures.Add($"while Line {context.Start.Line}");

            Visit(context.block());
            return null;
        }

        // Handle For statement
        public override object VisitForStatement([NotNull] MiniLangParser.ForStatementContext context)
        {
            _programData.CurrentFunction.ControlStructures.Add($"for Line {context.Start.Line}");

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

            Console.WriteLine($"Debug: variable '{name}' of type '{type}' with {value}");

            if (_programData.CurrentFunction == null)
            {
                if (_programData.GlobalVariables.Any(v => v.Name == name))
                {
                    throw CreateSemanticError($"Variable '{name}' is already declared in the global scope.");
                }

                AddVariable(name, type, value);
            }
            else
            {
                var currentFunction = _programData.CurrentFunction;

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
