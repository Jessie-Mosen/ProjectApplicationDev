namespace task4_1.Pages;

public partial class Statistics : ContentPage
{
    MainViewModel vm;

    public Statistics(MainViewModel vm)
    {
        InitializeComponent();
        this.vm = vm;
        BindingContext = vm;
    }

    private async void IDAnimalBtn_click(object sender, EventArgs e)
    {
        double cowMilkPrice = 9.4; // $ per kg
        double sheepWoolPrice = 6.2; // $ per kg
        double governmentTaxRate = 0.02; // Government tax rate per kg per day

        double cowProfitPerCow = 0;
        double sheepProfitPerSheep = 0;

        foreach (var animal in vm.Animals)
        {
            if (animal is Cow cow)
            {
                double dailyRevenue = cow.Milk * cowMilkPrice;
                double dailyTax = dailyRevenue * governmentTaxRate; // Corrected tax calculation
                cowProfitPerCow = dailyRevenue - cow.Cost - dailyTax;
                break;
            }
        }

        foreach (var animal in vm.Animals)
        {
            if (animal is Sheep sheep)
            {
                double dailyRevenue = sheep.Wool * sheepWoolPrice;
                double dailyTax = dailyRevenue * governmentTaxRate;
                sheepProfitPerSheep = dailyRevenue - sheep.Cost - dailyTax;
                break;
            }
        }

        string selectedType = typePicker.SelectedItem?.ToString();
        if (string.IsNullOrEmpty(selectedType))
        {
            await DisplayAlert("Error", "Please select an animal type.", "OK");
            return;
        }

        if (!int.TryParse(IDAnimalAmount.Text, out int animalAmount) || animalAmount <= 0)
        {
            await DisplayAlert("Error", "Invalid input. Please enter a valid integer greater than 0.", "OK");
            return;
        }

        double totalProfit;
        if (selectedType.Equals("Cow", StringComparison.OrdinalIgnoreCase))
        {
            totalProfit = cowProfitPerCow * animalAmount;
            ResultLabel.Text = $"Type of Animal: Cow\nEstimated daily profit: ${totalProfit:F2}";
        }
        else if (selectedType.Equals("Sheep", StringComparison.OrdinalIgnoreCase))
        {
            totalProfit = sheepProfitPerSheep * animalAmount;
            ResultLabel.Text = $"Type of Animal: Sheep\nEstimated daily profit: ${totalProfit:F2}";
        }
        else
        {
            ResultLabel.Text = "Animal type not found";
        }
    }

    private void OnResetClicked(object sender, EventArgs e)
    {
        IDAnimalAmount.Text = string.Empty;
        ResultLabel.Text = string.Empty;

        sheepdailyprft.Text = string.Empty;
        avgsheepdailyprft.Text = string.Empty;
        cowdailyprft.Text = string.Empty;
        avgcowdailyprft.Text = string.Empty;
        averageweight.Text = string.Empty;
        govttax.Text = string.Empty;
        farmdailyprofit.Text = string.Empty;
    }

    private void OnTypeSelectedIndexChanged(object sender, EventArgs e)
    {
        // This method is intentionally left blank, can be used for future event handling
    }

    private void CalulateStats(object sender, EventArgs e)
    {
        double totalWeight = vm.Animals.Sum(animal => animal.Weight);
        double GovernmentTaxDaily = vm.governmentTaxRate * totalWeight;
        double avgweight = totalWeight / vm.Animals.Count;
        int countSheep = vm.Animals.Count(animal => animal is Sheep);
        int countCow = vm.Animals.Count(animal => animal is Cow);

        // Calculate farm daily profit
        double totalIncome = vm.Animals.Sum(animal =>
        {
            if (animal is Cow cow)
                return cow.Milk * vm.cowMilkPrice;
            else if (animal is Sheep sheep)
                return sheep.Wool * vm.sheepWoolPrice;
            else
                return 0;
        });
        double totalCost = vm.Animals.Sum(animal => animal.Cost);
        double FarmDailyProfit = totalIncome - totalCost - GovernmentTaxDaily;
        farmdailyprofit.Text = $"$ {FarmDailyProfit:F2}";
        govttax.Text = $"$ {GovernmentTaxDaily * 30:F2}";
        averageweight.Text = $"{avgweight:F2} kg";

        double cowprofit = vm.Animals.Sum(animal =>
        {
            if (animal is Cow cow)
            {
                return (cow.Milk * vm.cowMilkPrice) - cow.Cost - (cow.Milk * vm.governmentTaxRate);
            }
            else return 0;
        });
        cowdailyprft.Text = $"$ {cowprofit:F2}";
        avgcowdailyprft.Text = $"$ {(cowprofit / countCow):F2}";

        double sheepprofit = vm.Animals.Sum(animal =>
        {
            if (animal is Sheep sheep)
            {
                return (sheep.Wool * vm.sheepWoolPrice) - sheep.Cost - (sheep.Wool * vm.governmentTaxRate);
            }
            else return 0;
        });
        sheepdailyprft.Text = $"${sheepprofit:F2}";
        avgsheepdailyprft.Text = $"${(sheepprofit / countSheep):F2}";
    }
}
