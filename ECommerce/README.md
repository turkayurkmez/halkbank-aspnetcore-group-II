# ECommerce Training Notes

## Topics Covered Today (2026-11-03)

- Built and registered custom middleware components in the API pipeline.
- Added centralized exception handling middleware.
- Added request logging middleware for incoming HTTP requests.
- Added request timing middleware and exposed duration with `X-Response-Time-Ms` response header.
- Added a development-focused sample endpoint to validate middleware behavior.
- Refactored `Program.cs` to move inline middleware logic into dedicated middleware classes.
- Implemented environment-aware shop configuration using `appsettings.json`, `appsettings.Development.json`, and `appsettings.Production.json`.
- Added strongly typed options (`ShopOptions`) and bound configuration values for controller usage.
- Updated launch settings and project setup to support environment-based behavior during local development.
