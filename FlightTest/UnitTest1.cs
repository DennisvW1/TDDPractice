using FluentAssertions;
using Domain;

namespace FlightTest
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(3, 1, 2)]
        [InlineData(6, 3, 3)]
        [InlineData(10, 6, 4)]
        public void Booking_reduces_the_number_of_seats(int seatCapacity, int numberOfSeatsBooked, int remainingNumberOfSeats)
        {
            var flight = new Flight(seatCapacity: seatCapacity);

            flight.Book("dennis@gmail.com", numberOfSeatsBooked);

            flight.RemainingNumberOfSeats.Should().Be(remainingNumberOfSeats);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(3, 4)]
        [InlineData(11, 12)]
        public void Avoids_overbooking(int numberOfSeats, int seatsBooked) {
            var flight = new Flight(numberOfSeats);

            var error = flight.Book("dennis@gmail.com", seatsBooked);

            error.Should().BeOfType<OverbookingError>();
        }

        [Theory]
        [InlineData(3, 1)]
        [InlineData(3, 3)]
        public void Books_flight_successfully(int numberOfSeats, int seatsBooked) {
            var flight = new Flight(numberOfSeats);

            var error = flight.Book("dennis@gmail.com", seatsBooked);

            error.Should().BeNull();
        }

        [Fact]
        public void Remembers_bookings() {
            var flight = new Flight(150);

            flight.Book("dennis@gmail.com", 4);
            flight.BookingList.Should().ContainEquivalentOf(new Booking("dennis@gmail.com", 4));
        }
    }
}