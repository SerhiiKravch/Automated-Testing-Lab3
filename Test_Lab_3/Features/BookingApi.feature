Feature: CRUD operations on Booking API

Scenario: Add a new booking
  Given I have the following booking details
    | firstname | lastname | totalprice | depositpaid | checkin      | checkout     | additionalneeds |
    | Jack   | Layton | 1200  | true           | 2023-05-20   | 2023-10-30   | Dinner	   |
	When next POST request is sent
	Then the response should have a booking ID and correct booking details

Scenario: Get a list of booking IDs
  Given I want to retrieve a list of booking IDs
  When I send a GET request to "/booking"
  Then I should receive a response with status code 200
  And I should receive a list of booking IDs	

Scenario: Updating a booking with valid details
  Given Create booking for updating
  And I have valid booking details
  When I send a PUT request to update the booking
  Then the booking should be updated with the new details

Scenario: Deleting a booking
   Given Create booking for deletion
   When I send a DELETE request to remove a booking
   Then the booking should be successfully deleted