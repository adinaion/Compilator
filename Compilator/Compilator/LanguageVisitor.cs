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

            // Handle global and local variables separately
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
                    Value = null
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
                    Value = null
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

            // Set the current function
            _programData.CurrentFunction = function;

            // Visit the function body
            Visit(context.block());

            // Reset the current function
            _programData.CurrentFunction = null;

            // Add the function to the list
            _programData.Functions.Add(function);
            return null;
        }

        // Visit If statements
        public override object VisitIfStatement([NotNull] MiniLangParser.IfStatementContext context)
        {
            string condition = context.expression().GetText();

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

        // Helper to create semantic errors
        private Exception CreateSemanticError(string message)
        {
            return new Exception($"Semantic Error: {message}");
        }
    }
}
