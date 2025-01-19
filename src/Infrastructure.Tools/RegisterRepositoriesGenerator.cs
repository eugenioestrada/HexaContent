using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace HexaContent.Infrastructure.Tools
{
	[Generator]
	public class RegisterRepositoriesGenerator : IIncrementalGenerator
	{
		public void Initialize(IncrementalGeneratorInitializationContext context)
		{
#if DEBUG
			if (!Debugger.IsAttached)
			{
				Debugger.Launch();
			}
#endif

			IncrementalValuesProvider<ClassDeclarationSyntax> calculatorClassesProvider =
				context.SyntaxProvider.CreateSyntaxProvider(
					predicate: (SyntaxNode node, CancellationToken _) =>
					{
						if (node is ClassDeclarationSyntax classDeclaration)
						{
							var identifier = classDeclaration.Identifier.ToString();
							if (identifier.EndsWith("Repository")
								&& classDeclaration.BaseList.Types.Where(s => s.Type is IdentifierNameSyntax).Select(s => (IdentifierNameSyntax)s.Type).Any(t => t.Identifier.ToString() == "I" + identifier))
							{
								return true;
							}
						}
						return false;
					},
					transform: (GeneratorSyntaxContext ctx, CancellationToken _) =>
					{
						var classDeclaration = (ClassDeclarationSyntax)ctx.Node;
						return classDeclaration;
					});
			context.RegisterSourceOutput(calculatorClassesProvider, (sourceProductionContext, calculatorClass) => Execute(calculatorClass, sourceProductionContext));
		}
		private void Execute(ClassDeclarationSyntax calculatorClass, SourceProductionContext context)
		{
			//Code to perform work on the calculator class

			var code = new StringBuilder();
			code.AppendLine("using Microsoft.Extensions.Hosting;");
			code.AppendLine();
			code.AppendLine("namespace HexaContent.Infrastructure.Extension;");
			code.AppendLine();
			code.AppendLine("public static partial class ApplicationBuilderExtensions");
			code.AppendLine("{");
			code.AppendLine("	public static bool Test(this IHostApplicationBuilder builder) => true;");
			code.AppendLine("}");

			/* namespace HexaContent.Infrastructure.Extension;

/// <summary>
/// Provides extension methods for configuring the application builder.
/// </summary>
public static partial class ApplicationBuilderExtensions*/


			context.AddSource("ApplicationBuilderExtensions.g.cs", code.ToString());
		}
	}
}