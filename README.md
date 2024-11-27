# ECommerce Project

## Overview

The ECommerce project is a full-stack web application designed to facilitate online shopping. It employs a layered architecture that
 separates concerns between the presentation layer, business logic, and data access. 
 This README provides the architecture used, technologies implemented, and instructions for setup and usage.

## Architecture

This project follows a **Multi-Layered Architecture** (also known as **N-Tier Architecture**), which separates the application into distinct layers:

1. **Presentation Layer**: 
   - The frontend built with Angular serves as the user interface, allowing users to interact with the application.
   - It handles user inputs, displays data, and communicates with the backend API.

2. **Service Layer**:
   - The backend, built with .NET Core, contains services that encapsulate the business logic of the application.
   - This layer interacts with the data access layer to perform operations and return results to the presentation layer.

3. **Data Access Layer**:
   - The DAL (Data Access Layer) uses Entity Framework Core to interact with the database.
   - It contains repositories that abstract the data access logic and entities that represent the database schema.

4. **API Layer**:
   - The API layer exposes endpoints for the frontend to communicate with the backend services, enabling CRUD operations for products 
   and user authentication.

## Technologies Used

- **Backend**: 
  - .NET Core
  - Entity Framework Core
  - ASP.NET Core Web API

- **Frontend**: 
  - Angular
  - RxJS
  - NgRx for state management

- **Database** - **Database**: SQL Server (or any other database you are using)
- **Other Tools**: Swagger for API documentation, JWT for authentication, etc.

## Installation

### Backend

1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd ECommerce

2. Navigate to the ECommerce.DAL project and run the migrations:
   cd ECommerce.DAL
   dotnet ef database update

3. Run the backend API:
   dotnet run

### Frontend

1. cd app
2. npm install
3. ng serve

### Usage
Once both the backend and frontend are running, you can access the application in your web browser at http://localhost:4200. 
You can interact with the API through the frontend interface, which allows users to register, log in, and browse products.

### Contributing
Contributions are welcome! If you would like to contribute to this project, please follow these steps:

1. Fork the repository.
2. Create a new branch (git checkout -b feature/YourFeature).
3. Make your changes and commit them (git commit -m 'Add some feature').
4. Push to the branch (git push origin feature/YourFeature).
5. Open a pull request.

### License
This project is licensed under the MIT License - see the LICENSE file for details.
