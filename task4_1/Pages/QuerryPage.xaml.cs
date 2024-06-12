namespace task4_1.Pages;

public partial class QuerryPage : ContentPage
{
    MainViewModel vm;
    

    public QuerryPage(MainViewModel vm)
    {
        InitializeComponent();
        this.vm = vm;
        BindingContext = vm;
        

    }

    private void OnTypeSelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedType = typePicker.SelectedItem.ToString();
        colourPicker.SelectedIndex = 0;

        if (selectedType == "Cow" || selectedType == "Sheep")
        {
            colourPicker.IsEnabled = true;
        }
    }

    private void OnSearchClicked(object sender, EventArgs e)
    {
        if (typePicker.SelectedItem is null)
        {
            DisplayAlert("Error", "Did Not select any animal", "OK");
            return;
        }
        if (colourPicker.SelectedItem is null)
        {
            DisplayAlert("Error", "Did Not select colour", "OK");
            return;
        }

        // Get selected type and colour
        string selectedType = typePicker.SelectedItem.ToString();
        string selectedColour = colourPicker.SelectedItem.ToString();

        // Filter animals based on selected criteria
        List<Animal> matchingAnimals = vm.Animals.Where(animal =>
        {
            // Check if the animal matches the selected type and colour
            bool isMatch = false;
            if (selectedType == "Cow" && animal is Cow cow)
            {
                isMatch = selectedColour == "All" || cow.Colour.ToLower() == selectedColour.ToLower();
            }
            else if (selectedType == "Sheep" && animal is Sheep sheep)
            {
                isMatch = selectedColour == "All" || sheep.Colour.ToLower() == selectedColour.ToLower();
            }
            return isMatch;
        }).ToList();

        int totalLivestockCount = matchingAnimals.Count;
        double percentage = (double)totalLivestockCount / vm.Animals.Count * 100;
       

        double totalIncome = 0;
        double totalCost = 0;
        double totalTax = 0;
        double totalWeight = 0;
        double totalProduceAmount = 0;

        foreach (var animal in matchingAnimals)
        {
            double weight = animal.Weight;
            double produceAmountPerDay = CalculateProduceAmount(animal);

            double income = 0;
            if (animal is Cow cow)
            {
                income = produceAmountPerDay * 9.4;
            }
            else if (animal is Sheep sheep)
            {
                income = produceAmountPerDay * 6.2;
            }

            double cost = animal.Cost; // Cost per day
            double tax = produceAmountPerDay * 0.02; // Tax per day on produce amount

            totalIncome += income;
            totalCost += cost;
            totalTax += tax;
            totalWeight += weight;
            totalProduceAmount += produceAmountPerDay;
        }

        double averageWeight = totalWeight / totalLivestockCount;
        double profit = totalIncome - totalCost - totalTax;
        //double percentage = Helper.CalculatePercentage(totalLivestockCount, vm.Animals.Count);
        // double profit = Helper.CalculateProfit(totalIncome, totalCost, totalTax);
        // Display results
        totalCountLabel.Text = $"{totalLivestockCount}";
        percentageLabel.Text = $"{percentage:F2}%";
        totalTaxLabel.Text = $"${totalTax:F2}";
        profitLabel.Text = $"${profit:F2}";
        averageWeightLabel.Text = $"{averageWeight:F2} kg";
        totalProduceAmountLabel.Text = $"{totalProduceAmount:F2} kg";
        label1.Text = $"Number of livestock ({selectedType} in {selectedColour} colour):";
    }

    public static double CalculateProduceAmount(Animal animal)
    {
        if (animal is Cow cow)
        {
            return cow.Milk;
        }
        else if (animal is Sheep sheep)
        {
            return sheep.Wool;
        }
        return 0;
    }

    private void OnResetClicked(object sender, EventArgs e)
    {
        totalCountLabel.Text = string.Empty;
        percentageLabel.Text = string.Empty;
        totalTaxLabel.Text = string.Empty;
        profitLabel.Text = string.Empty;
        averageWeightLabel.Text = string.Empty;
        totalProduceAmountLabel.Text = string.Empty;
        label1.Text = string.Empty;
    }
}
