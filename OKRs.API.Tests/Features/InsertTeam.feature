Feature: InsertTeam
	In order to insert a new team into ORKs system
	As a http client user
	I want to use the insert team endpoint

Background: 
	Given I have a valid TeamDto

Scenario: Add valid team	
	When I call the insert Team API endpoint
	Then the result should be the TeamDto submitted with the property TeamId greater than zero

Scenario: Add team with empty region
	When I update TeamDto with empty team region
	And I call the insert Team API endpoint
	Then the API returns 'BadRequest' status code
	And 'Team region is null' error message is returned as a response