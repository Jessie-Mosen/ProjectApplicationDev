namespace task4_1.Pages;

public partial class UpdateAnimal : ContentPage
{
    MainViewModel vm;
    private Animal animal; // Declare the animal variable at the class level

    public UpdateAnimal(MainViewModel vm)
    {
        InitializeComponent();
        this.vm = vm;
        BindingContext = vm;
    }

    private void OnResetClicked(object sender, EventArgs e)
    {
        IdEntry.Text = string.Empty;
        ResultLabel.Text = string.Empty;
        weightEntry.Text = string.Empty;
        costEntry.Text = string.Empty;
        milkEntry.Text = string.Empty;
        woolEntry.Text = string.Empty;
        colourPicker.SelectedIndex = -1;
        weightEntry.IsVisible = false;
        costEntry.IsVisible = false;
        milkEntry.IsVisible = false;
        woolEntry.IsVisible = false;
        milkLabel.IsVisible = false;
        woolLabel.IsVisible = false;
        colourPicker.IsVisible = false;
        UpdateBtn.IsVisible = false;
    }

    private void IdEntryBtn_click(object sender, EventArgs e)
    {
        if (!int.TryParse(IdEntry.Text, out int animalId))
        {
            DisplayAlert("Error", "Invalid input", "OK");
            return;
        }

        animal = vm.Animals.FirstOrDefault(a => a.Id == animalId);

        if (animal != null)
        {
            colourPicker.IsVisible = true;
            weightEntry.IsVisible = true;
            costEntry.IsVisible = true;
            weightEntry.Text = animal.Weight.ToString();
            costEntry.Text = animal.Cost.ToString();
            colourPicker.SelectedItem = animal.Colour;

            if (animal is Cow cow)
            {
                milkLabel.IsVisible = true;
                milkEntry.IsVisible = true;
                milkEntry.Text = cow.Milk.ToString();
                woolLabel.IsVisible = false;
                woolEntry.IsVisible = false;
            }
            else if (animal is Sheep sheep)
            {
                woolLabel.IsVisible = true;
                woolEntry.IsVisible = true;
                woolEntry.Text = sheep.Wool.ToString();
                milkLabel.IsVisible = false;
                milkEntry.IsVisible = false;
            }

            ResultLabel.Text = "Animal found. Modify the details below.";
            UpdateBtn.IsVisible = true;
        }
        else
        {
            ResultLabel.Text = "Animal not found";
        }
    }

    private void UpdateBtn_Click(object sender, EventArgs e)
    {
        if (animal == null) return;

        if (!double.TryParse(weightEntry.Text, out double weight) ||
            !double.TryParse(costEntry.Text, out double cost))
        {
            DisplayAlert("Error", "Weight and cost must be valid numbers", "OK");
            return;
        }

        animal.Weight = weight;
        animal.Cost = cost;
        animal.Colour = colourPicker.SelectedItem.ToString();

        if (animal is Cow cow)
        {
            if (!double.TryParse(milkEntry.Text, out double milkAmount))
            {
                DisplayAlert("Error", "Milk amount must be a valid number", "OK");
                return;
            }
            cow.Milk = milkAmount;
        }
        else if (animal is Sheep sheep)
        {
            if (!double.TryParse(woolEntry.Text, out double woolAmount))
            {
                DisplayAlert("Error", "Wool amount must be a valid number", "OK");
                return;
            }
            sheep.Wool = woolAmount;
        }

        // Update the animal in the database
        vm._database.UpdateItem(animal);

        // Reflect the changes in the UI
        int index = vm.Animals.IndexOf(vm.Animals.First(a => a.Id == animal.Id));
        if (index != -1)
        {
            vm.Animals[index] = animal;
        }

        DisplayAlert("Success", "Animal updated successfully", "OK");
    }
}
