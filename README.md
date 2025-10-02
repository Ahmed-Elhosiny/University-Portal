# 🎓 University Portal - Advanced Student Management System

[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-9.0-blue.svg)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-14.0-purple.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-purple.svg)](https://getbootstrap.com/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg)](CONTRIBUTING.md)

> **Enterprise-grade university management system** built with ASP.NET Core MVC. A complete solution for managing students, courses, and academic operations with a modern, intuitive interface and powerful analytics.

**🌟 Perfect for portfolios, learning projects, and real-world academic management needs.**

![Dashboard Preview](Screenshots/dashboard.png)

## ✨ Key Features

### 📚 Core Functionality
- ✅ **Complete CRUD Operations** - Full Create, Read, Update, Delete for Students & Courses
- 🔍 **Advanced Search & Filtering** - Multi-criteria search with real-time results
- 📊 **Interactive Dashboard** - Real-time statistics with Chart.js visualizations
- 🖼️ **Image Management** - Profile picture upload with validation (JPG, PNG, GIF - Max 5MB)
- 📱 **Fully Responsive** - Mobile-first design that works on all devices
- 📄 **Smart Pagination** - Efficient data handling for large datasets
- 💾 **Database Seeding** - Auto-populate with sample data (50 students & 15 courses)

### 📈 Reports & Analytics
- 📥 **Data Export** - Export students and courses to CSV format
- 📊 **Summary Reports** - Comprehensive overview reports in TXT format
- 📋 **Detailed Analytics** - Course enrollment statistics and student demographics
- 🎯 **Custom Reports** - Student details grouped by courses

### 🎨 Modern UI/UX
- 🌈 **Gradient Design** - Beautiful color schemes and modern aesthetics
- ✨ **Smooth Animations** - Fade-in, slide-in, and hover effects
- 🎭 **Glass Morphism** - Modern card designs with backdrop blur
- 🔄 **Loading States** - Professional loading overlays and skeletons
- 🍞 **Toast Notifications** - Non-intrusive success/error messages
- 📜 **Scroll to Top** - Smooth scrolling with floating button

## 🚀 Quick Start

### Prerequisites
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- [SQL Server 2019+](https://www.microsoft.com/sql-server) or SQL Server Express

### Installation

```bash
# Clone repository
git clone https://github.com/Ahmed-Elhosiny/university-portal.git
cd university-portal

# Restore packages
dotnet restore

# Update connection string in appsettings.json
"DefaultConnection": "Server=localhost;Database=UniversityDB;Trusted_Connection=True;TrustServerCertificate=True;"

# Run migrations
dotnet ef database update

# Run application
dotnet run
```



Database auto-seeds with sample data on first run! 🎉

## 📸 Screenshots

| Dashboard | Students | Courses |
|-----------|----------|---------|
| ![Dashboard](Screenshots/dashboard.png) | ![Students](Screenshots/students.png) | ![Courses](Screenshots/courses.png) |

## 🛠️ Tech Stack

### Backend
- **Framework:** ASP.NET Core 9.0 MVC
- **Language:** C# 14.0
- **ORM:** Entity Framework Core 9.0
- **Database:** SQL Server 2019+
- **Architecture:** MVC Pattern with Repository-like structure

### Frontend
- **CSS Framework:** Bootstrap 5.3
- **Icons:** Font Awesome 6.4
- **Charts:** Chart.js 4.x
- **Fonts:** Google Fonts (Inter)
- **JavaScript:** Vanilla JS with modern ES6+ features

### Additional Libraries
- **Image Processing:** System.Drawing.Common
- **Validation:** Data Annotations, jQuery Validation
- **Session Management:** ASP.NET Core Session
- **File Upload:** IFormFile with custom validation

## 📁 Project Structure

```
university-portal/
├── Controllers/
│   ├── HomeController.cs          # Dashboard & home page
│   ├── StudentController.cs       # Student CRUD operations
│   ├── CourseController.cs        # Course management
│   ├── ReportsController.cs       # Reports & data export
│   └── SeedController.cs          # Database seeding
├── Models/
│   ├── Student.cs                 # Student entity
│   ├── Course.cs                  # Course entity
│   └── UniversityMvcContext.cs    # Database context
├── ViewModels/
│   ├── DashboardViewModel.cs      # Dashboard data
│   ├── SearchViewModel.cs         # Search & filter
│   └── CourseEnrollmentStats.cs   # Analytics data
├── Views/
│   ├── Home/                      # Dashboard views
│   ├── Student/                   # Student management views
│   ├── Course/                    # Course management views
│   ├── Reports/                   # Reports & analytics views
│   └── Shared/                    # Layouts & partials
├── Validations/                   # Custom validation attributes
├── Data/                          # Database seeder
├── wwwroot/
│   ├── css/                       # Custom stylesheets
│   ├── js/                        # JavaScript utilities
│   ├── Images/                    # Uploaded images
│   └── lib/                       # Third-party libraries
└── Program.cs                     # Application entry point
```

## 💾 Database Seeding

Visit `/Seed/Index` for manual control:
- 🌱 Seed 50 students & 15 courses
- 🔄 Reset database for demos
- 🗑️ Clear all data

## 🎯 Detailed Features

### 👨‍🎓 Student Management
- **Full CRUD Operations** - Add, edit, view, and delete student records
- **Profile Pictures** - Upload and manage student photos with validation
- **Advanced Search** - Multi-criteria filtering (name, course, level, age range)
- **Smart Pagination** - Configurable page sizes (10, 25, 50, 100 records)
- **Sorting** - Sort by name, age, level, or course
- **Bulk Operations** - Export student data to CSV

### 📚 Course Management
- **Course Creation** - Add courses with credits and descriptions
- **Enrollment Tracking** - Real-time student enrollment counts
- **Visual Cards** - Modern card-based course display with student avatars
- **Delete Protection** - Prevent deletion of courses with enrolled students
- **Statistics** - View enrollment trends and popular courses

### 📊 Dashboard & Analytics
- **Real-time Statistics** - Total students, courses, enrollments, averages
- **Interactive Charts** - Bar charts for course enrollment (Chart.js)
- **Level Distribution** - Visual breakdown of students by academic level
- **Activity Feed** - Recent system activities and updates
- **Quick Actions** - One-click access to common tasks
- **Animated Counters** - Smooth number animations for statistics

### 📈 Reports & Export
- **CSV Export** - Download student and course data
- **Summary Reports** - Comprehensive system overview
- **Student Details** - Detailed reports grouped by courses
- **Course Analytics** - Enrollment statistics and demographics
- **Custom Date Ranges** - Filter reports by time period
- **Print-Friendly** - Optimized layouts for printing

### 🎨 UI/UX Excellence
- **Modern Design** - Gradient backgrounds and glass morphism effects
- **Responsive Layout** - Perfect on desktop, tablet, and mobile
- **Smooth Animations** - Fade-in, slide-in, hover, and loading effects
- **Toast Notifications** - Non-intrusive success/error messages
- **Loading States** - Professional loading overlays
- **Scroll to Top** - Floating button for easy navigation
- **Breadcrumbs** - Clear navigation hierarchy
- **Custom Scrollbar** - Styled scrollbars matching the theme

## 🚀 Performance & Best Practices

- ✅ **Async/Await** - All database operations are asynchronous
- ✅ **EF Core Optimization** - Efficient queries with Include/Select
- ✅ **Image Validation** - Server-side file type and size validation
- ✅ **CSRF Protection** - Anti-forgery tokens on all forms
- ✅ **Error Handling** - Graceful error pages and user feedback
- ✅ **Responsive Images** - Optimized image loading
- ✅ **Code Organization** - Clean separation of concerns (MVC pattern)
- ✅ **Security Headers** - X-Frame-Options, X-Content-Type-Options

## 🎓 Learning Outcomes

This project demonstrates proficiency in:

- **ASP.NET Core MVC** - Building full-stack web applications
- **Entity Framework Core** - Database operations and migrations
- **Responsive Design** - Mobile-first, cross-browser compatibility
- **JavaScript** - DOM manipulation, async operations, animations
- **SQL Server** - Database design and optimization
- **UI/UX Design** - Modern, user-friendly interfaces
- **Git & GitHub** - Version control and collaboration
- **Software Architecture** - MVC pattern, separation of concerns

## 🔮 Future Enhancements

- [ ] **Authentication & Authorization** - User roles (Admin, Teacher, Student)
- [ ] **Email Notifications** - Automated emails for enrollments
- [ ] **Advanced Analytics** - More charts and data visualizations
- [ ] **Excel Export** - Export to XLSX format with formatting
- [ ] **PDF Reports** - Generate PDF documents
- [ ] **Attendance Tracking** - Mark and track student attendance
- [ ] **Grade Management** - Record and calculate student grades
- [ ] **Calendar Integration** - Academic calendar and events
- [ ] **Dark Mode** - Theme switcher for dark/light modes
- [ ] **Multi-language Support** - Internationalization (i18n)
- [ ] **API Endpoints** - RESTful API for mobile apps
- [ ] **Real-time Updates** - SignalR for live notifications

## 🤝 Contributing

Contributions are welcome! Here's how you can help:

1. **Fork** the repository
2. **Create** a feature branch (`git checkout -b feature/AmazingFeature`)
3. **Commit** your changes (`git commit -m 'Add some AmazingFeature'`)
4. **Push** to the branch (`git push origin feature/AmazingFeature`)
5. **Open** a Pull Request

Please ensure your PR:
- Follows the existing code style
- Includes appropriate tests
- Updates documentation as needed
- Has a clear description of changes

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👨‍💻 Author

**Ahmed Elhosiny**

- GitHub: [@Ahmed-Elhosiny](https://github.com/Ahmed-Elhosiny)
- LinkedIn: [Connect with me](https://linkedin.com/in/ahmed-elhosiny)
- Portfolio: [View my work](https://ahmed-elhosiny.github.io)

## 🙏 Acknowledgments

- Bootstrap team for the amazing CSS framework
- Font Awesome for the comprehensive icon library
- Chart.js for beautiful, responsive charts
- Microsoft for ASP.NET Core and Entity Framework
- The open-source community for inspiration and support

## 📞 Support

If you have any questions or need help:

- 📧 Email: ahmed.elhosiny@example.com
- 💬 Open an [Issue](https://github.com/Ahmed-Elhosiny/university-portal/issues)
- 📖 Check the [Wiki](https://github.com/Ahmed-Elhosiny/university-portal/wiki)

---

<div align="center">

### ⭐ Star this repository if you find it helpful!

**Made with ❤️ using ASP.NET Core MVC**

*Perfect for portfolios, learning, and real-world projects*

[Report Bug](https://github.com/Ahmed-Elhosiny/university-portal/issues) · [Request Feature](https://github.com/Ahmed-Elhosiny/university-portal/issues) · [Documentation](https://github.com/Ahmed-Elhosiny/university-portal/wiki)

</div>