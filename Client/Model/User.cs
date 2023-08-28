namespace Socials.Client.Client.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public string NickName { get; set; }
        public DateTime BirthDay { get; set; }
        public string Email { get; set; }
        public string ImageName { get; set; }

        public User(string nick)
        {
            Id = Guid.NewGuid();
            NickName = nick;
            BirthDay = DateTime.Now;
            Email= string.Empty;
            ImageName = "icon-192.png";
        }


    }
}
