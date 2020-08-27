# How to run the application locally
The application is build on a 64 bit Windows machine.

## Running Web API Server 

    1. Open PersonPets.API/PersonPets.API.sln in Visual Studio 2019.
    2. Make sure PersonPets.API project is set as the Startup Project.
    3. Run the application using Kestral Server(make sure PersonPets.API is selected from the drop down and not IIS Express or Docker). 
    4. this will host the web API and open the swagger page at https://localhost:5001/index.html
   

---
## To Open Web client

    1. The client is build using Angular 10.
    2. From command prompt, go to person-pets-ui folder path.
    3. Run "npm install"
    4. And then "ng serve"
    5. Open http://localhost:4200/ in the web browser.

---
# Description of the solution
The server part of the application is build using ASP.NET CORE 3.1

## Web API design
* An ApiClient is implemented to fetch data from other web services/ APIs.
* A Validator service is implemented to perform validation on the data models fetched from the external source.
* PeopleService is responsible for processing the business logic and sorting the pet names and returning the data back to the controller.
* API Key Authentication is implemented. 
* Swagger is implemented for API documentation and Serilog logger is used for logging purposes.
 
### Test cases 
 Various unit and feature level test cases are implemented. NuGet packages used: <strong>MOQ</strong> - Mocking framework.

## Client design
* The front end of the application is a web portal build using Angular 10.
* A separate service is implemented to make a HTTP call to the Web API to fetch the data. 
* PetsComponent is used to display the data for the Male and Female owners.
