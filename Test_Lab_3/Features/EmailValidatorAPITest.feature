Feature: Email Validator API

Scenario: Query EmailValidator API with a valid email address
	Given a valid email address: 'kravchyshynserg@gmail.com'
	When I query the EmailValidator API
	Then I should receive domain 'gmail.com'

Scenario: Query EmailValidator API with a valid yahoo-email address
	Given a valid email address: 'kravchyshynserg@yahoo.com'
	When I query the EmailValidator API
	Then I should receive domain 'yahoo.com'

Scenario: Query EmailValidator API without entered email address
	Given a valid email address: ''
	When I query the EmailValidator API
	Then I should receive an error response

