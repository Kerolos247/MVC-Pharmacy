Pharmacy Management System

A full-featured Pharmacy Management System that simulates real-world pharmacy operations.
The system is designed to help pharmacists manage medications, prescriptions, suppliers, and patients efficiently, with strong validation and real-time alerts.

🚀 Features
🔐 Authentication & Security 

Secure login system for pharmacists

Password reset via email (Forgot Password & Reset Password)

Each logged-in pharmacist is linked to all actions performed on the system

Single role system (Pharmacist)

Built using ASP.NET Identity (IdentityUser) for secure user management

💊 Medication Management

Add, update, search, and delete medications

Strong validation (e.g. cannot add expired medications)

Alerts for low-stock and nearly-out-of-stock medications

Expiration date tracking

📝 Prescription Management

Add and search prescriptions

Dispense prescriptions

Automatic calculation of total price

Discount handling (holidays, celebrations, special offers)

Print final bill including discounts

Discounts applied dynamically using Strategy Pattern, printing handled efficiently using Factory Pattern

🤖 Chatbot Feature (in Future)

Integrated AI-powered chatbot to assist pharmacists with quick queries

Provides guidance on medication information, prescription handling, and patient management

🧑‍🤝‍🧑 Patient Management

Add new patients

Search for patients

Delete patients

🚚 Supplier Management

Add suppliers

Search suppliers

Delete suppliers

📊 Dashboard

A dynamic dashboard that displays:

Total number of patients

Total number of medications

Total number of suppliers

Total number of prescriptions

All dashboard data is real-time and linked to the currently logged-in pharmacist.

⚠️ Smart Alerts & Validation

Warnings for low or nearly finished medications

Prevent adding invalid or expired data

Strong input validation across the entire system

🏗 Architecture

MVC Architecture

Layered Architecture

Clean separation of concerns

Scalable and maintainable codebase

Dependency Injection for better decoupling and maintainability

🧩 Design Patterns Used
🏭 Factory Pattern

Used to simplify and centralize object creation

Improves maintainability by separating instantiation logic from business logic

Makes the system easier to extend when adding new entities

🎯 Strategy Pattern

Used to handle multiple discount types dynamically

Allows applying different discount strategies (e.g. holidays, celebrations, special offers) without modifying existing code

Follows the Open/Closed Principle and improves system flexibility

🛠 Technologies Used

ASP.NET (.NET)

MVC Pattern

Entity Framework

SQL Server

JavaScript (Client-side search & UI enhancements)

Email Service for Password Reset

ASP.NET Identity (IdentityUser) for secure user management

Dependency Injection

🎥 Demo

A full demo video will be shared on LinkedIn.

🔗 Demo Link: (Coming Soon)
