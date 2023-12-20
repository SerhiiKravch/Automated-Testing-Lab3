using System;
using Newtonsoft.Json;
using RestSharp;
using NUnit.Framework;
using TechTalk.SpecFlow;
using Test_Lab_3.Models.Booking;
using Test_Lab_3.Drivers;
using TechTalk.SpecFlow.Assist;

namespace Test_Lab_3.StepDefinitions.Part1
{
    [Binding]
    public class CreateBooking
    {
        private RestClient _client;
        private RestRequest _request;
        private RestResponse _response;
        private string baseURL = "https://restful-booker.herokuapp.com/booking";
        private BookingRequest _bookingRequest;

        [Given(@"I have the following booking details")]
        public void GivenIHaveTheFollowingBookingDetails(Table table)
        {
            _bookingRequest = table.CreateInstance<BookingRequest>();
            _bookingRequest.bookingdates = table.CreateInstance<Dates>();
            _client = new RestClient(baseURL);
            _request = new RestRequest(baseURL, Method.Post);
            _request.AddJsonBody(_bookingRequest);
            _request.AddHeader("Accept", "application/json");
        }

        [When(@"next POST request is sent")]
        public void WhenNextPOSTRequestIsSent()
        {
            _response = _client.Execute(_request);
        }

        [Then(@"the response should have a booking ID and correct booking details")]
        public void ThenTheResponseShouldHaveABookingIDAndCorrectBookingDetails()
        {
            var _newBooking = JsonConvert.DeserializeObject<Response>(_response.Content);
            Assert.IsNotNull(_newBooking.bookingid);
            Assert.That(_bookingRequest.firstname, Is.EquivalentTo(_newBooking.booking.firstname));
        }
    }
}
