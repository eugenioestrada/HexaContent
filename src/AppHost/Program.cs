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
	.WaitFor(bucket)
	.WithUrlForEndpoint("http", u => u.DisplayText = "Content Hub (HTTP)")
	.WithUrlForEndpoint("https", u => u.DisplayText = "Content Hub (HTTPS)");

// Static Forge

builder.AddProject<HexaContent_StaticForge>("forge")
	.WithReference(rabbitmq)
	.WithReference(mysqldb)
	.WithReference(bucket)
	.WaitFor(rabbitmq)
	.WaitFor(mysqldb)
	.WaitFor(bucket);


// Dynamic Bridge

var bridge = builder.AddProject<HexaContent_DynamicBridge>("bridge")
	.WithReference(mysqldb)
	.WaitFor(mysqldb)
	.WithUrlForEndpoint("http", u => u.DisplayText = "Dynamic Bridge (HTTP)")
	.WithUrlForEndpoint("https", u => u.DisplayText = "Dynamic Bridge (HTTPS)");


// Edge Proxy

var proxy = builder.AddProject<HexaContent_EdgeProxy>("proxy")
	.WithReference(bucket)
	.WithReference(bridge)
	.WaitFor(bucket)
	.WithUrlForEndpoint("http", u => u.DisplayText = "Proxy (HTTP)")
	.WithUrlForEndpoint("https", u => u.DisplayText = "Proxy (HTTPS)");

// Varnish

// builder.AddVarnish("varnish");

builder.AddVarnish("varnish")
	.WithReference(bridge)
	.WithReference(proxy)
	.WaitFor(bridge)
	.WaitFor(proxy)
	.WithUrlForEndpoint("http", u => u.DisplayText = "Varnish (HTTP)")	
	.WithUrlForEndpoint("https", u => u.DisplayText = "Varnish (HTTPS)");

builder.Build().Run();
