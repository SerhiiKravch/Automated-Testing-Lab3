using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow;
using Test_Lab_3.Models.Booking;

namespace Test_Lab_3.StepDefinitions.Part1
{
    [Binding]
    public class ReadBooking
    {
        private RestClient _client;
        private RestRequest _request;
        private RestResponse _response;

        [Given(@"I want to retrieve a list of booking IDs")]
        public void GivenIWantToRetrieveAListOfBookingIDs()
        {
            _client = new RestClient("https://restful-booker.herokuapp.com/");
            _request = new RestRequest("booking", Method.Get);
        }

        [When(@"I send a GET request to ""([^""]*)""")]
        public void WhenISendAGETRequestTo(string p0)
        {
            _response = _client.Execute(_request);
        }

        [Then(@"I should receive a response with status code (.*)")]
        public void ThenIShouldReceiveAResponseWithStatusCode(int p0)
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Then(@"I should receive a list of booking IDs")]
        public void ThenIShouldReceiveAListOfBookingIDs()
        {
            var bookings = JsonConvert.DeserializeObject<List<Dictionary<string, int>>>(_response.Content);
            Assert.IsNotNull(bookings);
            Assert.IsTrue(bookings.All(b => b.ContainsKey("bookingid")));
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
