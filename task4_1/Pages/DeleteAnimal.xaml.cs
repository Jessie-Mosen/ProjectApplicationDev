namespace task4_1.Pages;

public partial class DeleteAnimal : ContentPage
{
    MainViewModel vm;
    
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
        if (!int.TryParse(IdEntry.Text, out int animalId))
        {
            DisplayAlert("Error", "Invalid input", "OK");
            return;
        }

        Animal animal = vm.Animals.FirstOrDefault(a => a.Id == animalId);

        if (animal != null)
        {
            if (animal is Cow cow)
            {
                ResultLabel.Text =
                    $"Type of Animal:       Cow\n" +
                    $"Colour:       {cow.Colour}\n" +
                    $"Cost:     {cow.Cost}\n" +
                    $"Weight        {cow.Weight}\n" +
                    $"Milk:     {cow.Milk}\n";
                    
            }
            else if (animal is Sheep sheep)
            {
                ResultLabel.Text =
                    $"Type of Animal:Sheep\n" +
                    $"Colour:{sheep.Colour}\n" +
                    $"Cost:{sheep.Cost}\n" +
                    $"Weight:{sheep.Weight}\n" +
                    $"Wool:{sheep.Wool}\n";
            }
        }
        else
        {
            // If the animal with the provided ID is not found
            ResultLabel.Text = "Animal not found";
        }
    }

    private void DeleteBtn_Click(object sender, EventArgs e)
    {
       
        if (!int.TryParse(IdEntry.Text, out int animalId))
        {
            DisplayAlert("Error", "Invalid input", "OK");
            return;
        }

        Animal animal = vm.Animals.FirstOrDefault(a => a.Id == animalId);
        if(animal == null)
        {
            DisplayAlert("error", "no animal found", "OK");
        }
        if (animal != null)
        {
            vm._database.DeleteItem(animal);
            vm.Animals.Remove(animal);
            DisplayAlert("congrats", "Successful Deletion", "OK");
        }
    }
}
