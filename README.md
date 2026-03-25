# 💊 Pharmacy Management System

A full-featured Pharmacy Management System that simulates real-world pharmacy operations.  
Built to help pharmacists manage medications, prescriptions, patients, and suppliers efficiently, with a strong focus on **security, performance, and scalability**.

---

## 🚀 Key Features

### 🔐 Authentication & Security
- Secure authentication using ASP.NET Identity
- Password reset via email (Forgot & Reset Password)
- Brute-force protection with temporary account lockout
- Email alerts for suspicious login attempts
- Strong password policies and input validation
- All actions linked to the authenticated pharmacist

---

### 🤖 AI-Powered Features
- **Sentiment Analysis (NLP)**  
  Analyzes customer feedback using a custom NLP model to detect positive and negative reviews, enabling better decision-making and service improvement

- **Performance Optimization**  
  Implemented in-memory caching to reduce repeated NLP processing and improve response time

---

### 💊 Medication Management
- Full CRUD operations for medications
- Expiration tracking and validation
- Low-stock and near-expiry alerts

---

### 📝 Prescription Management
- Add, search, and dispense prescriptions
- Online prescription upload (Cloud-based storage)
- Pagination for efficient data handling
- Automatic pricing calculation with dynamic discounts

---

### 🧑‍🤝‍🧑 Patient & Supplier Management
- Manage patients and suppliers with full CRUD functionality

---

### 📊 Dashboard
- Real-time statistics for patients, medications, suppliers, and prescriptions
- Data scoped to the logged-in pharmacist

---

### ⚠️ Smart Validation & Alerts
- Prevents invalid or expired data
- System-wide validation and real-time alerts

---

## 🛠 Tech Stack

- ASP.NET Core (MVC)
- Entity Framework Core
- SQL Server
- LINQ
- ASP.NET Identity
- JavaScript
- FastAPI (Python - AI Integration)
- Cloudinary (File Uploads)
- IMemoryCache (Performance Optimization)

---

## 🏗 Architecture & Design

- Clean Architecture & MVC Pattern
- Dependency Injection for loose coupling
- Scalable and maintainable design

---

## 🧩 Design Patterns

- **Repository Pattern & Unit of Work**
- **Strategy Pattern** (Discount system)
- **Factory Pattern** (Invoice generation)

---

## 🎥 Demo

A working demo showcasing:
- Password reset workflow
- Brute-force protection
- Email notification system

🔗 https://drive.google.com/file/d/14wziabgBlKxHNud8WYIalyb1ykSPFlPR/view?usp=sharing
