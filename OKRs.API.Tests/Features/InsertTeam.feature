Feature: InsertTeam
	In order to insert a new team into ORKs system
	As a http client user
	I want to use the insert team endpoint

Background: 
	Given I have a valid TeamDto

Scenario: Add valid team	
	When I call the insert Team API endpoint
	Then 201 success code is returned as a response
	#And the response body should contain the TeamDto properties
