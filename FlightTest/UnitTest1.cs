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
            flight.RemainingNumberOfSeats.Should().Be(numberOfSeats - seatsBooked);
            error.Should().BeNull();
        }

        [Fact]
        public void Remembers_bookings() {
            var flight = new Flight(150);

            flight.Book("dennis@gmail.com", 4);
            flight.BookingList.Should().ContainEquivalentOf(new Booking("dennis@gmail.com", 4));
        }

        [Theory]
        [InlineData(150, "dennis@gmail.com", 4, 2, 148)]
        [InlineData(150, "dennis@gmail.com", 11, 6, 145)]
        [InlineData(150, "dennis@gmail.com", 60, 10, 100)]
        [InlineData(150, "dennis@gmail.com", 150, 25, 25)]
        public void Unbooking_increase_number_of_seats(int numberOfSeats, string email, int seatsBooked, int seatsCancelled, int shouldBeSeats)
        {
            var flight = new Flight(numberOfSeats);
            flight.Book(email, seatsBooked);
            flight.CancelBooking(email, seatsCancelled);

            flight.RemainingNumberOfSeats.Should().Be(shouldBeSeats);
        }

        [Theory]
        [InlineData("dennis@gmail.com", 4)]
        [InlineData("dennis.vw@gmail.com", 11)]
        public void Unbooking_removes_booking(string email, int numberOfSeats)
        {
            var flight = new Flight(150);
            flight.Book(email, numberOfSeats);
            
            flight.CancelBooking(email);
            
            flight.BookingList.Should().NotContainEquivalentOf(new Booking(email, numberOfSeats));
        }
    }
}