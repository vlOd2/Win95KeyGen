using System;
using System.Reflection;

public class Win95KeyGen
{
    public const int MIN_DAY = 1;
    public const int MAX_DAY = 366;
    public const int MIN_YEAR = 1995;
    public const int MAX_YEAR = 2003;
    public static readonly Random Rnd = new Random();
    public static readonly string? ASSEMBLY_NAME = Assembly.GetExecutingAssembly().GetName().Name;

    public static void Main(string[] args)
    {
        string type = args.Length < 2 ? "help" : (args[0] != "retail" && args[0] != "oem" ? "help" : args[0]);
        int amount = 0;

        if (type == "help" || !int.TryParse(args[1], out amount))
        {
            Console.Error.WriteLine($"Usage: {(ASSEMBLY_NAME != null ? ASSEMBLY_NAME : "Win95KeyGen")} <type> <amount>");
            Console.Error.WriteLine($"Valid types are: retail, oem");
            return;
        }

        switch (type)
        {
            case "retail":
                Console.WriteLine("Retail Keys:");

                for (int i = 0; i < amount; i++) 
                    Console.WriteLine($"[{i}/{amount - 1}] {GenerateRetailKey()}");

                break;
            case "oem":
                Console.WriteLine("OEM Keys:");

                for (int i = 0; i < amount; i++) 
                    Console.WriteLine($"[{i}/{amount - 1}] {GenerateOEMKey()}");

                break;
        }
    }

    public static bool CanDevideWithoutPeriod(int number, int devisor)
    {
        while (devisor % 2 == 0) { devisor /= 2; }
        while (devisor % 5 == 0) { devisor /= 5; }
        return number % devisor == 0;
    }

    private static int ConcatInt(int a, int b)
    {
        return int.Parse($"{a}{b}");
    }

    private static int GetRetailSecondSegment()
    {
        int number = 0;
        int sum = 1;

        while (!CanDevideWithoutPeriod(sum, 7))
        {
            number = 0;
            sum = 0;

            for (int i = 0; i < 7; i++) 
            {
                int digit;
                if (i == 6) 
                    digit = Rnd.Next(0, 8);
                else
                    digit = Rnd.Next(0, 9);

                number = ConcatInt(number, digit);
                sum += digit;
            }
        }

        return number;
    }

    private static int GetOEMThirdSegment()
    {
        int number = 0;
        int sum = 1;

        while (!CanDevideWithoutPeriod(sum, 7))
        {
            number = 0;
            sum = 0;

            for (int i = 0; i < 6; i++) 
            {
                int digit;
                if (i == 5) 
                    digit = Rnd.Next(0, 8);
                else
                    digit = Rnd.Next(0, 9);

                number = ConcatInt(number, digit);
                sum += digit;
            }
        }

        return number;
    }

    public static string GenerateRetailKey()
    {
        int firstSegment = 999;

        while (firstSegment == 333 || 
            firstSegment == 444 || 
            firstSegment == 555 || 
            firstSegment == 666 || 
            firstSegment == 777 || 
            firstSegment == 888 || 
            firstSegment == 999)
            firstSegment = Rnd.Next(0, 999);

        return $"{firstSegment:000}-{GetRetailSecondSegment():0000000}";
    }

    public static string GenerateOEMKey()
    {
        return $"{Rnd.Next(MIN_DAY, MAX_DAY):000}{(Rnd.Next(MIN_YEAR, MAX_YEAR) % 100):00}-OEM-0{GetOEMThirdSegment():000000}-{Rnd.Next(0, 100000):00000}";
    }
}