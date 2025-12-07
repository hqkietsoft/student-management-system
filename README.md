# 🎓 Student Management System (Hệ thống Quản lý Sinh viên)

![.NET](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Guna UI](https://img.shields.io/badge/Guna%20UI-3b82f6?style=for-the-badge)

> A comprehensive university administration desktop application built with **C# (WinForms)** and **Entity Framework Core**, featuring a modern UI and robust architecture.

## 📖 About The Project

This project is a Student Management System designed to help educational institutions manage student records, course registrations, and grading efficiently.

**Key Highlight:** The project initially used legacy ADO.NET with raw SQL. I successfully **refactored** the entire data access layer to **Entity Framework Core**, implementing a clean **3-Layer Architecture** (Presentation - Service - Data) to improve maintainability, performance, and security.

### 📸 Screenshots

*(Please add your screenshots here)*

| Login Screen | Dashboard |
|:---:|:---:|
| ![Login](path/to/your/login-image.png) | ![Dashboard](path/to/your/dashboard-image.png) |

| Student Management | Grading System |
|:---:|:---:|
| ![Student](path/to/your/student-image.png) | ![Grades](path/to/your/grades-image.png) |

## 🚀 Key Features

* **Student & Course Management:** CRUD operations for students, teachers, classes, and subjects.
* **Grade Management:** Input grades (Attendance, Mid-term, Final) and **automatically calculate GPA** for semesters.
* **Advanced Search:** Filtering data using **LINQ** for high performance.
* **Reporting:** Export statistical reports (Transcripts, Class Lists) to **Excel** (using EPPlus) and **PDF** (using ReportViewer).
* **Security:** Role-based authentication (Admin/User) with password hashing using **BCrypt**.
* **Modern UI:** Enhanced User Interface/User Experience (UI/UX) using the **Guna UI** framework.

## 🛠 Tech Stack

* **Language:** C# (.NET Framework)
* **GUI Framework:** Windows Forms (WinForms), Guna UI
* **Database:** Microsoft SQL Server
* **ORM:** Entity Framework Core (Database First approach)
* **Libraries:** * `BCrypt.Net` (Security)
    * `EPPlus` (Excel Export)
    * `Microsoft.Reporting.WinForms` (Reporting)

## 🏗 Architecture

The solution is structured following the **3-Layer Architecture**:

1.  **Presentation Layer (Views):** WinForms forms using Guna UI controls. Handles user interaction and calls Services.
2.  **Service Layer (Services):** Contains business logic (e.g., `TinhDiemTBHK`, `AuthService`). It bridges the UI and Data Access.
3.  **Data Access Layer (Models):** Entity Framework Core DbContext and Models generated from the database.

## ⚙️ Installation & Setup

1.  **Clone the repo:**
    ```bash
    git clone [https://github.com/your-username/student-management-system.git](https://github.com/your-username/student-management-system.git)
    ```
2.  **Database Setup:**
    * Open SQL Server Management Studio (SSMS).
    * Run the script `Database/Script.sql` (if provided) or attach the `.mdf` file.
    * Update the **Connection String** in `App.config` or `MyDbContext.cs`:
    ```csharp
    "Data Source=YOUR_SERVER_NAME;Initial Catalog=QUANLYSINHVIEN;Integrated Security=True;TrustServerCertificate=True"
    ```
3.  **Build & Run:**
    * Open the solution in **Visual Studio 2022**.
    * Restore NuGet Packages.
    * Press **F5** to run.

## 👤 Author

**Hà Gia Kiệt**
* **Role:** Backend & Desktop App Developer
* **Education:** IT Graduate (GPA 3.38)
* **Skills:** C#, .NET, Flutter, SQL, Cloud Basics.

---
*If you find this project helpful, please give it a star! ⭐️*
