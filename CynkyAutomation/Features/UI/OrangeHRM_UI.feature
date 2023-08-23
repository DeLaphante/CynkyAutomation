Feature: OrangeHRM_UI

Background: Navigate to home page
	Given user is on the Orange HRM homepage
	When user with the following details logs in:
		| Property | Value    |
		| Username | Admin    |
		| Password | admin123 |


Scenario: Delete record on Orange HRM
	And user navigates to 'PIM' Page
	When user deletes employee in row without employment status 'Full-Time Permanent'
	Then the employee should not be displayed


Scenario: Add new employee
	And user navigates to 'PIM' Page
	When user adds a new employee
	Then the employee can be seen on the list


Scenario: Update Info
	And user navigates to 'My Info' Page
	When user updates info
	Then the info is updated successfully