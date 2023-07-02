namespace sunstealer.mvc.Services {
    // ajm: interface
    public interface IGeneric
    {
        void ConsoleWriteMessage(string message);
    }

    // ajm: service
    public class Generic : IGeneric
    {
        public void ConsoleWriteMessage(string message)
        {
            System.Console.WriteLine($"GenericService.ConsoleWriteMessage message: {message}");
        }
    }
}