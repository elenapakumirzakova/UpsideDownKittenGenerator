# UpsideDownKittenGenerator

## UpsideDownKittenGenerator?
UpsideDownKittenGenerator is a free source code generator that allows you to get upside-down kitten pictures. The developing of the product is at the MVP stage now.

How can I use it?
Building
Features
Contributing
Links
License
How can I use it?
Authorized user can get a random upside-down kitten picture as follows:
1. Make POST request to user registration (Sing up)using body:
URL: `/Users/Register`
{
  "name": "Name",
  "email": "Email",
  "password": "Password"
}

Response:
{
  "token": "string",
  "result": boolean,
  "errors": "string[]"
}
Please note, that the "password" should have lowercase, uppercase, number and nonalphanummeric letters 
2. Make POST request to login user using body:
URL: `/Users/Login`
{
  "email": "Email",
  "password": "Password"
}
Response:
{
  "token": "string",
  "result": boolean,
  "errors": "string[]"
}
3. Make GET request to receive a upside-down kitten picture
URL /Kitten
Headers: 
"Authorization": "Bearer token"
Developing
Clone this repository:

$ git clone https://github.com/elenapakumirzakova/UpsideDownKittenGenerator.git
Building
Requirements:

.NET 5.0
MS SQL Server 2019;
Visual Studio IDE
To run the project locally:

import cloned project to your Visual Studio IDE
replace in appsettings.json with your database configuration:
 "System": {
 "Sql": {
 "Host": "your hostname",
 "Database": "your database name",
 "UserId": "encrypted UserId",
 "Password": "encrypted Password",
 "ConnectionStringTemplate": "Server={0};Database={1};User Id={2};Password={3};"
 }}

To create new database use script in PM console as follows:
Add-Migration "your migrationname"
Update-Database

Start application

Features
JWT Token for Auth;
All data is stored in the database;

ToDo List
What supposed to be done next:

Logging
Unit Tests
Errorhandler
Encryption
CI Deploy (Cloud preffered)
Contributing
If you'd like to contribute, please fork the repository and use a feature branch. Pull requests are warmly welcome :-)

Links
Author: https://www.linkedin.com/in/yelena-pak-umirzakova

License
Its use is governed by MIT License (X11 License).
