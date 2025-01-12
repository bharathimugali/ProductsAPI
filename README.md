# ProductAPI

## Overview
ProductAPI is a .NET Core Web API application designed for managing products. The application demonstrates modern development practices such as Entity Framework Core with code-first migrations, SQL Server integration, sequences for unique ID generation, and robust error handling. This project also includes structured layers for clean architecture and unit tests written with NUnit.

---

## Features
- **Product Management**: Add, update, delete, retrieve, and manage product stock.
- **Database Integration**: SQL Server used for persistent storage, with Entity Framework Core (EF Core) for ORM.
- **Auto-generated IDs**: Unique 6-digit product IDs using database sequences.
- **Validation**: Data annotations and custom validation for input.
- **Error Handling**: Middleware for unexpected errors and structured handling of domain-specific exceptions.
- **Testing**: Unit tests written using NUnit and Moq.
- **Swagger UI**: API documentation and testing.

---

## Technologies Used
- **Framework**: .NET 8
- **Database**: SQL Server
- **ORM**: Entity Framework Core (Code First)
- **Unit Testing**: NUnit, Moq
- **API Documentation**: Swagger

---

## Folder Structure
```
ProductsAPI
|-- Controllers
|-- Data
    |-- Entities
    |-- DbContext
|-- Infrastructure
    |-- CustomExceptions
    |-- Middlewares
|-- Models
    |-- RequestDto
    |-- ResponseDto
|-- Repositories
|-- Services
Tests (ProductAPI.Tests)
```
### Key Folders:
- **Controllers**: Contains API controllers for handling HTTP requests.
- **Data**: Includes EF Core entities and configurations.
- **DTOs**: Data Transfer Objects for request and response models.
- **Middleware**: Custom middleware for global error handling.
- **Repositories**: Implements data access logic.
- **Services**: Business logic for products.
- **Tests**: Unit tests for controllers and services.

---

## Database Setup
### Sequence for Product ID
The `ProductIdSequence` ensures unique 6-digit IDs for products.

## Setup Instructions
1. **Clone the Repository**:

   git clone <repository-url>
   cd ProductAPI

2. **Update the Database Connection**:
   Modify `appsettings.json` to include your SQL Server connection string:

   "ConnectionStrings": {
       "DefaultConnection": "Server=<your-server>;Database=ProductDb;Trusted_Connection=True;TrustServerCertificate=True;"
   }

3. **Run Migrations**:
   Execute the following commands in the terminal:

   dotnet ef migrations add InitialCreate
   dotnet ef database update

   Package Manager Console (Visual Studio): If you are using Visual Studio, you can run the migrations from the Package Manager Console:

  Open the Package Manager Console from Tools > NuGet Package Manager > Package Manager Console.
  Run the following commands:

  Add-Migration "Initial Migration"
  Update-Database

4. **Run the Application**:
   ```bash
   dotnet run
   ```
5. **Access Swagger**:
   Open your browser and navigate to `https://localhost:<port>/swagger` to view and test the API.

---

## API Endpoints
### Product Endpoints:
- **GET** `/api/products` - Retrieve all products.
- **GET** `/api/products/{id}` - Retrieve a product by ID.
- **POST** `/api/products` - Add a new product.
- **PUT** `/api/products/{id}` - Update a product.
- **DELETE** `/api/products/{id}` - Delete a product.
- **PUT** `/api/products/decrement-stock/{id}/{quantity}` - Decrement stock.
- **PUT** `/api/products/add-to-stock/{id}/{quantity}` - Add to stock.

---

## Testing
Unit tests are located in the `ProductAPI.Tests` project.

## Contributing
1. Fork the repository.
2. Create a feature branch.
3. Commit your changes.
4. Push to the branch.
5. Open a pull request.

---

## Future Enhancements
- Add authentication and authorization.
- Implement pagination for product retrieval.
- Integrate caching for improved performance.
- Expand test coverage for edge cases.

---

