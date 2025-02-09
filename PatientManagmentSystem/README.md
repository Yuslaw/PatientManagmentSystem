# Patient Management API

## Project Description

This project is a **CRUD API application** developed using **ASP.NET Core** with an **N-tier architecture** to manage patients and their records. The application allows users to perform operations such as creating, retrieving, updating, and soft-deleting patient data and managing associated records. The system uses an in-memory database (**SQLite**) for storage, making it lightweight and ideal for demonstration purposes.

Additionally, the application is designed with scalability and maintainability in mind. It integrates **Swagger** for API documentation, supports containerization through a **Dockerfile**, and includes unit tests for core functionalities.

---

## Thought Process and Design Decisions

### 1. N-tier Architecture
- **Reason:** This architecture separates concerns, ensuring scalability and easier maintenance. Each layer has a specific responsibility:
  - **Controllers:** Handle HTTP requests and responses.
  - **Services:** Contain business logic and communicate with the repository layer.
  - **Repositories/Database Layer:** Handle data persistence using Entity Framework Core.
- **Benefit:** Future updates to any layer can be implemented without affecting others, enhancing modularity.

### 2. In-memory Database
- **Choice:** SQLite (configured as in-memory for simplicity).
- **Reason:** This database is lightweight, requires minimal setup, and is perfect for development and testing purposes.
- **Benefit:** Avoids the complexity of setting up a production-grade database while allowing smooth transitions to other databases like SQL Server or PostgreSQL.

### 3. Swagger Integration
- **Reason:** Provides interactive documentation for the API, allowing developers to easily explore and test endpoints.
- **Benefit:** Enhances developer experience, especially for clients consuming the API.

### 4. Soft Deletion
- **Choice:** Implemented a `IsDeleted` field for the `Patient` entity.
- **Reason:** Preserves historical data while marking resources as inactive instead of permanently removing them.
- **Benefit:** Ensures data integrity and allows recovery of accidentally deleted records.

### 5. Validation of IDs
- **Improvement:** IDs are auto-generated for both patients and records.
- **Reason:** Reduces potential errors caused by incorrect or duplicate IDs in requests.
- **Benefit:** Follows RESTful API best practices and ensures consistent ID management.

### 6. Unit Tests
- **Reason:** Ensures reliability by verifying key functionalities, such as:
  - Creating, retrieving, updating, and deleting patients.
  - Creating and retrieving patient records.
- **Benefit:** Early detection of bugs and confidence in code quality.

### 7. Containerization
- **Reason:** Added a Dockerfile to enable easy deployment across various environments.
- **Benefit:** Ensures the application runs consistently across development, testing, and production environments.

---

## Key Endpoints

### Patient Endpoints
- `GET /api/patients` - Retrieve all patients.
- `GET /api/patients/{id}` - Retrieve a specific patient.
- `POST /api/patients` - Create a new patient.
- `PUT /api/patients/{id}` - Update an existing patient.
- `DELETE /api/patients/{id}` - Soft-delete a patient.

### Patient Records Endpoints
- `GET /api/patients/{patientId}/records` - Retrieve records of a specific patient.
- `POST /api/patients/{patientId}/records` - Create a record for a specific patient.
- `PUT /api/patients/{patientId}/records/{id}` - Update a record for a specific patient.

---

## How to Run the Project

1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd <repository-folder>

2. Build and Run the Application Using Docker
```bash
docker build -t patient-management-api .
docker run -p 8080:80 patient-management-api

## Access the API Documentation

- Visit [http://localhost:8080/swagger](http://localhost:8080/swagger) in your browser.

---

## Testing

Unit tests are written using **xUnit** to validate the following functionalities:

- Creation, retrieval, update, and soft-deletion of patients.
- Association of records with patients and retrieval of records.

### Run the Tests
```bash
dotnet test

