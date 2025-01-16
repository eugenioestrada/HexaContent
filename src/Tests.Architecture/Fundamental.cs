using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;
using ArchUnitNET.MSTestV2;
using HexaContent.Core.Messaging;
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

	private readonly IObjectProvider<IType> CoreLayer =
		Types().That().ResideInAssembly(Name("HexaContent.Core"))
		.As("Core Layer");

	private readonly IObjectProvider<IType> InfrastructureLayer = 
		Types().That().ResideInAssembly(Name("HexaContent.Infrastructure"))
		.As("Infrastructure Layer");

	private readonly IObjectProvider<Class> Repositories = 
		Classes().That().ImplementInterface(typeof(IRepository))
		.As("Repositories");

	private readonly IObjectProvider<Interface> RepositoriesPorts = 
		Interfaces().That().ImplementInterface(typeof(IRepository))
		.As("Repositories Ports");

	private readonly IObjectProvider<Class> Messages =
		Classes().That().ImplementInterface(typeof(IMessage))
		.As("Messages");

	[TestMethod]
	public void TypesShouldBeInCorrectLayer()
	{
		Classes().That().Are(Repositories)
			.Should().Be(InfrastructureLayer)
			.Check(Architecture);

		Classes().That().Are(Messages)
			.Should().Be(CoreLayer)
			.Check(Architecture);

		Interfaces().That().Are(RepositoriesPorts)
			.Should().Be(CoreLayer)
			.Check(Architecture);
	}

    [TestMethod]
    public void CoreLayerShouldNotAccessInfrastructureLayer() => 
		Types().That().Are(CoreLayer)
            .Should().NotDependOnAny(InfrastructureLayer)
            .Check(Architecture);


    [TestMethod]
    public void RepositoriesShouldEndWithRepository() => 
		Classes().That().Are(Repositories)
            .Should().HaveNameEndingWith("Repository")
            .Check(Architecture);

    [TestMethod]
    public void MessagesShouldEndWithMessage() =>
		Classes().That().Are(Messages)
            .Should().HaveNameEndingWith("Message")
            .Check(Architecture);

    private static string Name(string name) => 
		Architecture.Assemblies.Where(a => a.Name.StartsWith($"{name},")).First().Name;
}
