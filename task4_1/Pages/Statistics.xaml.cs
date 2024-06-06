namespace task4_1.Pages;

public partial class Statistics : ContentPage
{
    
    
    MainViewModel vm;
    public double cowMilkPrice = 9.4; // $ per kg
    public double sheepWoolPrice = 6.2; // $ per kg
    public double governmentTaxRate = 0.02; // Government tax rate per kg per day
    public double totalWeight;
    public double GovernmentTaxDaily;
    public double avgweight;
    public int countSheep;
    public int countCow;
    public Statistics(MainViewModel vm)
	{
		InitializeComponent();
        this.vm = vm;   
		BindingContext = vm;
	}

    //comment from jessie 
    private void CalulateStats(object sender, EventArgs e)
    {
        
        totalWeight = vm.Animals.Sum(animal => animal.Weight);
        GovernmentTaxDaily = governmentTaxRate * totalWeight;
        avgweight = totalWeight / vm.Animals.Count;
        countSheep = vm.Animals.Count(animal => animal is Sheep);
        countCow = vm.Animals.Count(animal => animal is Cow);

        // Calculate farm daily profit
        
        double totalIncome = vm.Animals.Sum(animal =>
        {
            if (animal is Cow cow)
                return cow.Milk * cowMilkPrice;
            else if (animal is Sheep sheep)
                return sheep.Wool * sheepWoolPrice;
            else
                return 0;
        });
        double totalCost = vm.Animals.Sum(animal => animal.Cost);
        double FarmDailyProfit = totalIncome - totalCost - (GovernmentTaxDaily);
        farmdailyprofit.Text = $"$ {FarmDailyProfit:F2}";
        govttax.Text = $"$ {GovernmentTaxDaily *30:F2}";
        averageweight.Text = $"$ {avgweight:F2}";
               
        double cowprofit = vm.Animals.Sum(animal => 
        {
            if (animal is Cow cow)
            {
                return (cow.Milk * cowMilkPrice)- cow.Cost - (cow.Milk * governmentTaxRate);
            }
            else return 0;
        }
        );
        
        cowdailyprft.Text = $"$ {cowprofit:F2}";
        avgcowdailyprft.Text = $"$ {(cowprofit / countCow):F2}";
        
        double sheepprofit = vm.Animals.Sum(animal =>
        {
            if (animal is Sheep sheep)
            {
                return (sheep.Wool * sheepWoolPrice) - sheep.Cost - (sheep.Wool * governmentTaxRate);
            }
            else return 0;
        }
        );
        sheepdailyprft.Text = $"${sheepprofit:F2}";
        avgsheepdailyprft.Text = $"${(sheepprofit / countSheep):F2}";
    }

    private void OnResetClicked(object sender, EventArgs e)
    {
        sheepdailyprft.Text = string.Empty;
        avgsheepdailyprft.Text = string.Empty;
        cowdailyprft.Text = string.Empty;
        avgcowdailyprft.Text = string.Empty;
        averageweight.Text = string.Empty;
        govttax.Text = string.Empty;
        farmdailyprofit.Text = string.Empty;

    }
}
