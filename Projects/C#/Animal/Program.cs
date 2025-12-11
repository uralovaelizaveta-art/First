class Animal
{
    protected string name;
    protected int age;
    public Animal(){name = "No_name";age = 0;}
    public Animal(string name, int age){this.name = name; this.age = age;}
    public string Name{get{return name;}}
    public int Age{get{return age;}}
    public void Sound(){Console.WriteLine($"Animal {name} said 'Hello!' ");}
}
class Dog : Animal
{
    public Dog(){name = "No_name";age = 0;}
    public Dog(string name, int age){this.name = name; this.age = age;}
    public void Gav(){Console.WriteLine($"Dog {name}: \n-Gav-gav!!! ");}
}
class Cat : Animal
{
    public Cat(){name = "No_name";age = 0;}
    public Cat(string name, int age){this.name = name; this.age = age;}
    public void Mav(){Console.WriteLine($"Cat {name}:\n-Maaaaaav");}
}
class Programm
{
    public static string Name(){
    Console.Write("Name: "); 
    string name = Console.ReadLine(); 
    return name;}
    public static int Age(){
    Console.Write("Age: ");
    int age = Convert.ToInt32(Console.ReadLine()); 
    return age;}
    static void Main()
    {
        Console.WriteLine("Add new animal");
        Animal a1 = new Animal(Name(), Age());
        Console.WriteLine("Add new dog");
        Dog d1 = new Dog(Name(), Age());
        Console.WriteLine("Add new cat");
        Cat c1 = new Cat(Name(), Age());

        a1.Sound();
        d1.Sound();
        d1.Gav();
        c1.Sound();
        c1.Mav();



    }
}