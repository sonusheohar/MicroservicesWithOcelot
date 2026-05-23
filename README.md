# Microservices with Ocelot API Gateway

## 📖 Overview
This project demonstrates a **microservices architecture** using **Ocelot API Gateway** in .NET.  
It integrates multiple services — **UserService**, **ProductService**, and **Authentication/IdentityService** — behind a single gateway, providing centralized **routing, authentication, authorization, rate limiting, caching, and request aggregation**.

---

## 🏗️ Architecture
- **[API Gateway](ca://s?q=Explain_API_Gateway_Architecture)**: Single entry point for all client requests.  
- **Microservices**: Independent services (`User`, `Product`, `Account`) running on different ports.  
- **Middleware**: Custom middlewares (`TokenCheckerMiddleware`, `InterceptionMiddleware`) for security and request handling.  
- **Load Balancing**: Configured via Ocelot (`LeastConnection` strategy).  

---

## 🔐 Authentication & Authorization
- **[Authentication](ca://s?q=API_Gateway_Authentication)** handled via `TokenCheckerMiddleware`.  
- **[Authorization](ca://s?q=API_Gateway_Authorization)** enforced using `app.UseAuthorization()` with role/claim checks.  
- Login/Register routes connect to **IdentityAPI** for issuing JWT tokens.

---

## ⚖️ Rate Limiting
- Configured in `Ocelot.json` with `RateLimitOptions`.  
- Example: `UserService` allows **2 requests per 60 seconds per client**.  
- Prevents abuse and ensures fair usage across clients.

---

## 🔗 Request Aggregation
- Aggregate route `/api/aggregate/user_prod` merges **UserService** and **ProductService** responses.  
- Useful for dashboards or composite views.  
- Configured via `Aggregates` with `RouteKeys`.

---

## 🗄️ Caching
- Global cache enabled: `TtlSeconds=30`.  
- Reduces repeated downstream calls.  
- Custom header `OC-Caching-Control` can override caching behavior.

---

## 🚦 Routing
- **UpstreamPathTemplate** → Client-facing route (`/api/user`, `/api/product`).  
- **DownstreamPathTemplate** → Actual microservice endpoint (`localhost:8001`, `localhost:8002`).  
- Supports multiple downstream hosts with load balancing.

---

## 📂 Project Structure
MicroservicesWithOcelot_BySonu/
│── APIGateway/
│   ├── Program.cs
│   ├── ocelot.json
│   └── Middlewares/
│── AuthenticationAPI/
│── IdentityAPI/
│── ProductAPI/
│── SharedLib/
