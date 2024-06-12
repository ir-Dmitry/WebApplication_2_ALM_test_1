# Usage

Example API Request
Get employee by name and phone
GET /employee

## Query parameters:

name: employee name (string)
phone: phone number (string)
## Example request:


GET /employee?name=John&phone=+1234567890
Host: localhost:5000
## Example response:


{
    "id": 1,
    "name": "John Doe",
    "phone": "+1234567890",
    "email": "john.doe@example.com"
}
Create a new organisation
POST /organisation

## Example request body:


{
    "OrganisationName": "Example Organisation",
    "PhoneNumber": "+12345678901234",
    "Email": "example@organisation.com",
    "Employee": []
}
## Example response:


{
    "message": "Organisation created successfully"
}
