- To properly implement tailwind while debugging:
On VS startup, run the following command from the CLI:
```
tw -i .\wwwroot\css\app.css -o .\wwwroot\css\app.min.css --watch
```
		
---
#### DEPRECATED INFORMATION - THE DATABASE CAN BE INITIALIZED BY SIMPLY RUNNING THE PROGRAM AND FILLING IN THE REQUESTED INFORMATION
Leaving this information here in case it's required for testing.

| To setup the database to handle accounts |
|---|
| 	0. Make sure the 'database' info explained above points to the correct schema |
|	1. If it exists, delete the file ending with '_databaseMigration.cs' under the 'Migrations' folder |
|	2. Open the Package Manager CLI (Visual Studio: Tools -> Nuget Package Manager -> Package Manager CLI) |
|	3. Run the following commands:  |
		remove-migration
		add-migration 'databaseMigration'
		Update-Database
		
| Command | Description |
|---| --- |
| remove-migration | Resets the migration status as the database is yet to be created |
| add-migration | Creates or updates the migration file to include the latest edits |
| update-database | Creates the schema (if missing) and all the required tables |
