# Sectional Title CRM System

This is a legacy CRM system built for managing and tracking maintenance queries within Sectional Title complexes. It allows real estate agents who manage these complexes to track and manage maintenance issues, communicate with subcontractors, and interact with apartment owners and tenants.

---

## Table of Contents

- Project Overview
- Technologies
- Features
- Limitations
- License

---

## Project Overview

This CRM was developed to streamline the management of maintenance requests in Sectional Title complexes. The goal was to provide a centralized platform for property agents to manage maintenance queries, assign tasks to subcontractors, and track the status of issues reported by tenants or owners.

The project is built on **ASP.NET Web Forms**, with a **MySQL** database. Database operations are mainly handled via **stored procedures** to encapsulate complex logic and maintain better performance. On the frontendl, the **AjaxControlToolkit** is ued to enhance the user experience with AJAX-based controls for asynchronous updates.

While the application logic remains functional, the MySQL database has been lost, making it impossible to replicate the system in its full operational state.

---

## Technologies

- **Frontend:** ASP.NET Web Forms, AjaxControlToolkit
- **Backend:** C# (.NET Framework)
- **Database:** MySQL (using stored procedures for most operations)
- **Web Server:** IIS (Internet Information Services)

---

## Features

- **Agent Dashboard:** Shows a protracted list of jobs which are due on the day or currently overdue jobs, allowing the user to view the job details and quickly follow up.
- **Subcontractor Management:**: Track subcontractors responsible for different maintenance tasks (eg. electricians, plumbers, contractors, etc.).
- **Tenant/Owner:** View and track apartment tenants and owners, allowing necessasry updates as ownership/occupancy changes over time.
- **Maintenance Query Management:** Submit, track, and resolve maintenance queries.
- **Search & Filter:** Advanced search functionality to find specific queries based on status, priority, and other criteria.

---

## Limitations

- **Database Missing:** The application relies on a MySQL database to store user data, maintenance requests, and more. Unfortunately, the original database has been lost, making it impossible to replicate the system in a fully working state.
- **No Javascript:** The project primarily uses **AJAXControlToolkit** for client-side interactivity, and Javascript was not directly utilized for most of the UI interactions. As a result, transitioning this project to modern frontend frameworks (like React or Angular) would require significant refactoring.
- **ASP.NET Web Forms:** The system is built on **ASP.NET Web Forms**, which is now considered outdated. It may be difficult to integrate with modern web technologies without a major rewrite.

---

## License

This project is for demonstration purposes only. Feel free to use it as a reference for legacy ASP.NET Web Forms applications.
