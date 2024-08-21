namespace PasswordSaver.Models.User
{
    public class Password
    {

        public Password(Guid id, string name, string hashedPassword)
        {
            Id = id;
            Name = name;
            HashedPassword = hashedPassword;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string HashedPassword { get; set; }


        public static Password Create(Guid id, string name, string hashedPassword)
        {
            return new Password(id, name, hashedPassword);
        }
    }
}
