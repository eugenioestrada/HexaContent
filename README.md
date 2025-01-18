# HexaContent

## Project Description

HexaContent is a hybrid architecture designed for content management systems (CMS) in media organizations. It combines microservices for management with a headless architecture that uses pre-generation for static content and dynamic modules injected at runtime. This modular and scalable approach prioritizes flexibility, availability, security, and cost reduction. The proposal addresses issues of traditional CMS, such as complexity, low performance, and high operational costs, by integrating modern technologies to optimize load times, SEO, and scalability while adapting to changing market demands.

## Purpose and Goals

The primary purpose of HexaContent is to provide a robust and efficient content management system that can handle the dynamic needs of media organizations. The goals of the project include:
- Enhancing flexibility and scalability
- Improving performance and load times
- Reducing operational costs
- Ensuring high availability and security
- Optimizing SEO and adapting to market demands

## Architecture and Technologies Used

HexaContent employs a hybrid architecture that combines microservices and a headless CMS approach. The key components and technologies used in the project include:
- **Microservices**: For managing different aspects of the CMS
- **Headless CMS**: For pre-generating static content and injecting dynamic modules at runtime
- **RabbitMQ**: For messaging and communication between services
- **MySQL**: For database management
- **Redis**: For caching and improving performance
- **Minio**: For object storage
- **.NET 9.0**: For building and running the application

## Setup and Running Instructions

To set up and run the HexaContent project, follow these steps:

1. **Clone the repository**:
   ```sh
   git clone https://github.com/eugenioestrada/HexaContent.git
   cd HexaContent
   ```

2. **Set up the environment**:
   Ensure you have the required tools and dependencies installed, including .NET 9.0, MySQL, RabbitMQ, Redis, and Minio.

3. **Restore dependencies**:
   ```sh
   dotnet restore
   ```

4. **Build the project**:
   ```sh
   dotnet build
   ```

5. **Run the application**:
   ```sh
   dotnet run --project src/AppHost/HexaContent.AppHost.csproj
   ```

6. **Access the application**:
   Open your browser and navigate to `http://localhost:15131` or `https://localhost:17208` to access the HexaContent application.

## Contributing

We welcome contributions to the HexaContent project. To contribute, follow these steps:

1. **Fork the repository**:
   Click the "Fork" button at the top right corner of the repository page.

2. **Create a new branch**:
   ```sh
   git checkout -b feature/your-feature-name
   ```

3. **Make your changes**:
   Implement your feature or fix the issue.

4. **Commit your changes**:
   ```sh
   git commit -m "Add your commit message here"
   ```

5. **Push to your branch**:
   ```sh
   git push origin feature/your-feature-name
   ```

6. **Create a pull request**:
   Open a pull request from your forked repository to the main repository.

Please ensure your code follows the project's coding standards and includes appropriate tests.

Thank you for contributing to HexaContent!
