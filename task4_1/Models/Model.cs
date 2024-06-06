namespace task4_1.Models;

public class Animal
{
    [PrimaryKey ,AutoIncrement]
    public int Id { get; set; }
    public string Colour { get; set; }

    public double Cost { get; set; }

    public double Weight { get; set; }

    public override string ToString()
    {
        //this.gettype() will return whether its a manager or a salesperson
        //this.name this will return the actual name of the person
        return $"{GetType().Name,-15} {Id,-5}{Colour,-20}{Cost,-5} {Weight,-5}";

    }
}
public class Cow : Animal
{
    public double Milk { get; set; }

    public override string ToString()
    {
        return base.ToString() + $"{Milk}";
    }

}

public class Sheep : Animal 
{
    public double Wool { get; set; }

    public override string ToString()
    {
        return base.ToString() + $"{Wool}";
    }
}