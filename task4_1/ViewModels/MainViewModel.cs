namespace task4_1.ViewModels;

public class MainViewModel
{
    public ObservableCollection<Animal> Animals { get; set; }
    public readonly Database _database;
    public double cowMilkPrice = 9.4; // $ per kg
    public double sheepWoolPrice = 6.2; // $ per kg
    public double governmentTaxRate = 0.02; // Government tax rate per kg per day

    public MainViewModel()
    {
        _database = new Database(); // Assuming Database is your data access class
        Animals = new ObservableCollection<Animal>();
        _database.ReadItems().ForEach(x => Animals.Add(x));
    }

    public double CalculateCowProfit(Cow cow)
    {
        double income = cow.Milk * cowMilkPrice;
        double tax = cow.Weight * governmentTaxRate;
        return income - cow.Cost - tax;
    }

    public double CalculateSheepProfit(Sheep sheep)
    {
        double income = sheep.Wool * sheepWoolPrice;
        double tax = sheep.Weight * governmentTaxRate;
        return income - sheep.Cost - tax;
    }

    public double AvgCowDailyProfit()
    {
        var cows = Animals.OfType<Cow>().ToList();
        if (!cows.Any()) return 0;

        double totalCowProfit = cows.Sum(cow => CalculateCowProfit(cow));
        return totalCowProfit / cows.Count;
    }

    public double AvgSheepDailyProfit()
    {
        var sheep = Animals.OfType<Sheep>().ToList();
        if (!sheep.Any()) return 0;

        double totalSheepProfit = sheep.Sum(sheep => CalculateSheepProfit(sheep));
        return totalSheepProfit / sheep.Count;
    }

    public double CalculateAvgGovernmentTax()
    {
        double totalWeight = Animals.Sum(animal => animal.Weight);
        return governmentTaxRate * totalWeight;
    }

    public double CalculateTotalCowProfit()
    {
        var cows = Animals.OfType<Cow>().ToList();
        return cows.Sum(cow => CalculateCowProfit(cow));
    }

    public double CalculateTotalSheepProfit()
    {
        var sheep = Animals.OfType<Sheep>().ToList();
        return sheep.Sum(sheep => CalculateSheepProfit(sheep));
    }

    public double CalculateTotalFarmProfit()
    {
        return CalculateTotalCowProfit() + CalculateTotalSheepProfit();
    }

    public double CalculateAverageWeight()
    {
        if (!Animals.Any()) return 0;
        return Animals.Average(animal => animal.Weight);
    }

    public void AddItem(Animal animal)
    {
        int r = _database.InsertItem(animal);
        if (r > 0)
        {
            Animals.Add(animal);
        }
    }
}
