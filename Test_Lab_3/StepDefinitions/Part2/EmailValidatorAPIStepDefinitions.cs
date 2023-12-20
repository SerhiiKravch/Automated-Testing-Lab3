using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Net;
using System.Net.Mail;
using TechTalk.SpecFlow;

namespace Test_Lab_3.StepDefinitions.Part2
{
    public class EmailInfo
    {
        public string domain { get; set; }
        public string hostname { get; set; }
        public string username { get; set; }
        public bool disposable { get; set; }
        public bool webmail { get; set; }
        public bool mxvalid { get; set; }
    }

    [Binding]
    public class EmailValidatorAPIStepDefinitions
    {
        private readonly RestClient _client;
        private RestRequest _request;
        private RestResponse _response;

        public EmailValidatorAPIStepDefinitions()
        {
            _client = new RestClient("https://scraper.run/");
        }

        [Given(@"a valid email address: '([^']*)'")]
        public void GivenAValidEmailAddress(string p0)
        {
            _request = new RestRequest("email", Method.Get);
            _request.AddParameter("addr", p0);

        }

        [When(@"I query the EmailValidator API")]
        public void WhenIQueryTheEmailValidatorAPI()
        {
            _response = _client.Execute(_request);
            Console.WriteLine(_response);
        }

        [Then(@"I should receive domain '([^']*)'")]
        public void ThenIShouldReceiveDomain(string p0)
        {
            Assert.NotNull(_response);
            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);

            var result = JsonConvert.DeserializeObject<EmailInfo>(_response.Content);
            Assert.NotNull(result);
            Assert.AreEqual(p0, result.domain);
        }
        [Then(@"I should receive an error response")]
        public void ThenIShouldReceiveAnErrorResponse()
        {
            Assert.IsFalse(_response.IsSuccessful, "The response was successful but should have been an error.");
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest)
            .Or.EqualTo(HttpStatusCode.NotFound)
            .Or.EqualTo(HttpStatusCode.InternalServerError), "The status code was not an expected error code.");
        }
    }
}
