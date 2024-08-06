# SkiStore

## Description

The project is an online store specializing in ski equipment. It integrates with Stripe for secure and reliable payment processing. Users can create accounts, log in, manage their cart, and place orders. JWT Token is used for user authentication and authorization, ensuring secure access. Additionally, some data is cached to improve performance and reduce latency, resulting in a more efficient user experience.

## Used technologies

**Backend**

- .NET 8.0

**Frontend**

- Angular v17

**Databases**

- PostgreSQL - for storing products and users data,
- Redis - for basket and caching

**Containerization**

- Docker - for databases

## Build and run

To run the project perform the following steps:

1. Install Node.js 18.3 or higher
2. Install ASP.NET core 8.0 or higher
3. Install Docker
4. Install the Angular CLI:
   `npm install -g @angular/cli`
5. Open a command prompt in root folder and run `docker-compose up --detach`
6. Go to the project's `client` folder
7. Run `npm install`
8. Run `ng serve` to start the Angular build process
9. Open a new command window in the root of the project and run the following commands:

```
dotnet restore
dotnet build
dotnet watch run --project API
```

10. Visit http://localhost:4200 in the browser

```
dotnet restore
dotnet build
dotnet watch run
```

## Screenshots

**Products with filters**
![](ReadmePictures/1.jpg)
**Product details**
![](ReadmePictures/2.jpg)
**Cart**
![](ReadmePictures/3.jpg)
**Finalizing order (address)**
![](ReadmePictures/4.jpg)
**Finalizing order (delivery)**
![](ReadmePictures/5.jpg)
**Finalizing order (review)**
![](ReadmePictures/6.jpg)
**Finalizing order (payment)**
![](ReadmePictures/7.jpg)
**Finalized order**
![](ReadmePictures/8.jpg)
**Orders**
![](ReadmePictures/9.jpg)
**Login**
![](ReadmePictures/10.jpg)
**Register**
![](ReadmePictures/11.jpg)
