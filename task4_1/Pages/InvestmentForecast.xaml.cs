namespace task4_1.Pages
{
    public partial class InvestmentForecast : ContentPage
    {
        MainViewModel vm;

        public InvestmentForecast(MainViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
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
                    double dailyTax = dailyRevenue * governmentTaxRate; // Altered: Corrected tax calculation
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

            double totalProfit = 0;
            if (selectedType.Equals("Cow", StringComparison.OrdinalIgnoreCase))
            {
                totalProfit = cowProfitPerCow * animalAmount;
                ResultLabel.Text = $"Type of Animal: Cow\nEstimated daily profit: ${totalProfit:F2}";
            }
            else if (selectedType.Equals("Sheep", StringComparison.OrdinalIgnoreCase))
            {
                totalProfit = sheepProfitPerSheep * animalAmount; // Altered: Use single sheep profit
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
        }

        private void OnTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            // This method is intentionally left blank, can be used for future event handling
        }
    }
}
