# Project summary
The aim of this project is to create an example where a post with author can be added to a Blogging system.
This project is created using .NET technology and C# as programming language
It follows the principles of DDD, Clean Architecture and testing.

## Steps to install and implement the project
The first and most important thing is to install version 9 of the .NET SDK.

* Clone the project on the local machine with the command ```git clone```

* Enter the main ```BloggingApp``` project folder and run the ```dotnet build``` command.

* Execute the command ```dotnet run --project BloggingApp.API```

* Once confirmed that the server is working access the url ```http://localhost:{port}/swagger/index.html``` to know the details of the API and be able to execute the calls.

* To pass the test suite, run the command ```dotnet test``` in the root folder of the project

