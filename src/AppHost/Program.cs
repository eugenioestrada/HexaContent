using Aspire.Hosting;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// RabbitMQ

var rabbitmq = builder.AddRabbitMQ("messaging")
	.WithManagementPlugin();

// MySQL

var mysql = builder.AddMySql("mysql")
	.WithPhpMyAdmin();

var mysqldb = mysql.AddDatabase("mysqldb");

// Redis

var redis = builder.AddRedis("redis");

// Minio

var storage = builder.AddMinio("storage");

var bucket = storage.AddBucket("bucket");

// Content Hub

builder.AddProject<HexaContent_ContentHub>("hub")
	.WithReference(rabbitmq)
	.WithReference(mysqldb)
	.WithReference(bucket)
	.WaitFor(rabbitmq)
	.WaitFor(mysqldb)
	.WaitFor(bucket);

// Static Forge

builder.AddProject<HexaContent_StaticForge>("forge")
	.WithReference(rabbitmq)
	.WithReference(mysqldb)
	.WithReference(storage)
	.WithReference(bucket)
	.WaitFor(rabbitmq)
	.WaitFor(mysqldb)
	.WaitFor(bucket);


// Edge Proxy

builder.AddProject<HexaContent_EdgeProxy>("proxy")
	.WithReference(bucket)
	.WaitFor(bucket);

builder.Build().Run();
