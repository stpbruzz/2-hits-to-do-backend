namespace to_do_api.Data
{
    public class ToDoTask
    {
        public int Id { get; set; }
        public required string Description { get; set; }
        public bool flag { get; set; } = false;
    }
}