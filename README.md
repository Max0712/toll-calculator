# Toll fee calculator 1.0 -- Coastal Code Example
A calculator for vehicle toll fees.

* SQLite databas skapas via migration-projektet. Körs som console och lägger toll.db i c:\temp

## Endpoints
* POST: "*/toll/event"
	{
    		"RegistrationNumber" : "ABC123",
    		"VehicleType" : "Car",
    		"EventTime" : "2020-12-01T10:30Z"
	}

* GET: "*/toll/sum?registrationNumber=ABC123&FromDate=2020-12-01T00:00Z&ToDate=2020-12-02T00:00Z"