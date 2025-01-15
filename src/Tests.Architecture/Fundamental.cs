using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;
using ArchUnitNET.MSTestV2;
using HexaContent.Core.Repositories;
using System.Reflection;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

[TestClass]
public sealed class Fundamental
{
	private static readonly Architecture Architecture =
		new ArchLoader().LoadAssemblies(
			System.Reflection.Assembly.Load("HexaContent.Core"),
			System.Reflection.Assembly.Load("HexaContent.Infrastructure")
		).Build();

	private readonly IObjectProvider<IType> CoreLayer = Types()
			.That()
			.ResideInAssembly(FindName("HexaContent.Core"))
			.As("Core Layer");

	private readonly IObjectProvider<IType> InfrastructureLayer = Types()
		.That()
		.ResideInAssembly(FindName("HexaContent.Infrastructure"))
		.As("Infrastructure Layer");

	private readonly IObjectProvider<Class> Repositories = Classes()
		.That()
		.ImplementInterface(typeof(IRepository))
		.As("Repositories");

	private readonly IObjectProvider<Interface> RepositoriesPorts = Interfaces().That().ImplementInterface(typeof(IRepository)).As("Repositories Ports");
	private static string FindName(string name) => Architecture.Assemblies.Where(a => a.Name.StartsWith($"{name},")).First().Name;

	[TestMethod]
	public void TypesShouldBeInCorrectLayer()
	{
		IArchRule repositoriesShouldBeInInfrastructureLayer = 
			Classes()
			.That()
			.Are(Repositories)
			.Should()
			.Be(InfrastructureLayer);

		repositoriesShouldBeInInfrastructureLayer.Check(Architecture);

		IArchRule repositoryPortsShouldBeCoreLayer =
			Interfaces()
			.That()
			.Are(RepositoriesPorts)
			.Should()
			.Be(CoreLayer);

		repositoryPortsShouldBeCoreLayer.Check(Architecture);
	}

	[TestMethod]
	public void CoreLayerShouldNotAccessInfrastructureLayer()
	{
		IArchRule coreLayerShouldNotAccessInfrastructureLayer = Types().That()
			.Are(CoreLayer).Should().NotDependOnAny(InfrastructureLayer);

		coreLayerShouldNotAccessInfrastructureLayer.Check(Architecture);
	}


	[TestMethod]
	public void RepositoriesShouldEndWithRepository()
	{
		IArchRule repositoriesShouldEndWithRepository = Classes()
				.That()
				.ImplementInterface(typeof(IRepository))
				.Should()
				.HaveNameEndingWith("Repository");

		repositoriesShouldEndWithRepository.Check(Architecture);
	}
}
