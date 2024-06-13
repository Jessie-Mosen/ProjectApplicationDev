namespace task4_1.Pages
{
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
            // Constants for prices and tax rate
            double cowMilkPrice = 9.4; // $ per kg
            double sheepWoolPrice = 6.2; // $ per kg
            double governmentTaxRate = 0.02; // Government tax rate per kg per day

            double cowProfitPerCow = 0;
            double sheepProfitPerSheep = 0;

            // Calculate profit per animal type
            foreach (var animal in vm.Animals)
            {
                if (animal is Cow cow)
                {
                    double dailyRevenue = cow.Milk * cowMilkPrice;
                    double dailyTax = dailyRevenue * governmentTaxRate;
                    cowProfitPerCow = dailyRevenue - cow.Cost - dailyTax;
                }
                else if (animal is Sheep sheep)
                {
                    double dailyRevenue = sheep.Wool * sheepWoolPrice;
                    double dailyTax = dailyRevenue * governmentTaxRate;
                    sheepProfitPerSheep = dailyRevenue - sheep.Cost - dailyTax;
                }
            }

            // Validate user input
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

            // Calculate total profit based on selected animal type
            double totalProfit = 0;
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
            // Clear input fields and result labels
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

        private void CalulateStats(object sender, EventArgs e)
        {
            // Calculate total weight and daily government tax
            double totalWeight = vm.Animals.Sum(animal => animal.Weight);
            double GovernmentTaxDaily = vm.governmentTaxRate * totalWeight;
            double avgweight = totalWeight / vm.Animals.Count;

            // Count number of Sheep and Cow
            int countSheep = vm.Animals.Count(animal => animal is Sheep);
            int countCow = vm.Animals.Count(animal => animal is Cow);

            // Calculate total farm income and daily profit
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

            // Update UI with calculated values
            farmdailyprofit.Text = $"$ {FarmDailyProfit:F2}";
            govttax.Text = $"$ {GovernmentTaxDaily * 30:F2}";
            averageweight.Text = $"{avgweight:F2} kg";

            // Calculate and display daily profit per Cow and Sheep
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

        private void OnTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            // This method is intentionally left blank, can be used for future event handling
        }
    }
}
