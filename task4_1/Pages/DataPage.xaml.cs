using task4_1.ViewModels;

namespace task4_1.Pages;

public partial class DataPage : ContentPage
{
	public DataPage(MainViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		AnimalCollView.ItemsSource = vm.Animals;

    }
}