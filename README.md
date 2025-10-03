Overview
Gift of the Givers II is a comprehensive web application built with ASP.NET MVC that facilitates charitable donations, relief project management, and user administration for a non-profit organization.

Features
🔐 Authentication & Authorization
User Registration & Login: Secure user registration and authentication system

Role-based Access Control: Different access levels for Admin and regular users

Cookie-based Authentication: Persistent login sessions with proper security measures

👥 User Management
User Registration: New users can register with email, password, and personal details

Auto-login: Users are automatically logged in after registration

Profile Management: Users can view and manage their donation history

💰 Donation System
Make Donations: Authenticated users can make monetary donations

Multiple Payment Methods: Support for various payment options

Anonymous Donations: Option to donate anonymously

Donation History: Users can view their donation history

Total Donation Tracking: System-wide donation tracking

🎯 Relief Projects Management
Project Listings: View all active relief projects

Project Details: Detailed view of each project with specific needs

Admin Project Creation: Administrators can create new relief projects

Project Status Tracking: Track active and inactive projects

Project Needs Management: Associate specific items with each project

👨‍💼 Admin Dashboard
Comprehensive Overview: Total donations, user count, and active projects

User Management: View and manage all registered users

Donation Management: Monitor all donations across the platform

Project Administration: Full CRUD operations for relief projects

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

