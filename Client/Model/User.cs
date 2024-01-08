namespace Socials.Client.Client.Model
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime? EmailVerifiedAt { get; set; }
        public string? GoogleId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Photo { get; set; }
        public int? Age { get; set; }
        public int VerifiedId { get; set; }
        public int Hidden { get; set; }
        public DateTime? Birthdate { get; set; }
        public bool? Verified { get; set; }

        public string? Password { get; set; }

        public int AgeCalc => CalculateAgeCorrect(Birthdate, DateTime.Now);
        public int CalculateAgeCorrect(DateTime? birthDate, DateTime now)
        {
            if(birthDate == null)
            {
                return 0;
            }
            int age = now.Year - birthDate.Value.Year;

            if (now.Month < birthDate.Value.Month || (now.Month == birthDate.Value.Month && now.Day < birthDate.Value.Day))
                age--;

            return age;
        }

    }
}
