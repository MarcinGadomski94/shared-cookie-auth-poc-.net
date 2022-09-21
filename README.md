# Shared cookie authentication with .NET 6

This repository contains an example how shared authentication is working using .NET 6 framework between Auth microserivce and Auth front-end hosted as separate environment/application.

## Running the solution

In order to run the solution, you need to have .NET 6 installed on your computer.

First, run Auth-Microservice and access Swagger page in order to create user, and afterwards login into the system.
Later, run Auth-Frontend and start up the browser to navigate to the index page. There, if you are logged in, you will be able to see the username of the user that you are currently logged in with.
