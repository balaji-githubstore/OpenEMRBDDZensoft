Feature: Patient
	In order to maintain patients record
	As a admin
	I want to add, edit, delete patient details in the portal 

Scenario Outline: Add Patient
	Given I have browser with OpenEmr url
	When I enter username as 'admin'
	And I enter password as 'pass'
	And I select language as 'English (Indian)'
	And I click on login
	And I click on patient-client
	And I click on patients
	And I click on add new patient
	And I fill the patient detail
		| firstname | lastname | dob   | gender   | licensenumber |
		| <fname>   | <lname>  | <dob> | <gender> | <license>     |
	And I click on create new patient
	And I click on confirm create new patient
	And I store the text and handle the alert
	And I close the happy birthday popup if dob is today's date
	Then I should get the alert message as '<expectedalert>'
	And I should get the added patient detail as '<expectedaddedpatient>'

	Examples:
		| fname | lname | dob        | gender | license | expectedalert | expectedaddedpatient                   |
		| john  | wick  | 2021-09-26 | Male   | 7878    | Tobacco       | Medical Record Dashboard - John Wick   |
		| peter | kenny | 2021-10-22 | Female | 7878    | Tobacco       | Medical Record Dashboard - Peter Kenny |