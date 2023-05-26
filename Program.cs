namespace ConOperatorok
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Expression> expressions = File.ReadAllLines("DataSource\\kifejezesek.txt")
                .ToList().Select(csvLine => new Expression(csvLine)).ToList();

            Console.WriteLine($"2. feladat: Kifejezések száma: {expressions.Count}");

            int resultCount = expressions.Count(x => x.Operation == "mod");
            Console.WriteLine($"3. feladat: Kifejezések maradékos osztással: {resultCount}");

            bool resultAny = expressions.Any(x => x.OperandLeft % 10 == 0 && x.OperandRight % 10 == 0);
            Console.WriteLine($"4. feladat: {(resultAny ? "Van" : "Nincs")} ilyen kfejezés");

            Console.WriteLine("5. feladat: Statisztika");
            expressions.Where(x => x.IsValidOperation)
                       .GroupBy(x => x.Operation)
                       .ToList()
                       .ForEach(x => Console.WriteLine($"\t{x.Key} -> {x.Count()} db"));

            string stringFromConsole = "";
            do
            {
                Console.Write($"7. feladat: Kérek egy kifejezést (pl.: 1 + 1):");
                stringFromConsole = Console.ReadLine();
                if (stringFromConsole.ToLower() != "vége")
                {
                    Console.WriteLine($"\t{new Expression(stringFromConsole).Result}");
                }
            } while (stringFromConsole.ToLower() != "vége");

            Console.WriteLine("8. feladat: eredmenyek.txt");
            File.WriteAllLines("eredmenyek.txt", expressions.Select(ob => ob.Result));
        }
    }
}