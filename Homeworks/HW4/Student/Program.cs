using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;

class Student
{
    protected string name;
    protected int age;
    protected int group;

    public Student()
    {
        age = 0;
        name = "No_name";
        group = 0;
    }
    public Student (string n, int a, int g)
    {
        name = n;
        age = a;
        group = g;
    }
    public string Name{get{return name;}}
    public int Age{get{return age;}}
    public int Group{get{return group;}}
    public void Study()
    {
        Console.WriteLine($"Student {name}, who is {age} years old, studies in group №{group}");
    }
}
class Master : Student
{
    protected string diploma_progress;
    public string Diploma_progress{get{return diploma_progress;}}
    public Master()
    {
        diploma_progress = "No diploma";
        age = 0;
        name = "No_name";
        group = 0;
    }
    public Master (string pr, Student s1)
    {
        diploma_progress = pr;
        age = s1.Age;
        name = s1.Name;
        group = s1.Group;
    }

    public void Defend_diploma()
    {
        Console.WriteLine($"Student {name} ({age} years old, group №{group}) has diploma at the stage: {diploma_progress}");
    }
}
class Bachelor : Student
{
    protected bool was_exem;
    public bool Was_exem{get{return was_exem;}}
    public Bachelor()
    {
        age = 0;
        name = "No_name";
        group = 0;
        was_exem = false;
    }
    public Bachelor ( bool we, Student s1)
    {
        was_exem = we;
        age = s1.Age;
        name = s1.Name;
        group = s1.Group;
    }
    public void Pass_exem()
    {
        if (was_exem) {Console.WriteLine($"Student {name} in group №{group} passed exem!");} 
        else {Console.WriteLine($"Student {name} in group №{group} didn't pass exem!");}
    }
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
    public static int Group(){
    Console.Write("Group: ");
    int group = Convert.ToInt32(Console.ReadLine()); 
    return group;}
    public static string Diploma_progress(){
    Console.Write("Diploma progress: ");
    string dp = Console.ReadLine(); 
    return dp;}
    public static bool Was_exem(){
    Console.Write("Did Student pass exem: ");
    bool we = Convert.ToBoolean(Console.ReadLine()); 
    return we;}
    static void Main()
    {
        Console.WriteLine("Add new student");
        Student s1 = new Student(Name(), Age(), Group());
        Console.Write("This Student is Bachelor(1) or Master(2)? ");
        int a = Convert.ToInt32(Console.ReadLine());
        if (a == 2) {
            Master m1 = new Master(Diploma_progress(), s1);
            m1.Study();
            m1.Defend_diploma();}
        else {
            Bachelor b1 = new Bachelor(Was_exem(), s1);
            b1.Study();
            b1.Pass_exem();
        }

        Console.WriteLine("Add new student");
        Student s2 = new Student(Name(), Age(), Group());
        Console.Write("This Student is Bachelor(1) or Master(2)?");
        a = Convert.ToInt32(Console.ReadLine());
        if (a == 2) {
            Master m2 = new Master(Diploma_progress(), s2);
            m2.Study();
            m2.Defend_diploma();}
        else {
            Bachelor b2 = new Bachelor(Was_exem(), s2);
            b2.Study();
            b2.Pass_exem();}
    }
}