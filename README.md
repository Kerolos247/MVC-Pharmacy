💊 Pharmacy Management System

A full-featured Pharmacy Management System that simulates real-world pharmacy operations.
Designed to help pharmacists efficiently manage medications, prescriptions, patients, and suppliers, with strong validation and real-time alerts.

🚀 Features

🔐 Authentication & Security

 -Secure login system with password reset (Forgot & Reset Password)

 -All actions linked to the logged-in pharmacist

 -Single role: Pharmacist

 -Built using ASP.NET Identity (IdentityUser) for secure user management

 -Brute-force protection: Account is temporarily locked after multiple failed login attempts

 -Email alerts for suspicious activity: Pharmacist notified if someone tries to access their account without authorization

 -Strong password policies: Requires digits, uppercase, lowercase, non-alphanumeric, and minimum 8 characters

 -Input validation & sanitization: Prevents invalid or malicious data entries to reduce vulnerability


🤖Customer Feedback Sentiment Analysis using Custom NLP Model

 -This feature analyzes customer reviews and automatically detects their sentiment (positive or negative) using an NLP model trained on our custom dataset, providing insights to improve customer satisfaction and service quality.

💊 Medication Management

 -Add, update, search, and delete medications

 -Track expiration dates and prevent expired stock

 -Alerts for low-stock and nearly-out-of-stock medications

📝 Prescription Management

 -Add, search, and dispense prescriptions

 -Online prescription upload with paginated display for better performance

 -Automatic total price calculation

 -Discounts via Strategy Pattern

 -Invoice generation using Factory Pattern

🤖 Chatbot Feature (Future)

 -AI-powered chatbot to assist pharmacists with quick queries

 -Provides guidance on medications, prescriptions, and patient management

🧑‍🤝‍🧑 Patient Management

 -Add, search, and delete patients

🚚 Supplier Management

 -Add, search, and delete suppliers

📊 Dashboard

 -Displays total patients, medications, suppliers, and prescriptions

 -Real-time data linked to the currently logged-in pharmacist

⚠️ Smart Alerts & Validation

 -Warnings for low or nearly finished medications

 -Prevents invalid or expired data

 -Strong input validation across the system

🛠 Technologies Used

 -ASP.NET (.NET)

 -MVC Pattern

 -Entity Framework

 -SQL Server

 -JavaScript (Client-side search & UI enhancements)

 -LINQ

 -Email Service for Password Reset

 -ASP.NET Identity (IdentityUser)

 -Dependency Injection

🏗 Architecture

 -MVC & Clean Architecture for clean separation of concerns

 -Scalable and maintainable codebase

 -Dependency Injection for better decoupling and maintainability

🧩 Design Patterns

 -Factory Pattern

 -Centralizes object creation

 -Improves maintainability

 -Makes the system easier to extend

 -Strategy Pattern

 -Handles multiple discount types dynamically

 -Follows Open/Closed Principle for flexibility

🎥 Demo Highlight: This project includes a working demo showing how a pharmacist can reset passwords and the workflow involved. It also illustrates how the system notifies the pharmacist in real-time when someone attempts unauthorized access to their account.

🔗 https://drive.google.com/file/d/14wziabgBlKxHNud8WYIalyb1ykSPFlPR/view?usp=sharing
