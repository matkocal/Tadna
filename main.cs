// Functionality 1. - read CSV file and prints a summary.
// =======================================================

// ── a class is a blueprint for a data structure ──────────────
// This represents one single bite event from the CSV

class BiteEvent
{
    // these are "properties" — data the object holds
    public int    BiteNumber  { get; set; }
    public float  PeakForce   { get; set; }  // Newtons
    public int    Duration    { get; set; }  // milliseconds
    public long   Timestamp   { get; set; }  // ms since boot
}


// ── another class for the whole session ──────────────────────
class Session
{
    // metadata entered by researcher
    public string Species { get; set; } = "";
    public string Sex     { get; set; } = "";
    public float  Mass    { get; set; }  // grams
 
    // List<T> is like a dynamic array in C++: std::vector<BiteEvent>
    public List<BiteEvent> Bites { get; set; } = new List<BiteEvent>();
 
    // ── computed properties (calculated on the fly) ───────────
    public float MaxForce  => Bites.Count > 0 ? Bites.Max(b => b.PeakForce) : 0;
    public float MinForce  => Bites.Count > 0 ? Bites.Min(b => b.PeakForce) : 0;
    public float MeanForce => Bites.Count > 0 ? Bites.Average(b => b.PeakForce) : 0;
    public int   NBites    => Bites.Count;
}

/ ── the main program ─────────────────────────────────────────
class Program
{
    static void Main(string[] args)
    {
        // for now hardcode a test CSV path
        // later this will come from a file picker in the UI
        string csvPath = "test_session.csv";
 
        // check file exists before trying to read
        if (!File.Exists(csvPath))
        {
            Console.WriteLine($"File not found: {csvPath}");
            Console.WriteLine("Creating a test CSV for you...");
            CreateTestCsv(csvPath);
        }
 
        // ── read the CSV ──────────────────────────────────────
        Session session = LoadCsv(csvPath);
 
        // ── ask for metadata ──────────────────────────────────
        // Console.Write is like print without newline
        Console.Write("Species: ");
        session.Species = Console.ReadLine() ?? "";
 
        Console.Write("Sex (M/F): ");
        session.Sex = Console.ReadLine() ?? "";
 
        Console.Write("Mass (g): ");
        // float.TryParse safely converts string → float
        // in C++ you'd use atof() or strtof()
        float.TryParse(Console.ReadLine(), out float mass);
        session.Mass = mass;

              // ── print the table ───────────────────────────────────
        PrintTable(session);
    }
 
    // ── load CSV into a Session object ───────────────────────
    static Session LoadCsv(string path)
    {
        Session session = new Session();
 
        // File.ReadAllLines reads every line into a string array
        // like reading line by line with fgets in C
        string[] lines = File.ReadAllLines(path);
 
        // skip the header line (index 0)
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;
 
            // split by comma — like strtok in C
            string[] parts = line.Split(',');
            if (parts.Length < 4) continue;
 
            // parse each column
            // int.Parse and float.Parse convert string → number
            BiteEvent bite = new BiteEvent
            {
                BiteNumber = int.Parse(parts[0].Trim()),
                PeakForce  = float.Parse(parts[1].Trim()),
                Duration   = int.Parse(parts[2].Trim()),
                Timestamp  = long.Parse(parts[3].Trim()),
            };
 
            session.Bites.Add(bite);
        }
 
        return session;
    }
 
    // ── print the summary table ───────────────────────────────
    static void PrintTable(Session session)
    {
        // $"..." is string interpolation — like sprintf in C
        Console.WriteLine();
        Console.WriteLine($"Session summary");
        Console.WriteLine($"Species : {session.Species}");
        Console.WriteLine($"Sex     : {session.Sex}");
        Console.WriteLine($"Mass    : {session.Mass} g");
        Console.WriteLine();
        Console.WriteLine($"{"Bite",-6} {"Peak (N)",-12} {"Duration (ms)",-16} {"Timestamp (ms)",-16}");
        Console.WriteLine(new string('-', 52));
 
        foreach (BiteEvent bite in session.Bites)
        {
            Console.WriteLine($"{bite.BiteNumber,-6} {bite.PeakForce,-12:F2} {bite.Duration,-16} {bite.Timestamp,-16}");
        }
 
        Console.WriteLine(new string('-', 52));
        Console.WriteLine($"{"Max",-6} {session.MaxForce,-12:F2}");
        Console.WriteLine($"{"Min",-6} {session.MinForce,-12:F2}");
        Console.WriteLine($"{"Mean",-6} {session.MeanForce,-12:F2}");
        Console.WriteLine($"{"N",-6} {session.NBites,-12}");
    }
 
    // ── creates a fake CSV so you can test without hardware ───
    static void CreateTestCsv(string path)
    {
        string[] lines = {
            "bite_n,peak_force_N,duration_ms,timestamp_ms",
            "1,45.32,312,4201",
            "2,67.18,280,8540",
            "3,71.02,295,12830",
            "4,68.44,301,17200",
            "5,52.10,288,21500",
        };
        File.WriteAllLines(path, lines);
        Console.WriteLine("Test CSV created: test_session.csv");
        Console.WriteLine();
    }
}
 
