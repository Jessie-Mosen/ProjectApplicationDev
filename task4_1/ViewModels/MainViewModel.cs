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
        _database = new();
        Animals = new();
        _database.ReadItems().ForEach(x => Animals.Add(x));
    }
    public void AddItem(Animal ani)
    {
        int r = _database.InsertItem(ani);
        if(r > 0)
        {
            Animals.Add(ani);
        }
    }
}
