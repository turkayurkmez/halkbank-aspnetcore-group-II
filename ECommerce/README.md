# ECommerce (.NET 10) Learning Journal

This repository currently contains a starter ASP.NET Core Web API project targeting **.NET 10**.

## Daily Notes

| Day | What is covered in this repo | Learning outcomes |
|-----|-------------------------------|-------------------|
| Day 1 | Initial API setup with `WebApplication.CreateBuilder`, controller registration, minimal endpoint mapping (`/ping`), and middleware pipeline basics (request logging + response timing via `OnStarting`) | Understand how an ASP.NET Core API app boots, how middleware order affects request/response flow, how to log incoming/outgoing requests, and how to attach custom response headers such as `X-Response-Time-Ms`. |

## Current Project Structure

- `ECommerce.API/Program.cs`  
  Application startup, middleware configuration, and endpoint mapping.
- `ECommerce.API/Controllers/SampleController.cs`  
  Basic sample controller endpoint.
- `ECommerce.API/ECommerce.API.csproj`  
  Project definition targeting `net10.0`.

## Summary

This repo currently represents **Day 1 foundation work** for an e-commerce API backend: startup configuration, middleware concepts, and first endpoint exposure.
