namespace Tests
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var turpgine = new Engine.Turpgine();

            var testGraphicalInterface = new TestingGraphicalInterface(turpgine);
        }
    }
}