namespace Domain
{
    public class Flight
    {

        public int RemainingNumberOfSeats { get; set; }
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

    }
}
