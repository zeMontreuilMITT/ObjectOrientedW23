// declared variable of Animal type with name fido
// not yet instantiated or assigned
Animal fido;

Console.WriteLine("What kind of animal is Fido?");
string animalType = Console.ReadLine();

if(animalType == "dog")
{
    fido = new Dog();
    // instatiate fido as dog at runtime
} else {
    fido = new Cat();
} 



// fido is an instance of its defined class, and counts as an instance of all inherited classes
Console.WriteLine(fido is Animal);
Console.WriteLine(fido is Dog);
Console.WriteLine(fido is Console);

// invoke methods defined on the parent class in the child
fido.SetName("Fido the Dog");
fido.GetName();

fido.Limbs = -1;
Console.WriteLine(fido.Limbs);

List<string> names = new List<string> { "Fido", "Dorothy", "Anyaduba" };
HashSet<string> nameSet = new HashSet<string> { "Bob", "Brody" };

WriteAll(names);
WriteAll(nameSet);

void WriteAll(ICollection<string> collection)
{
    List<string> newList = new List<string>();
    foreach (string name in newList)
    {
        collection.Add(name);
    }
}

// abstract class, cannot be instantiated
abstract class Animal
{
    // members: fields and properties
    // field
    protected string _name;

    // field exposed by public property
    private int _limbs;

    // property that exposes field "_limbs"
    // default get and set methods
    public int Limbs { get
        {
            return _limbs;
        }

        set
        {
            if(value >= 0)
            {
                _limbs = value;
            }
        }
    }

    // method (also a member)
    // [accesor] [return type] [name] ([parameters])
    // SetName exposes setting _name
    public void SetName(string newName)
    {
        if (!String.IsNullOrEmpty(newName))
        {
            _name = newName;
        }
    }

    // method exposes the _name value to get
    public string GetName()
    {
        return _name;
    }
}

// concrete class: can be instantiated
// inherits from abstract class Animal (implements Animal)

abstract class FurryAnimal: Animal
{
    public string FurColour { get; set; }
}
class Dog : FurryAnimal
{
    public void GetTheBall(string colour)
    {
        Console.WriteLine($"{_name} runs to get the {colour} ball.");
    }
}

class Cat : FurryAnimal
{
    public void ScratchPost()
    {
        Console.WriteLine("You wake up at 3:00 AM to the sound of scratching.");
    }
}

