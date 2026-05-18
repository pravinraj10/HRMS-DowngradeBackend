# Implementing Role-Based Authorization (RBAC)

This guide explains how to implement Role-Based Authorization in your ASP.NET Core backend and React frontend using JWT (JSON Web Tokens).

## 1. Overview

Role-Based Authorization ensures that only users with specific roles (e.g., `Admin`, `HR`, `Employee`) can access certain API endpoints in the backend and specific pages/components in the frontend.

---

## 2. Backend Implementation (ASP.NET Core)

### Step 2.1: Add Role to the User Model
Ensure your database has a way to link a User (or Employee) to a Role.

```csharp
public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    
    // Foreign Key to Role
    public int RoleId { get; set; }
    public Role Role { get; set; }
}

public class Role
{
    public int Id { get; set; }
    public string RoleName { get; set; } // e.g., "Admin", "HR"
}
```

### Step 2.2: Include the Role in the JWT Claims
When a user logs in successfully, generate the JWT token and include their role as a Claim. 
Update your `JwtService.cs` or wherever you generate tokens:

```csharp
using System.Security.Claims;

// Inside your JWT generation method:
var claims = new List<Claim>
{
    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
    new Claim(ClaimTypes.Name, user.Username),
    // ADD THE ROLE CLAIM HERE
    new Claim(ClaimTypes.Role, user.Role.RoleName) 
};

var tokenDescriptor = new SecurityTokenDescriptor
{
    Subject = new ClaimsIdentity(claims),
    // ... other properties (Expires, SigningCredentials)
};
```

### Step 2.3: Protect Controllers and Endpoints
Now you can restrict access to specific endpoints by using the `[Authorize]` attribute with the `Roles` parameter.

```csharp
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    // Any authenticated user can access this
    [HttpGet]
    [Authorize]
    public IActionResult GetAllEmployees()
    {
        return Ok(...);
    }

    // ONLY Admins and HR can access this
    [HttpPost]
    [Authorize(Roles = "Admin,HR")]
    public IActionResult CreateEmployee()
    {
        return Ok(...);
    }

    // ONLY Admins can access this
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteEmployee(int id)
    {
        return Ok(...);
    }
}
```

---

## 3. Frontend Implementation (React)

The frontend needs to know the user's role to restrict access to pages and hide UI elements.

### Step 3.1: Decode the JWT Token
When the user logs in, save the JWT token in `localStorage` or context. You can use the `jwt-decode` library to read the claims.

```bash
npm install jwt-decode
```

```javascript
import jwtDecode from 'jwt-decode';

// Retrieve token from storage
const token = localStorage.getItem('token');

if (token) {
    const decodedToken = jwtDecode(token);
    // Note: The exact key for roles depends on your backend claim type.
    // Standard ClaimTypes.Role maps to "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
    const userRole = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    console.log("Current User Role:", userRole);
}
```

### Step 3.2: Create a Protected Route Component
Create a wrapper component for your routes that checks if the user has the required role.

```jsx
// components/ProtectedRoute.jsx
import { Navigate, Outlet } from 'react-router-dom';
import jwtDecode from 'jwt-decode';

const ProtectedRoute = ({ allowedRoles }) => {
    const token = localStorage.getItem('token');

    if (!token) {
        return <Navigate to="/login" replace />;
    }

    try {
        const decoded = jwtDecode(token);
        const userRole = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];

        if (allowedRoles.includes(userRole)) {
            return <Outlet />; // User has access, render the child routes
        } else {
            return <Navigate to="/unauthorized" replace />; // User lacks permission
        }
    } catch (error) {
        return <Navigate to="/login" replace />;
    }
};

export default ProtectedRoute;
```

### Step 3.3: Apply Protected Routes in `App.js`
Wrap your routes with the `ProtectedRoute` component.

```jsx
// App.jsx
import { Routes, Route } from 'react-router-dom';
import ProtectedRoute from './components/ProtectedRoute';

function App() {
  return (
    <Routes>
      <Route path="/login" element={<Login />} />
      <Route path="/unauthorized" element={<Unauthorized />} />

      // Routes accessible to Admin and HR
      <Route element={<ProtectedRoute allowedRoles={['Admin', 'HR']} />}>
        <Route path="/dashboard" element={<Dashboard />} />
        <Route path="/employees" element={<EmployeeList />} />
      </Route>

      // Routes accessible ONLY to Admin
      <Route element={<ProtectedRoute allowedRoles={['Admin']} />}>
        <Route path="/settings" element={<Settings />} />
        <Route path="/roles" element={<Roles />} />
      </Route>
    </Routes>
  );
}
```

### Step 3.4: Conditionally Render UI Elements
You can also hide/show specific buttons (like "Delete Employee") based on the user's role.

```jsx
// components/EmployeeTable.jsx
import jwtDecode from 'jwt-decode';

const EmployeeTable = ({ employees }) => {
    const token = localStorage.getItem('token');
    let userRole = null;
    
    if (token) {
        const decoded = jwtDecode(token);
        userRole = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    }

    return (
        <table>
            {/* ... */}
            <tbody>
                {employees.map(emp => (
                    <tr key={emp.id}>
                        <td>{emp.name}</td>
                        <td>
                            {/* Everyone can view */}
                            <button>View</button>

                            {/* Only Admin can delete */}
                            {userRole === 'Admin' && (
                                <button onClick={() => handleDelete(emp.id)}>Delete</button>
                            )}
                        </td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
};
```

---

## 4. Summary Checklist
1. [ ] Assign roles to users in the database.
2. [ ] Add `ClaimTypes.Role` to the JWT generation logic in the backend.
3. [ ] Use `[Authorize(Roles="RoleName")]` on backend controller methods.
4. [ ] Install `jwt-decode` on the React frontend.
5. [ ] Create a `ProtectedRoute` component to secure frontend routes.
6. [ ] Conditionally render sensitive UI buttons/links based on the decoded role.
