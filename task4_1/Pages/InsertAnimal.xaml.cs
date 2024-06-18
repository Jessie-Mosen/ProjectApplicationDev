namespace task4_1.Pages;

public partial class InsertAnimal : ContentPage
{
    public MainViewModel vm;
    private Animal animal; 
    public InsertAnimal(MainViewModel vm)
    {
        InitializeComponent();
        this.vm = vm;
        BindingContext = vm;
    }

    private void OnTypeSelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedType = typePicker.SelectedItem.ToString();
        colourPicker.SelectedIndex = 0;

        if (selectedType == "Cow")
        {
            typePicker.SelectedIndex = 0;
            colourPicker.IsEnabled = true;
            milkEntry.IsVisible = true;
            milkLabel.IsVisible = true;
            woolEntry.IsVisible = false;
            woolLabel.IsVisible = false;
            animal = new Cow(); 
        }
        else if (selectedType == "Sheep")
        {
            typePicker.SelectedIndex = 1;
            colourPicker.IsEnabled = true;
            colourPicker.SelectedIndex = 0;
            woolEntry.IsVisible = true;
            woolLabel.IsVisible = true;
            milkEntry.IsVisible = false;
            milkLabel.IsVisible = false;
            animal = new Sheep(); 
        }
    }

    private void OnInsertAnimalClicked(object sender, EventArgs e)
    {
        double milkAmount = 0;
        double woolAmount = 0;

        if (!double.TryParse(costEntry.Text, out double cost))
        {
            DisplayAlert("Error", "Cost must be a valid number", "OK");
            return;
        }

        if (!double.TryParse(weightEntry.Text, out double weight))
        {
            DisplayAlert("Error", "Weight must be a valid number", "OK");
            return;
        }

        if (milkEntry.IsVisible && !double.TryParse(milkEntry.Text, out milkAmount))
        {
            DisplayAlert("Error", "Milk amount must be a valid number", "OK");
            return;
        }

        if (woolEntry.IsVisible && !double.TryParse(woolEntry.Text, out woolAmount))
        {
            DisplayAlert("Error", "Wool amount must be a valid number", "OK");
            return;
        }

        // Set common properties
        animal.Colour = colourPicker.SelectedItem.ToString();
        animal.Cost = cost;
        animal.Weight = weight;

        // Set specific properties based on type
        if (animal is Cow)
        {
            ((Cow)animal).Milk = milkAmount;
        }
        else if (animal is Sheep)
        {
            ((Sheep)animal).Wool = woolAmount;
        }

        // Insert the animal into the list and database
        vm.AddItem(animal);
        vm._database.InsertItem(animal);
        DisplayAlert("Congratulations", "Animal successfully inserted", "OK");

    }

    private void OnResetClicked(object sender, EventArgs e)
    {
        // Clear all entries
        weightEntry.Text = string.Empty;
        costEntry.Text = string.Empty;
        milkEntry.Text = string.Empty;
        woolEntry.Text = string.Empty;

        // Reset pickers
        typePicker.SelectedIndex = 0;
        colourPicker.SelectedIndex = 0;

        // Hide milk and wool entries and labels
        //milkEntry.IsVisible = false;
        //milkLabel.IsVisible = false;
        //woolEntry.IsVisible = false;
        //woolLabel.IsVisible = false;
    }
}
