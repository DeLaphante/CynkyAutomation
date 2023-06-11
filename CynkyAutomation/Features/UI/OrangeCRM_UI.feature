@OrangeCRM
Feature: OrangeCRM_UI

Background: Navigate to home page
	Given customer is on the Orange CRM homepage
	When user with the following details logs in:
		| Property | Value    |
		| Username | Admin    |
		| Password | admin123 |


Scenario: Delete record on Orange CRM
	Given naviagtes to 'PIM' Page
	When user deletes customer in row without employment status 'Full-Time'
	Then the customer should not be displayed


Scenario: Add new employee
	Given naviagtes to 'PIM' Page
	When user adds a new employee
	Then the employee can be seen on the list


Scenario: Update Info
	Given naviagtes to 'My Info' Page
	When user updates info
	Then the info is updated successfully