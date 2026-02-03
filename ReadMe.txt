Overview : 
ASP .NET Web Application with a Microsoft Azure SQL Database showcasing a functioning online marketplace .

Features:
- Users that have created and logged in to their accounts have access to the shopping cart for placing products to buy (no payment methods are used , since this is a project for showcasing functionalities) . They can also view their order history in the User Orders page.
- Users who are not logged in can still browse products and view their extended info on the Product Details page.
- Search functionality for products is included with text search and category filters.
- Home Page includes Brochure images , which when clicked , redirect to a new page with product offers.
- Products that are out of stock or are on sale get special elements added to their displays in the product browsing page.
- When an order is placed , the database updates automatically and subtracts the amount of items bought from their respective available quantities.
- The user with admin privileges has special pages for admins , where they can view the details of ,edit,remove and add products and brochures.

Prerequisites for running the project locally:
- Visual Studio with .NET version 8.0
- MS SQL Server Management Studio or an alternative program for using a SQL database
- (Optional) Docker and Docker Desktop

Running the app after installation :
- After pulling the project from GitHub, if you wish to create a local database instead of using the Azure database, go to the appsettings.Development.json file in the YMart project folder (not YMart.Tests) and edit the SqlServer connection string to something like this "Server=DESKTOP-...;Database=YMartDb;Trusted_Connection=True;TrustServerCertificate=True;" and make sure you are connected to your local SQL instance . After that in the Program.cs file edit this line : "var connectionString = builder.Configuration.GetConnectionString("AzureConnection") ?? throw new InvalidOperationException("Connection string not found.");" to replace "AzureConnection" to "SqlServer". Open the Package Manager Console and write "add-migration Initial".
When creating migrations for connecting tables , '...: ReferentialAction.Cascade' needs to be changed to '...: ReferentialAction.NoAction' manually. After that open the Package Manager Console again and write
"update-database". After that you should have a working web app with an empty database . If you wish to fill the database with data , you can do it manually through SQL or register as admin@gmail.com and have admin privileges (you can change the admin e-mail by editing
this line in Program.cs :"string email = "admin@gmail.com";"). You might get an error related to user roles when launching the app for the 1st time or not see the links to the admin only pages on the top left corner of the site , just launch it again and log out and log in and it should work.

(Optional) Creating a docker image and container :
- (Might not work if the Azure Database has been disabled for cost related reasons)
- For the docker image to work the app needs to connect to the Azure database by using the "AzureConnection" connection string , like the default in the GitHub repo. Make sure you have Docker Desktop open . From the dropdown menu with the green arrow at the top of the
Visual Studio UI select Docker and run the app. You should see a docker image and a docker container created on Docker Desktop . While the app is running click on the localhost link from the newly created docker container.

Notes for the Azure Live Demo:
- (The Live Demo might be disabled for cost related reasons)
- Link : ymart.azurewebsites.net
- If you get an error when clicking the azure link, try opening the link again in 10-15 minutes, since the database is on a serverless plan and has periodic pauses.
