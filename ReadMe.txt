Steps to Build:
================

Build DoorManagement.sln solution    
DoormanagementServer - To run as console to start listener on port 5000
DoorManagementClient --> Run WPF client to access the server

Technologies Used:
===================

- C#
- Asp.net core - .net 5.1
- Autofac
- WPF 
- Serilog
- FakeItEasy



Summary of the overall design of the application. Please include what features you added and any notes you think is noteworthy about any design decisions you made within the application.

	- I have used MVVM pattern (ModelView) in WPF to avoid Glue coding in UI. Properly injected dependency as interfaces and so it could be easy for mock and test the functionality. With MVVM, we could test the controllers also by mocking the external dependencies.
	- Used asp.net core api for Rest services
	- I have implemented Add door, Update Door value, Delete door and Connect to the door facility
	- Not implemented Notification to all clients on door config value change - I am thinking of to use some MSMQ and Akka.net for remote communication of the change.I will do this if given extra time.

If you had access to the customer when you were developing this application, what questions would you have asked in relation to your design?

	- Is GateLabel mandatory field?
	- Whats default value for OpenDoor and LockUnlock states for the door
	- Whats maximum concurrent clients that will be accessing the server and whats maximum door will be there in the facility
	- Any Firewall restrictions on server ports
	- Server and client system information where this application will be deployed. So basic framework installation check to be done
	- What are Ui culture to support for WPF client

If you were given another 2 hours, what would you have done next and why

	I would fix async functionalities and add unit tests for the code coverage. Also notification functionality will be implemented to notify the clients on door config changes.