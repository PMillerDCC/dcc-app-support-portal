# dcc-app-support-portal

ASP.NET Core MVC Application Support Knowledge Portal for Software Engineering \& DevOps module.

Project Overview:

The Application Support Knowledge Portal is a secure, web-based system designed to centralise technical information about applications, servers, and troubleshooting notes used within Durham County Council. The goal is to replace documentation (emails, shared files, tacit knowledge) with a single, searchable, role-secured platform.

This project is being developed as part of the Software Engineering \& DevOps module and will also integrate with my Major Project (MCP Server) through a lightweight API.

Current Project Status:

* GitHub repository created and initial solution committed
* ASP.NET Core MVC project scaffolded
* Branching strategy established (main + feature branches)
* Development environment configured and running

Tech Stack:

* ASP.Net Core MVC (C#)
* Entity Framework Core
* PostgreSQL (Render)
* Docker for containerisation
* GitHub for version control
* GitHub Actions for CI/CD
* Render for cloud hosting and deployment

Planned Database Schema:

The system will use a relational database with four core tables:

* Applications - name, developer, environment, status
* Servers - server name, OS, status
* Notes - troubleshooting notes, known issues, documentation links
* Users - authentication + role-based access

Security:

The application will implement protections aligned with OWASP best practices:

* Input validation
* Parameterised queries
* XSS protection
* Password hashing
* Role-based authorisation

API Integration (for MCP Server):

A lightweight, read-only API layer will expose selected data:

* GET/api/applications
* GET/api/servers
* GET/api/aupportnotes

This API will be consumed by my MCP Server as part of my Major Project.

DevOps \& Deployment:

The project will follow a modern DevOps workflow.

* GitHub branching strategy
* GitHub Actions CI pipeline
* Docker containerisation
* Render deployment with automatic redeploys  on push



\## Source Code

https://github.com/PMillerDCC/dcc-app-support-portal



\## Live Demo

https://dcc-app-support-portal-1.onrender.com/





