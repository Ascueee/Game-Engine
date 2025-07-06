namespace ProjectLS;

class Program
{
    private static string title = "Honey Engine | PHYSICS TEST";
    static void Main(string[] args)
    {
        Console.WriteLine("Opening LootNShoot");
        
        using (Game game = new Game( 900, 500, title))
        {
            game.Run();
        }
    }
}