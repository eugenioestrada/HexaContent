using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.MSTestV2;
using HexaContent.Core.Messaging;
using HexaContent.Core.Repositories;
using HexaContent.Infrastructure.Database;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

[TestClass]
public sealed class Infrastructure
{
	private static readonly Architecture Architecture =
		new ArchLoader().LoadAssemblies(
			System.Reflection.Assembly.Load("HexaContent.Core"),
			System.Reflection.Assembly.Load("HexaContent.Infrastructure")
		).Build();

	private readonly IObjectProvider<IType> CoreLayer =
		Types().That().ResideInAssembly(Name("HexaContent.Core"))
		.As("Core Layer");

	private readonly IObjectProvider<IType> InfrastructureLayer = 
		Types().That().ResideInAssembly(Name("HexaContent.Infrastructure"))
		.As("Infrastructure Layer");

	private readonly IObjectProvider<Class> Repositories = 
		Classes().That().AreNotAbstract().And().ImplementInterface(typeof(IRepository))
		.As("Repositories");

	private readonly IObjectProvider<Class> Models =
		Classes().That().AreNotAbstract().And().ResideInNamespace("HexaContent.Core.Model")
		.As("Models");

	private readonly IObjectProvider<Interface> RepositoriesInterfaces = 
		Interfaces().That().ImplementInterface(typeof(IRepository))
		.As("Repositories Interfaces");

	private readonly IObjectProvider<Class> Messages =
		Classes().That().ImplementInterface(typeof(IMessage))
		.As("Messages");

	
	private static string Name(string name) =>
		Architecture.Assemblies.Where(a => a.Name.StartsWith($"{name},")).First().Name;
}
