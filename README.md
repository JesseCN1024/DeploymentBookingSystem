# Overview
| *This project is the presentation project for the FPT Software Internship period

Deployment Booking System: A mock booking system for booking source code deployment time range supporting multiples group to work on a same AWS environment without encountering conflicts. Users can book a time range they want to deploy their code, efficently manage shared resources.

| This project for now just presents the backend microservices size of the booking system

**With noticable uses of:**

* Token validation
* Fluent Validation: split validations into multiple files for scaling and easy management
* Custom Authorization: using policy for authorization
* Middleware Error Exception: a full functional system to catch error and return easy-to-read errors 

## Requirements
We have the overall and simple architecture of the project

![](/assets/img/2025-01-11-21-36-28.png)

And requirements for each endpoints/services look like:

![](/assets/img/2025-01-11-21-41-54.png)

## Endpoints example
All Endpoints for local run and docker run

![](assets/img/2025-01-12-10-02-21.png)

![](assets/img/2025-01-12-10-03-54.png)

Example of some endpoint calls and returns

![](assets/img/2025-01-12-09-58-08.png)

![](assets/img/2025-01-12-10-05-02.png)

![](assets/img/2025-01-12-10-05-10.png)

![](assets/img/2025-01-12-10-05-18.png)

![](assets/img/2025-01-12-10-05-42.png)


# Tech Stack
Backend:
* .NET Core API 
* Microservices architecture
Docker:
* Containerizing all the services and the BFF




# Installation 
1. Clone the repo
2. Open project in Visual Studio
3. Install all dependencies / Nugget packages required
4. Check namespace, utils and any error,...
5. Run the project


