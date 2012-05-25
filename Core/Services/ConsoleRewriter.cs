using Roslyn.Compilers.CSharp;

namespace ConsolR.Core.Services
{
	internal sealed class ConsoleRewriter : SyntaxRewriter
	{
		public ConsoleRewriter(string consoleClassName, SemanticModel semanticModel)
		{
			model = semanticModel;
			name = Syntax.IdentifierName(consoleClassName);
		}

		private readonly SemanticModel model;
		private readonly IdentifierNameSyntax name;

		protected override SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
		{
			var info = model.GetSemanticInfo(node);

			var method = (MethodSymbol)info.Symbol;

			if (method != null &&
				(method.ContainingType != null && method.ContainingType.Name == "Console") &&
				(method.ContainingNamespace != null && method.ContainingNamespace.Name == "System") &&
				(method.ContainingAssembly != null && method.ContainingAssembly.Name == "mscorlib"))
			{
				var old = (MemberAccessExpressionSyntax)node.Expression;

				return node.ReplaceNode(old, old.Update(name, old.OperatorToken, old.Name));
			}

			return node;
		}
	}
}