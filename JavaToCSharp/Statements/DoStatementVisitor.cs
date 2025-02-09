﻿using com.github.javaparser.ast.stmt;
using JavaToCSharp.Expressions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace JavaToCSharp.Statements
{
    public class DoStatementVisitor : StatementVisitor<DoStmt>
    {
        public override StatementSyntax? Visit(ConversionContext context, DoStmt statement)
        {
            var condition = statement.getCondition();
            var conditionSyntax = ExpressionVisitor.VisitExpression(context, condition);
            if (conditionSyntax is null)
            {
                return null;
            }

            var body = statement.getBody();
            var bodySyntax = VisitStatement(context, body);
            if (bodySyntax is null)
            {
                return null;
            }

            return SyntaxFactory.DoStatement(bodySyntax, conditionSyntax);
        }
    }
}
