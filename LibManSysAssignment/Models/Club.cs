namespace LibManSysAssignment.Models
{
    public class Club
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }
}
