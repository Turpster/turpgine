namespace Tests
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var engine = new Engine.Engine();

            var testGraphicalInterface = new TestingGraphicalInterface(engine.GraphicalManager);
        }
    }
}