namespace Domain
{
    public class Booking
    {
        public string Email { get; set; }
        public int numberOfSeats { get; set; }

        public Booking(string email, int numberOfSeats)
        {
            this.Email = email;
            this.numberOfSeats = numberOfSeats;
        }
    }
}
