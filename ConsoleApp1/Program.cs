

// abstract class, cannot be instantiated
abstract class Animal
{
    // members: fields and properties
    // field
    private int _limbs;
    private string _name;

    // methods
    public void SayName()
    {
        Console.WriteLine($"Hello, my name is {_name}");
    }
}

