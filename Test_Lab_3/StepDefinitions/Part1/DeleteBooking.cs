using NUnit.Framework;
using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;
using Test_Lab_3.Drivers;
using Test_Lab_3.Models.Booking;

namespace Test_Lab_3.StepDefinitions.Part1
{
    [Binding]
    public class DeleteBooking
    {
        private BookingApi _bookingApi;
        private RestResponse _response;

        public int id;
        public DeleteBooking()
        {
            _bookingApi = new BookingApi();
            _response = new RestResponse();
        }

        [Given(@"Create booking for deletion")]
        public void GivenCreateBookingForDeletion()
        {
            Creation c = new Creation();
            id = c.Create();
        }

        [When(@"I send a DELETE request to remove a booking")]
        public void WhenISendADELETERequestToRemoveABooking()
        {
            _response = _bookingApi.DeleteBooking(id);
        }

        [Then(@"the booking should be successfully deleted")]
        public void ThenTheBookingShouldBeSuccessfullyDeleted()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }
    }
}
