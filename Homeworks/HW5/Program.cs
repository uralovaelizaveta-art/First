interface IDamageable{
    int TakeDamage(int damage);}
abstract class Character:IDamageable
{
    string name;
    int health;
    public Character(string n, int h){
        name = n;
        health = h;
    }
    public Character(){
        name = "Noname";
        health = 100;
    }

    public abstract void Attack();
    public void Mode(){Console.WriteLine("Top-top-top");}
    // этот класс должен быть абстрактным, чтобы использовать его как шаблон для наследников, у кого какие характистики должны быть

    public int TakeDamage(int damage)
    {
        Console.WriteLine("Ahh :(");
        health -= damage;
        return health;
    }
}
class Warrior : Character{
    public override void Attack(){
        Console.WriteLine("I will kill you!");}}
class Mage : Character{
    public override void Attack(){
        Console.WriteLine("It's magic");}}
class Elf : Character{
    public override void Attack(){
        Console.WriteLine("* Glitter * attacck! *");}}
class Programm
{
    static void Main(){
    Character [] mas = [new Warrior(), new Mage(), new Elf()];
    foreach (Character i in mas){
        i.Attack();}
    }
    //Потому что мы переопределяем метод с помощью бинамического полиморфизма
}


