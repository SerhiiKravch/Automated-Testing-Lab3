namespace Test_Lab_3.Models.Booking
{

    internal class BookingRequest
    {

        public string firstname { get; set; }
        public string lastname { get; set; }
        public int totalprice { get; set; }
        public bool depositpaid { get; set; }
        public Dates bookingdates { get; set; }
        public string additionalneeds { get; set; }
    }
}