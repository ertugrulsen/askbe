Feature: Login
	

@mytag
Scenario: Logging in with valid credentials
	Given I am at the login page
  When I fill in the following form
  | field | value |
  | Email | test |
  | Password | 123 |
  And I click the login button
  Then I should be at the home page