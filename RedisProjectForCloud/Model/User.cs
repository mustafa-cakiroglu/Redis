namespace RedisProjectForCloud.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Twitter { get; set; }
        public string Blog { get; set; }

        public User()
        {

        }
        public User(int id, string firstname, string lastname, string twitter, string blog)
        {
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
            Twitter = twitter;
            Blog = blog;
        }
    }
}
