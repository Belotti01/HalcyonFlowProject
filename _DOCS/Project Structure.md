# PROJECT STRUCTURE
## STARTUP PROCESS
The service is started once the Main method in Program.cs finishes.
The Main method prepares the required connections (to the Database and to external APIs), and injects them.
To access injected items from any class, simply define a member with the type of the item you wish to inject and prepend it with the [Inject] attribute.
Once the injections are made by the application builder, the authorization and authentication systems are registered into the app.

## THE STRUCTURE
| Folder | Description |
| --- | --- |
| _DOCS | All kinds of documentation, split across text files. |
| Areas | The authentication components of the application. |
| Components | The Razor components used across the pages. For the most basic components check the NLRazor library. |
| Components/Buttons | Any kind of component that's build around the user clicking on it. |
| Components/Database | Components oriented toward interactions with the database, such as data display. |
| Components/Settings | The components used to handle configuration data reading and writing. |
| Config | Created in runtime. Do NOT edit manually. |
| Data | All classes/structures/records that store data. |
| Data/Database/Context | Contains the local extension of the DbContext (DB.cs). Should only be changed along with the Database itself. |
| Data/Database/Tables | All tables of the database, converted into classes. |
| Data/Settings | Data representation of settings and configurations. |
| Logic | Any class suited to manipulate data or execute complex operations. |
| Migrations | The files used to create and update databases. NEVER edit this folder manually, check <_DOCS/Setup Guide> for more information. |
| Pages | The Razor pages accessible by the users, and their wrappers (_Host, _Layout). |
| Resources | Storage used for translations. |
| Shared | The components that make up the layout shared across all pages of the website. |
| wwwroot | Folder that's plainly accessible during runtime. Mostly contains the stylesheets of the website (css, Bootstrap and TailwindCSS). |
