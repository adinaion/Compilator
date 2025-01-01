using Antlr4.Runtime.Misc;
using System;
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

        public override object VisitDeclaration([NotNull] MiniLangParser.DeclarationContext context)
        {
            string type = context.type().GetText();
            string name = context.IDENTIFIER().GetText();
            var value = context.expression()?.GetText();

            // Parsează tipul variabilei
            var variableType = ParseVariableType(type);

            // Verifică dacă este în context global sau local
            if (_programData.CurrentFunction == null)
            {
                // Variabilă globală
                if (_programData.GlobalVariables.Any(v => v.Name == name))
                {
                    throw new Exception($"Semantic Error: Variable '{name}' is already declared globally.");
                }

                _programData.GlobalVariables.Add(new ProgramData.Variable
                {
                    Name = name,
                    VariableType = variableType,
                    Value = EvaluateExpression(variableType, value)
                });
            }
            else
            {
                // Variabilă locală
                var currentFunction = _programData.CurrentFunction;
                if (currentFunction.LocalVariables.Any(v => v.Name == name))
                {
                    throw new Exception($"Semantic Error: Variable '{name}' is already declared in function '{currentFunction.Name}'.");
                }

                currentFunction.LocalVariables.Add(new ProgramData.Variable
                {
                    Name = name,
                    VariableType = variableType,
                    Value = EvaluateExpression(variableType, value)
                });
            }

            return null;
        }

        public override object VisitFunctionDeclaration([NotNull] MiniLangParser.FunctionDeclarationContext context)
        {
            string returnType = context.type()?.GetText() ?? "Void";
            string functionName = context.IDENTIFIER().GetText();

            var function = new ProgramData.Function
            {
                Name = functionName,
                ReturnType = ParseVariableType(returnType)
            };

            // Parametrii funcției
            foreach (var parameter in context.parameterList()?.parameter() ?? Enumerable.Empty<MiniLangParser.ParameterContext>())
            {
                string paramType = parameter.type().GetText();
                string paramName = parameter.IDENTIFIER().GetText();

                function.Parameters.Add(new ProgramData.Variable
                {
                    Name = paramName,
                    VariableType = ParseVariableType(paramType)
                });
            }

            // Verifică dacă funcția este recursivă
            function.IsRecursive = context.block().GetText().Contains(functionName);

            // Adaugă funcția la lista globală
            if (_programData.Functions.Any(f => f.Name == functionName))
            {
                throw new Exception($"Semantic Error: Function '{functionName}' is already declared.");
            }

            _programData.Functions.Add(function);

            // Setează funcția curentă și vizitează blocul
            _programData.CurrentFunction = function;
            Visit(context.block());
            _programData.CurrentFunction = null;

            return null;
        }

        public override object VisitAssignment([NotNull] MiniLangParser.AssignmentContext context)
        {
            string variableName = context.IDENTIFIER().GetText();
            var variable = _programData.GlobalVariables.FirstOrDefault(v => v.Name == variableName)
                           ?? _programData.CurrentFunction?.LocalVariables.FirstOrDefault(v => v.Name == variableName);

            if (variable == null)
            {
                throw new Exception($"Semantic Error: Variable '{variableName}' is not declared.");
            }

            string expression = context.expression().GetText();

            // Evaluează expresia și verifică tipul
            var evaluatedValue = EvaluateExpression(variable.VariableType, expression);
            if (evaluatedValue == null)
            {
                throw new Exception($"Semantic Error: Type mismatch in assignment to '{variableName}'.");
            }

            variable.Value = evaluatedValue;

            return null;
        }

        public override object VisitIfStatement([NotNull] MiniLangParser.IfStatementContext context)
        {
            string condition = context.expression().GetText();

            // Verifică dacă condiția este validă
            if (!IsBooleanExpression(condition))
            {
                throw new Exception($"Semantic Error: Condition '{condition}' is not valid for an if statement.");
            }

            // Adaugă structura de control
            _programData.CurrentFunction?.ControlStructures.Add("if");

            // Vizitează blocurile de cod
            Visit(context.block(0));
            if (context.block().Length > 1)
            {
                Visit(context.block(1));
            }

            return null;
        }

        public override object VisitWhileStatement([NotNull] MiniLangParser.WhileStatementContext context)
        {
            string condition = context.expression().GetText();

            // Verifică condiția
            if (!IsBooleanExpression(condition))
            {
                throw new Exception($"Semantic Error: Condition '{condition}' is not valid for a while statement.");
            }

            // Adaugă structura de control
            _programData.CurrentFunction?.ControlStructures.Add("while");

            // Vizitează blocul
            Visit(context.block());

            return null;
        }

        private ProgramData.Variable.Type ParseVariableType(string type)
        {
            return type switch
            {
                "int" => ProgramData.Variable.Type.Int,
                "float" => ProgramData.Variable.Type.Float,
                "string" => ProgramData.Variable.Type.String,
                "void" => ProgramData.Variable.Type.Void,
                _ => throw new Exception($"Semantic Error: Unknown type '{type}'.")
            };
        }

        private dynamic? EvaluateExpression(ProgramData.Variable.Type variableType, string expression)
        {
            return variableType switch
            {
                ProgramData.Variable.Type.Int => int.TryParse(expression, out var intValue) ? intValue : null,
                ProgramData.Variable.Type.Float => float.TryParse(expression, out var floatValue) ? floatValue : null,
                ProgramData.Variable.Type.String => expression.StartsWith("\"") && expression.EndsWith("\"") ? expression.Trim('"') : null,
                _ => null
            };
        }

        private bool IsBooleanExpression(string expression)
        {
            // Logica de validare pentru expresii booleene
            return expression == "true" || expression == "false" || expression.Contains("==") || expression.Contains("!=");
        }
    }
}
