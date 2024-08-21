namespace PasswordSaver.Entities
{
    public class PasswordEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string HashedPassword { get; set; }
    }
}
