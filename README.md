##[ASP.NET Core Webjob Logger](https://oarklimited.wordpress.com/asp-net-core-azure-webjob-logger)

ASP.NET CORE has an implementation of a logger against which you can add a custom logger. I created a custom logger to demonstrate how you can implement your own custom logger. 
The logger is implemented using a webjob to avoid overusing the resources of the main application and process the log independently of the main application at the background.
The demo app is hosted here at GitHub. In the demo solution, there are three projects: Logger, Client and Webjob.
To test the app, run both the client and the webjob at the localhost. They are both using a local azure emulation, so you will have to install the azure emulator. 
If you want to use a real storage account from azure, change the appsetting.json.
Also, you will learn how to implement dependency injection, how to use Interfaces to abstract a functionalities, and dynamic resolution of a webjob name.

