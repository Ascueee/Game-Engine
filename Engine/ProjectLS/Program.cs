namespace ProjectLS;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Opening LootNShoot");
        
        using (Game game = new Game( 1920, 900, "HoneyEngine | TEST"))
        {
            game.Run();
        }
    }
}