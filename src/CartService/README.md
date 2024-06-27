# Description

This application includes a single entity - Cart which encapsulates the following functionalities:

Generate a unique client-side cart ID
Fetch list of items in the cart
Add an item into the cart
Remove an item from the cart

Each item in a cart encapsulates the following information:
Required ID, which serves to denote the item in the external system
Required item name
Optional image URL and alt text
Required price
Quantity of items in the cart


# Architecture
The project was generated using the [Clean.Architecture.Solution.Template](https://github.com/jasontaylordev/CartService) version 8.0.5.
LiteDB, a simple, fast and lightweight .NET database, is used for managing the data persistence and retrieval.

## Build

Run `dotnet build -tl` to build the solution.

## Run

To run the web application:

```bash
cd .\src\Web\
dotnet watch run
```

Navigate to https://localhost:5001. The application will automatically reload if you change any of the source files.

## Code Styles & Formatting

The template includes [EditorConfig](https://editorconfig.org/) support to help maintain consistent coding styles for multiple developers working on the same project across various editors and IDEs. The **.editorconfig** file defines the coding styles applicable to this solution.

## Code Scaffolding

The template includes support to scaffold new commands and queries.

Start in the `.\src\Application\` folder.

Create a new command:

```
dotnet new ca-usecase --name CreateTodoList --feature-name TodoLists --usecase-type command --return-type int
```

Create a new query:

```
dotnet new ca-usecase -n GetTodos -fn TodoLists -ut query -rt TodosVm
```

If you encounter the error *"No templates or subcommands found matching: 'ca-usecase'."*, install the template and try again:

```bash
dotnet new install Clean.Architecture.Solution.Template::8.0.5
```

## Test

The solution contains unit, integration, and functional tests.

To run the tests:
```bash
dotnet test
```
