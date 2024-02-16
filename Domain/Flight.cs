namespace Domain
{
    public class Flight
    {

        public int? RemainingNumberOfSeats { get; set; }
        private List<Booking> bookingList = new List<Booking>();
        public IEnumerable<Booking> BookingList => bookingList;

        public Flight(int seatCapacity)
        {
            RemainingNumberOfSeats = seatCapacity;
        }

        public object? Book(string email, int numberOfSeats) {

            if (numberOfSeats > RemainingNumberOfSeats) {
                return new OverbookingError();
            }

            RemainingNumberOfSeats -= numberOfSeats;

            bookingList.Add(new Booking(email, numberOfSeats));

            return null;
        }

        public object? CancelBooking(string email, int? numberOfSeats = null) {

            if (numberOfSeats == null)
            {
                if (BookingList.Any(b => b.Email == email))
                {
                    var idx = bookingList.FindIndex(b => b.Email == email);
                    Booking booking = bookingList[idx];

                    bookingList.RemoveAt(idx);
                    RemainingNumberOfSeats += booking.NumberOfSeats;
                }

                return null;
            } else 
            {
                if (BookingList.Any(b => b.Email == email))
                {
                    var idx = bookingList.FindIndex(b => b.Email == email);
                    Booking booking = bookingList[idx];

                    bookingList[idx] = new Booking(email, numberOfSeats);
                    RemainingNumberOfSeats += numberOfSeats;
                }
            }

            return null;
        }

    }
}
