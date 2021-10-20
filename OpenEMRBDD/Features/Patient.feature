Feature: Patient
	In order to maintain patients record
	As a admin
	I want to add, edit, delete patient details in the portal 


Scenario: Add Patient
	Given I have browser with OpenEmr url
	When I enter username as 'admin'
	And I enter password as 'pass'
	And I select language as 'English (Indian)'
	And I click on login
	And I click on patient-client
	And I click on patients 
	And I click on add new patient

