using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// RabbitMQ

var rabbitmq = builder.AddRabbitMQ("messaging")
	.WithManagementPlugin();

// MySQL

var mysql = builder.AddMySql("mysql")
	.WithPhpMyAdmin()
	.WithLifetime(ContainerLifetime.Persistent);

var mysqldb = mysql.AddDatabase("mysqldb");

// Content Hub

builder.AddProject<HexaContent_ContentHub>("hub")
	.WithReference(rabbitmq)
	.WithReference(mysqldb);

// Static Forge

builder.AddProject<HexaContent_StaticForge>("forge")
	.WithReference(rabbitmq)
	.WithReference(mysqldb);

builder.Build().Run();
