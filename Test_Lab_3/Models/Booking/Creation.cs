using Newtonsoft.Json;
using RestSharp;

namespace Test_Lab_3.Models.Booking
{
    internal class Creation
    {
        private RestClient client;
        private RestRequest request;
        private string baseUrl = "https://restful-booker.herokuapp.com/booking";
        private BookingRequest booking = new BookingRequest
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

        public int Create()
        {
            client = new RestClient(baseUrl);
            request = new RestRequest(baseUrl, Method.Post);
            request.AddJsonBody(booking);
            request.AddHeader("Accept", "application/json");
            string jsonResponse = client.Execute(request).Content;
            Response bookingResponse = JsonConvert.DeserializeObject<Response>(jsonResponse);
            int id = bookingResponse.bookingid;
            return id;
        }
    }

}