💊 Pharmacy Management System

A full-featured Pharmacy Management System that simulates real-world pharmacy operations.
Designed to help pharmacists efficiently manage medications, prescriptions, patients, and suppliers, with strong validation and real-time alerts.

🚀 Features
🔐 Authentication & Security

- Secure login system with password reset (Forgot & Reset Password)

- All actions linked to the logged-in pharmacist

- Single role: Pharmacist

- Built using ASP.NET Identity (IdentityUser) for secure user management

💊 Medication Management

- Add, update, search, and delete medications

- Track expiration dates and prevent expired stock

- Alerts for low-stock and nearly-out-of-stock medications

📝 Prescription Management

- Add, search, and dispense prescriptions

- Automatic total price calculation

- Discounts applied dynamically using Strategy Pattern

- Print final bills efficiently using Factory Pattern

🤖 Chatbot Feature (Future)

- AI-powered chatbot to assist pharmacists with quick queries

- Provides guidance on medications, prescriptions, and patient management

🧑‍🤝‍🧑 Patient Management

- Add, search, and delete patients

🚚 Supplier Management

- Add, search, and delete suppliers

📊 Dashboard

- Displays total patients, medications, suppliers, and prescriptions

- Real-time data linked to the currently logged-in pharmacist

⚠️ Smart Alerts & Validation

- Warnings for low or nearly finished medications

- Prevents invalid or expired data

- Strong input validation across the system

🛠 Technologies Used

- ASP.NET (.NET)

- MVC Pattern

- Entity Framework

- SQL Server

- JavaScript (Client-side search & UI enhancements)

- LINQ

- Email Service for Password Reset

- ASP.NET Identity (IdentityUser)

- Dependency Injection

🏗 Architecture

- MVC & Layered Architecture for clean separation of concerns

- Scalable and maintainable codebase

- Dependency Injection for better decoupling and maintainability

🧩 Design Patterns

 Factory Pattern

- Centralizes object creation

- Improves maintainability

- Makes the system easier to extend

🎯 Strategy Pattern

- Handles multiple discount types dynamically

- Follows Open/Closed Principle for flexibility

🎥 Demo: Shows pharmacist password reset functionality and workflow

-🔗 Demo Link: https://drive.google.com/file/d/1wDp0H-OAbrGYtAECVJpgPTsHOwm9VT5l/view?usp=drive_link
