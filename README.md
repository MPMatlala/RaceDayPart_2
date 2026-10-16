# RaceDay API (Part 2)

## First time setup
1. Open RaceDay.sln in Visual Studio.
2. IMPORTANT: if you already ran the Part 1 SQL script, delete the old RaceDayDB first
   (SQL Server Object Explorer -> right click RaceDayDB -> Delete). The API creates its own tables
   and the Part 1 sample users have fake passwords that cannot log in.
3. Tools -> NuGet Package Manager -> Package Manager Console. Set "Default project" to RaceDay.Api, then run:
       Add-Migration InitialCreate
       Update-Database
4. Press F5. Swagger opens at https://localhost:7100/swagger

## Using Swagger
1. POST /api/auth/register/organiser (or participant): click Try it out, fill in the boxes, Execute. Expect 201.
2. POST /api/auth/login: use the same email and password. Expect 200 with "Logged in successfully." and a token.
3. Copy the token, click Authorize (top right), paste the token only, click Authorize.
4. Now the protected endpoints work for your role.

## Tests
    dotnet test RaceDay.sln
    picture for verified 
<img width="1872" height="578" alt="image" src="https://github.com/user-attachments/assets/f181e4e8-02ad-47d5-9e6e-c066d66b88a6" />

