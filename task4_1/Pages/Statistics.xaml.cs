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

        double avgDailyProfit;
        if (selectedType.Equals("Cow", StringComparison.OrdinalIgnoreCase))
        {
            avgDailyProfit = vm.AvgCowDailyProfit();
            double totalDailyProfit = avgDailyProfit * animalAmount;
            ResultLabel.Text = $"Average daily profit per Cow:: ${avgDailyProfit:F2}\n"
               +$"Total daily profit for {animalAmount} Cows:: ${totalDailyProfit:F2}";
        }
        else if (selectedType.Equals("Sheep", StringComparison.OrdinalIgnoreCase))
        {
            avgDailyProfit = vm.AvgSheepDailyProfit();
            double totalDailyProfit = avgDailyProfit * animalAmount;
            ResultLabel.Text = $"Average daily profit per Sheep: ${avgDailyProfit:F2}\n" +
                $"Total daily profit for {animalAmount} Sheep: ${totalDailyProfit:F2}";
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
       
    }

    private void CalulateStats(object sender, EventArgs e)
    {
        double avgCowDailyProfit = vm.AvgCowDailyProfit();
        double avgSheepDailyProfit = vm.AvgSheepDailyProfit();
        double avgDailyTax = vm.CalculateAvgGovernmentTax();

        avgcowdailyprft.Text = $"$ {avgCowDailyProfit:F2}";
        avgsheepdailyprft.Text = $"$ {avgSheepDailyProfit:F2}";
        govttax.Text = $"$ {avgDailyTax * 30:F2}"; // Monthly government tax
        cowdailyprft.Text = $"$ {vm.CalculateTotalCowProfit():F2}";
        sheepdailyprft.Text = $"$ {vm.CalculateTotalSheepProfit():F2}";
        farmdailyprofit.Text = $"$ {vm.CalculateTotalFarmProfit():F2}";
        averageweight.Text = $"{vm.CalculateAverageWeight():F2} kg";
    }


}

    