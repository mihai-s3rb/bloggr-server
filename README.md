# Bloggr App - create your own blog
#### Fullstack application

***Backend:***
- Project structure based on Clean Arhitecture
- Made using ASP. NET Core 6 Web Api and implements CRUD operations while implementing the Mediator Pattern
- Uses EF Core as ORM to communicate with the database while implementing the Repository Pattern / Unit of Work
- Uses Microsoft Identity with JWT implicit flow for authentication & authorization with a custom handler
- Uses SignalR for real time communication for the user-to-user chat feature
- Uses Fluent Validation to validate incoming user data through a generic filter
- Global exception middleware to show appropiate error messages
- Uses Azure Blob Storage to store images

***Frontend:***
- Created using React and Material UI
- Uses Redux Toolkit for global state management and RTK Query for communicating with the backend API
- Uses React Router to create routing
- Utilizes react-hook-form to make smooth user experience forms with validation
- Uses draft-js to implement a custom WYSIWYG editor with custom plugins (such as image support and pop-up text editor)
- Uses @microsoft/signalr to communicate with the web socket

![](https://raw.githubusercontent.com/smrazvan/Bloggr/dev/Presentation/src/assets/img/bloggr-low-resolution-logo-white-on-black-background.png)
