## Game Catalogue 🎮

* In this API, we created a game catalogue using ADO .NET and asynchronous programming. 

* The table was created prior to this project directly from SQL Server Management Studio:

  ###### create table Games (

  ###### 			 Id uniqueidentifier not null default newId(),

  ######              Name varchar(100) not null,

  ###### 			 Producer varchar(50) not null,

  ###### 			 Price float not null

  ###### )

* We used pagination to return the results, which facilitates when bringing large amounts of data from the database.

* An exception middleware was used to handle errors during requests in ASP .NET Core.

* This project was created during the "Creating a game catalogue using architecture good practices with .NET" course, from Digital Innovation One.
