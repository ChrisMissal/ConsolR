using Roslyn.Compilers.CSharp;

namespace ConsolR.Core.Extensions
{
	internal static class SyntaxTreeExtensions
	{
		internal static SyntaxTree RewriteWith<TRewriter>(this SyntaxTree tree) where TRewriter : SyntaxRewriter, new()
		{
			return tree.RewriteWith(new TRewriter());
		}

		internal static SyntaxTree RewriteWith<TRewriter>(this SyntaxTree tree, TRewriter rewriter) where TRewriter : SyntaxRewriter
		{
			return SyntaxTree.Create(tree.FileName, (CompilationUnitSyntax)rewriter.Visit(tree.Root), tree.Options);
		}
	}
}