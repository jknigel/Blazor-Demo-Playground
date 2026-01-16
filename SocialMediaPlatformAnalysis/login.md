Front-end: The user enters the account details such as user and password through input fields from blazor framework. The details are sent to the server using HttpClient.
Back-end: The Minimal API verifies the credentials using SQL Server. If successful, a session token is returned to the client for future authentication.

Data Flow:
Credentials input → API → Validation in SQL Server.
Token generation → Front-end receives token for secure session management.