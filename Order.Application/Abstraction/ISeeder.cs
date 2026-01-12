namespace Order.Application.Abstraction
{
    public interface ISeeder
    {
        public int ExecutionOrder { get; set; }
        Task SeedAsync();
    }
}
