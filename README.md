🌍 Gift of the Givers II
[![Build Status](https://dev.azure.com/GiftOfGivers12/Disaster%20Alleviation/_apis/build/status%2FLwazi02Hadebe.GiftOfTheGiversII?branchName=master)

A donation and relief management system built with ASP.NET Core MVC, designed to streamline donations, manage relief projects, and provide administrative oversight for humanitarian efforts.

🚀 Features
🔑 Authentication & User Management

Secure login and registration with cookie-based authentication.

Role-based authorization (Admin, User).

Automatic login after registration for a seamless experience.

💸 Donations

Users can make secure monetary donations.

Support for multiple payment methods.

Option to donate anonymously.

Users can view their personal donation history.

🛠️ Admin Dashboard

Overview of total donations, active projects, and registered users.

Manage users and donations.

Create, view, and manage relief projects with associated needs and items.

🏗️ Project Structure

Controllers

AuthController → Handles login, registration, and logout.

DonationController → Manages donation flow and donor history.

AdminController → Provides tools for administrators to manage users, donations, and projects.

Services

IAuthService → Handles authentication and registration logic.

IDonationService → Handles donation creation and retrieval.

Models

ReliefProject, Donation, User, and related entities.

⚙️ Tech Stack

Backend: ASP.NET Core MVC 7+

Database: Entity Framework Core with SQL Server

Authentication: Cookie-based authentication with claims

Frontend: Razor Views

🖥️ Getting Started
1️⃣ Prerequisites

.NET 7 SDK

SQL Server (local or remote)

2️⃣ Clone the Repository
git clone https://github.com/your-repo/GiftOfTheGiversII.git
cd GiftOfTheGiversII

Technology Stack
Backend
ASP.NET MVC: Web application framework

Entity Framework Core: ORM for database operations

Dependency Injection: Built-in IoC container for service management

Security
Cookie Authentication: Secure user authentication

Claims-based Authorization: Fine-grained access control

Role-based Security: Admin and user role separation

Input Validation: Model state validation for all forms

Database
SQL Server: Primary database (inferred from DbContext usage)

Entity Relationships:

Users → Donations

ReliefProjects → ProjectNeeds → Items

Project Structure
Controllers
AdminController: Administrative functions (Admin role required)

AuthController: Authentication and user registration

DonationController: Donation-related operations

HomeController: Public-facing pages and project listings

Services
IAuthService: Authentication business logic

IDonationService: Donation processing and tracking

ApplicationDbContext: Database context and entity management

Models
User: User accounts and profiles

Donation: Donation records and tracking

ReliefProject: Relief project information

ProjectNeed: Project-specific requirements

Item: Donatable items catalog

Key Functionalities
For Regular Users
Register and login to the system

Make monetary donations (anonymous or identified)

View personal donation history

Browse active relief projects

View project details and specific needs

For Administrators
Access admin dashboard with statistics

Manage all user accounts

View and track all donations

Create and manage relief projects

Monitor project status and needs

Security Features
Admin-only access to sensitive administrative functions

User authentication required for donations

Secure password handling

Session management with proper logout functionality

Role-based view restrictions

Getting Started
Prerequisites
.NET 6.0 or later

SQL Server

Visual Studio 2022 or compatible IDE

Installation
Clone the repository

Configure the database connection string in appsettings.json

Run Entity Framework migrations to create the database

Build and run the application

Default Roles
Admin: Full administrative access

User: Standard user permissions for donations and profile management

Usage
Registration: New users can register through the Auth/Register endpoint

Login: Existing users can log in via Auth/Login

Donations: Authenticated users can make donations through Donation/MakeDonation

Project Browsing: All users can view projects via Home/Projects

Admin Access: Administrators can access /Admin/Dashboard for management

Contributing
This project follows standard ASP.NET MVC patterns and can be extended with additional features like:

Email notifications

Advanced reporting

Payment gateway integration

Mobile-responsive views

API endpoints for external integrations

License
This project is designed for charitable organizations and follows open-source principles for non-profit usage.

