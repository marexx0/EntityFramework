using EF_first_.Data;

class Program
{
    static void Main()
    {
        var MusicDb = new MusicDbContext();

        Console.WriteLine("Enter track name:");
        string trackName = Console.ReadLine();

        Console.WriteLine("Enter album ID:");
        int albumId = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter duration:");
        double duration = double.Parse(Console.ReadLine());

        var track = new Track { Name = trackName, AlbumId = albumId, Duration = duration };
        MusicDb.Tracks.Add(track);
        MusicDb.SaveChanges();

        Console.WriteLine("Track added successfully!");
    }
}