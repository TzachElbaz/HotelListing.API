# Hotel Listing API (Example Project)

This is a sample **ASP.NET Core Web API** project (not deployed to a live server).  
It demonstrates common practices such as authentication, authorization, caching, paging, and OData filtering.

## Features

- **Countries & Hotels**
  - View and manage a list of countries
  - Each country contains a list of hotels
  - View and manage hotels

- **Authentication & Authorization**
  - All modification actions (`POST`, `PUT`, `DELETE`) require authentication (JWT-based)
  - `DELETE` operations are restricted to users with the **Admin** role

- **User Accounts**
  - Account management (login, register, etc.) is handled by the `AccountsController`

- **Advanced API Capabilities**
  - Caching for optimized performance
  - Paging support for large datasets
  - OData query support for filtering and flexible data access

## Controllers
- `CountriesController`
- `HotelsController`
- `AccountsController`

---

## Postman Collection

A Postman collection is included to help you easily test all API endpoints.  
See the file: **`HotelListing Local.postman_collection.json`**

### How to Use
1. Open Postman  
2. Import the file **`HotelListing Local.postman_collection.json`**  
3. Make sure the global variable `{{localhost}}` is defined:
   - Default value: `http://localhost:5103`  
   - If your API runs on a different port, update this value accordingly

Now you can execute requests against all endpoints and explore the API functionality.

