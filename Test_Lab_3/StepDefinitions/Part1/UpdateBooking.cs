using Newtonsoft.Json;
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
    public class UpdateBooking
    {
        private BookingApi bookingApi;
        private RestResponse _response;
        private BookingRequest booking;

        public int id;
        public UpdateBooking()
        {
            bookingApi = new BookingApi();
            _response = new RestResponse();
        }
        [Given(@"Create booking for updating")]
        public void GivenCreateBookingForUpdating()
        {
            Creation c = new Creation();
            id = c.Create();
        }

        [Given(@"I have valid booking details")]
        public void GivenIHaveValidBookingDetails()
        {
            booking = new BookingRequest
            {
                firstname = "Alan",
                lastname = "Rickman",
                totalprice = 111,
                depositpaid = true,
                bookingdates = new Dates
                {
                    checkin = "1946-02-21",
                    checkout = "2016-01-14"
                }
            };
        }

        [When(@"I send a PUT request to update the booking")]
        public void WhenISendAPUTRequestToUpdateTheBooking()
        {
            _response = bookingApi.UpdateBooking(booking, id);
        }

        [Then(@"the booking should be updated with the new details")]
        public void ThenTheBookingShouldBeUpdatedWithTheNewDetails()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Console.WriteLine("Response Content: " + _response.Content);
            var updatedBooking = JsonConvert.DeserializeObject<BookingRequest>(_response.Content);
            Assert.That(booking.firstname, Is.EqualTo(updatedBooking.firstname));
            Assert.That(booking.lastname, Is.EqualTo(updatedBooking.lastname));
            Assert.That(booking.totalprice, Is.EqualTo(updatedBooking.totalprice));
            Assert.That(booking.depositpaid, Is.EqualTo(updatedBooking.depositpaid));
            Assert.That(booking.bookingdates.checkin, Is.EqualTo(updatedBooking.bookingdates.checkin));
            Assert.That(booking.bookingdates.checkout, Is.EqualTo(updatedBooking.bookingdates.checkout));
            Assert.That(booking.additionalneeds, Is.EqualTo(updatedBooking.additionalneeds));
        }
    }
}
