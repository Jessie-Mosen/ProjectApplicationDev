namespace task4_1.Pages;

public partial class DeleteAnimal : ContentPage
{
    private readonly MainViewModel vm;

    public DeleteAnimal(MainViewModel vm)
    {
        InitializeComponent();
        this.vm = vm;
        BindingContext = vm;
    }

    private void OnResetClicked(object sender, EventArgs e)
    {
        IdEntry.Text = string.Empty;
        ResultLabel.Text = string.Empty;
    }

    private void IdEntryBtn_click(object sender, EventArgs e)
    {
        if (!TryGetAnimalById(out Animal animal))
        {
            ResultLabel.Text = "Animal not found";
            return;
        }

        ResultLabel.Text = animal is Cow cow
            ? $"Type of Animal: Cow\nColour: {cow.Colour}\nCost: {cow.Cost}\nWeight: {cow.Weight}\nMilk: {cow.Milk}\n"
            : $"Type of Animal: Sheep\nColour: {animal.Colour}\nCost: {animal.Cost}\nWeight: {animal.Weight}\nWool: {((Sheep)animal).Wool}\n";
    }

    private void DeleteBtn_Click(object sender, EventArgs e)
    {
        if (!TryGetAnimalById(out Animal animal))
        {
            DisplayAlert("Error", "No animal found", "OK");
            return;
        }

        vm._database.DeleteItem(animal);
        vm.Animals.Remove(animal);
        DisplayAlert("Success", "Animal successfully deleted", "OK");
    }

    private bool TryGetAnimalById(out Animal animal)
    {
        animal = null;
        if (!int.TryParse(IdEntry.Text, out int animalId))
        {
            DisplayAlert("Error", "Invalid input", "OK");
            return false;
        }

        animal = vm.Animals.FirstOrDefault(a => a.Id == animalId);
        return animal != null;
    }
}
