# Catalog Service (BLL & DAL)

A catalog service structured with a  [Clean.Architecture.Solution.Template](https://github.com/jasontaylordev/CatalogService) version 8.0.5.

## System Requirements

- .NET 8.0 or above
- SQLite

## Entities 

The application contains two entities - `Category` and`Product` - with the following functional requirements:

### Category 

- Name: Required (Max length = 50)
- Image: Optional (URL)
- Parent Category: Optional 
- Operations: get/list/add/update/delete

### Product 

- Name: Required (Max length = 50)
- Description: Optional (Can include HTML)
- Image: Optional (URL)
- Category: Required
- Price: Required (Money format)
- Amount: Required (Positive integer)
- Operations: get/list/add/update/delete

## Key Features

- The project follows Clean Architecture principles.
- Extensible and highly testable structure.
- Usage of SQLite for the SQL database in the Infrastructure.

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
