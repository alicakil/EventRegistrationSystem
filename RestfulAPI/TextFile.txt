App Name: 
Event Registration System

Description:
App provides simple a calendar events, listing events, listing subscribers and functionality such as to subscribe to those events. checking geolocation info from google api etc.
The app contains backend part using Restful-API. (no front-end included)
Events, Subscribers, Database Design (EF Code first Approch) Dummy Data


How To Run:
Before you run the app from (Visual Studio, IIS etc.) You must provide a connection string in Context.cs
if the connection string is valid, app will create a database automatically with the first run (see CreateDemoDatabase method in Program.cs)

Also good to provide an API_KEY for Google MAPS API. (not mandotory)


How to extend this design?:
Project includes EF Code First. So incase you add new fields to table designs (cs files under Model folder) after you modify, 
use add-migration <give a name>
update-database


Dependicies:
.NET CORE 6
.EF 6 packs


Author : Ali CAKIL
Web site: https://alicakil.com
Date:2022