@Booking_api
Feature: Booking

A short summary of the feature

Background:
	Given an auth token is received

Scenario: Get Booking Ids
	When a get request is made to the booking endpoint
	Then the response should contain a list of booking ids


Scenario: Create a Booking
	When a post request is made to the create booking endpoint
	Then the response should contain a booking id
