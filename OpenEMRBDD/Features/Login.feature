@login
Feature: Login
	In order to maintain the health records
	As a portal user
	I want to login to the openemr portal 

Background:
	Given I have 'ff' browser with OpenEmr url

	@invalid @low
Scenario: Invalid Credential
	When I enter username as 'jack'
	And I enter password as 'jack123'
	And I select language as 'Dutch'
	And I click on login
	Then I should get the error stating 'Invalid username or password'

	@valid @high @ignore
Scenario Outline: Valid Credential
	When I enter username as '<username>'
	And I enter password as '<password>'
	And I select language as '<language>'
	And I click on login
	Then I should get access to the dashboard with text '<waitfortext>' and title as '<expectedvalue>'

	Examples:
		| username   | password   | language         | waitfortext | expectedvalue |
		| admin      | pass       | English (Indian) | Messages    | OpenEMR       |
		| accountant | accountant | English (Indian) | About       | OpenEMR       |
		| physician  | physician  | English (Indian) | Calendar    | OpenEMR       |