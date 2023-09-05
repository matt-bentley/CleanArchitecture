@weather_cleanup
Feature: Weather Forecast

Weather Forecast page shows a table of weather forecasts 
and allows new forecasts to be generated.

Scenario: 1 Navigate to Weather Forecast page
	Given a user is on the Home page
	When Weather Forecast page is opened
	Then Weather Forecast page is open

Scenario: 2 Generate a Weather Forecasts
	Given a user is on the Weather Forecast page
	When 'London' location is selected
	And a weather forecast is generated
	And a weather forecast is generated
	Then '2' weather forecasts present

Scenario: 3 Generate Weather Forecasts prompt shown
	Given a user is on the Weather Forecast page
	When 'Mumbai' location is selected
	Then Generate prompt is visible
	And '0' weather forecasts present