interface ICommand //команды
{
    public abstract void PrintCommand();


}
interface IInteractable //взаимодействие
{
    public abstract void Iteractable();
}
interface ICondition //условия
{
    public bool And(bool a, bool b)
    {
        if (a == false | (b == false)){
            return false;}
        else {return true;}
        
    }
}
interface IEffect //эффекты
{
    public abstract void Effect();
}
interface IGamestate // состояние игры
{
    public abstract void Gamestate();
}

class GameState
{
public static Hall loc_hall; 
public static Storage loc_storage;
public static DarkCorridor loc_dark_corridor;
public static GeneratorRoom loc_generator_room; 
public static Labyrinth loc_labyrinth;
public static Roof loc_roof;
public static TrapRoom loc_trap_room;
public static Door it_door;
public static List <CommandBase> commands;
public static List <Interactable> inventory; 

public GameState()
    {
        loc_hall = new Hall();
        loc_dark_corridor = new DarkCorridor();
        loc_generator_room = new GeneratorRoom();
        loc_labyrinth = new Labyrinth();
        loc_roof = new Roof();
        loc_storage = new Storage();
        loc_trap_room = new TrapRoom();
        inventory = [];
    }
    public static Hall Loc_Hall {
        get{return loc_hall;}
    }
    public static DarkCorridor Loc_Dark_corridor {
        get{return loc_dark_corridor;}
    }
    public static GeneratorRoom Loc_Generator_room {
        get{return loc_generator_room;}
    }
    public static Labyrinth Loc_Labyrinth {
        get{return loc_labyrinth;}
    }
    public static Roof  Loc_Roof {
        get{return loc_roof;}
    }
    public static Storage Loc_Storage {
        get{return loc_storage;}
    }
}
abstract class CommandBase
{
    public string name;
    public string terminal_name;
    public CommandBase()
    {
        name = "no_name";
        terminal_name = "no_name";
    }
    public CommandBase(string command_name, string command_terminal_name)
    {
        name = command_name;
        terminal_name = command_terminal_name;
    }
    public string Name {
        get{return name;}
    }
    public string Terminal_name {
        get{return terminal_name;}
    }
    
}
class LookCommand : CommandBase, ICommand
{
    public void PrintCommand()
    {
        Console.WriteLine("You can see ...");
    }

}
class GoCommand : CommandBase, ICommand
{
    public void PrintCommand()
    {
        Console.WriteLine("You went to ...");
    }
}
class InteractCommand : CommandBase, ICommand
{
    public void PrintCommand()
    {
        Console.WriteLine("You find ...");
    }
    
}
class InventoryCommand : CommandBase, ICommand
{
    public void PrintCommand()
    {
        Console.WriteLine("You have: ...");
    }
    
}
class HelpCommand : CommandBase, ICommand
{
    public void PrintCommand()
    {
        Console.WriteLine("Help — list of commands, \nlook — description of the current location, \ngo — transition to another location, \ninteract — interaction with an object, \ninv — view inventory, \nstatus — information about the player's status.");
    }  
}
class StatusCommand : CommandBase, ICommand
{
    public void PrintCommand()
    {
        Console.WriteLine("");
    }  
}

abstract class ConditionBase
{
    public bool item; //наличие предмета
    public bool flag; // занчение флага
    public int health; // уровень здоровья
    public ConditionBase()
    {
        item = false;
        flag = false;
        health = 100;
    }
    public ConditionBase(bool item1, bool flag1, int health1)
    {
        item = item1;
        flag = flag1;
        health = health1;
    }
    public bool Item {
        get{return item;}
        set{item = value;}
    }
    public bool Flag {
        get{return flag;}
        set{flag = value;}
    }
    public int Health {
        get{return health;}
        set{health += value;}
    }
}
class HasItemCondition : ConditionBase
{

}
class FlagCondition : ConditionBase
{
    
}
class HealthCondition : ConditionBase
{
    
}
class AndCondition : ConditionBase
{
    
}
class OrCondition : ConditionBase
{
    
}
class NotCondition : ConditionBase
{
    
}

abstract class EffectBase
{
    public List <ConditionBase> itmes; //объекты класса ConditionBase
    public List <ConditionBase> flags; //объекты класса ConditionBase
    public List <ConditionBase> healths; //объекты класса ConditionBase
    public List <string> list; // журнал
    public List <LocationBase> locations;
    public EffectBase()
    {
        itmes = [];
        flags = [];
        healths = [];
        list = [];
        locations = [new Hall()];
    }
    // public EffectBase(как тут лучше обыграть со списком элементов, и как его сохранить)
    // {
    //     itmes = [];
    //     flags = [];
    //     healths = [];
    //     list = [];
    //     locations = [];
    // }
    public List <ConditionBase> Itmes{
        get{return itmes;}
        set{}
    }
    public List <ConditionBase> Flags{get{return flags;}}
    public List <ConditionBase> Healths{get{return healths;}}
    public List <string> List{get{return list;}}
    public List <LocationBase> Locations{get{return locations;}}
}
class AddItemEffect : EffectBase
{
    
}
class RemoveItemEffect : EffectBase
{
    
}
class SetFlagEffect : EffectBase
{
    
}
class DamageEffect : EffectBase
{
    
}
class HealEffect : EffectBase
{
    
}
class LogEffect : EffectBase
{
    
}
class AddExitEffect : EffectBase
{
    
}
class ChangeLocationEffect : EffectBase
{
    
}

abstract class LocationBase
{
    public List<LocationBase> exits; 
    public List<Interactable> items_in_location; 
    public string description; 
    public string location_name; 
    public List <GameEventBase> events; 
    public LocationBase()
    {
        exits = [];
        items_in_location = [];
        description = "Noting";
        location_name = "Noname";
        events = [];
    }
    public LocationBase(string descr, string nam)
    {
        exits = [];
        items_in_location = [];
        description = descr;
        location_name = nam;
        events = [];
    }
    public List<LocationBase> Exits{get{return exits;}}
    public List<Interactable> Items_in_location{get{return items_in_location;}}
    public string Description{get{return description;}}
    public string Location_name{get{return location_name;}}
    public List <GameEventBase> Events{get{return events;}}
}
class Hall : LocationBase
{
    public Hall()
    {
        exits = [GameState.loc_dark_corridor, GameState.loc_labyrinth, GameState.loc_roof];
        items_in_location = [new First_pieces()];
        description = "Hall. This is the starting point of the game. There are 4 exits from here: to the maze, to the roof, to the dark corridor, and out of the game. To exit the game, you need to find 4 pieces of the password and enter them into the tablet next to the game exit.";
        location_name = "Hall";
        events = [];
    }
}
class Storage : LocationBase
{
    public Storage()
    {
        exits = [GameState.loc_labyrinth];
        items_in_location = [new Key(), new Third_pieces(), new Bomb(), new Ladder()];
        description = "";
        location_name = "Storage";
        events = [];
    }
}
class DarkCorridor : LocationBase
{
    public DarkCorridor()
    {
        exits = [GameState.loc_trap_room, GameState.loc_generator_room];
        items_in_location = [new Fourth_pieces()];
        description = "Noting";
        location_name = "Dark corridor";
        events = [];
    }
}
class GeneratorRoom : LocationBase
{
    public GeneratorRoom()
    {
        exits = [GameState.loc_dark_corridor];
        items_in_location = [];
        description = "Noting";
        location_name = "Generator room";
        events = [];
    }
}
class Roof : LocationBase
{
    public Roof()
    {
        exits = [GameState.loc_hall];
        items_in_location = [new Fuse()];
        description = "Noting";
        location_name = "Roof";
        events = [];
    }
}
class Labyrinth : LocationBase
{
    public Labyrinth()
    {
        exits = [GameState.loc_hall, GameState.loc_storage];
        items_in_location = [new Wrench(), new Second_pieces()];
        description = "Noting";
        location_name = "Labyrinth";
        events = [];
    }
}
class TrapRoom : LocationBase
{
    public TrapRoom()
    {
        exits = [GameState.loc_dark_corridor];
        items_in_location = [];
        description = "Noting";
        location_name = "Trap";
        events = [];
    }
}

abstract class GameEventBase
{
    public List <ConditionBase> condition; //условия
    public List <EffectBase> effects;//объекты класса Effects
    public bool one_time; //признак одноразового события
    public GameEventBase()
    {
        condition = [];
        effects = [];
        one_time = false;
    }
    public GameEventBase(bool time_1)
    {
        condition = [];
        effects = [];
        one_time = time_1;
    }
    public List <ConditionBase> Condition{get{return condition;}}
    public List <EffectBase> Effects{get{return effects;}}
    public bool One_time{get{return one_time;}}
}
class OnEnterLocationEvent : GameEventBase
{
    public OnEnterLocationEvent()
    {
        condition = [];
        effects = [];
        one_time = false;
    }
}
class OnTurnEvent : GameEventBase
{
    public OnTurnEvent()
    {
        condition = [];
        effects = [];
        one_time = false;
    }
}
class OneTimeEvent : GameEventBase
{
    public OneTimeEvent()
    {
        condition = [];
        effects = [];
        one_time = false;
    }
}

abstract class Interactable
{
    public string item_name;
    public string item_id;

}
// class Chest : Interactable, IInteractable
// {
//     public void Iteractable()
//     {
//         Console.WriteLine("You open the chest and find ...");
//     }
//     public Chest()
//     {
//         item_name = "Chest";
//         item_id = "chest_1";
//     }
// }
class Door : Interactable, IInteractable
{
    public void Iteractable()
    {
        Console.WriteLine("You open the door and go to the next location:");
    }
    public Door()
    {
        item_name = "Door";
        item_id = "door_1";
    }
}
class NPC : Interactable, IInteractable
{
    public void Iteractable()
    {
        Console.WriteLine($"");
    }
    public NPC()
    {
        item_name = "NPC";
        item_id = "npc_1";
    }
}
class Trap : Interactable, IInteractable
{
    public void Iteractable()
    {
        Console.WriteLine($"You triggered a trap and lost some health.");
    }
    public Trap()
    {
        item_name = "Trap";
        item_id = "trap_1";
    }
}
class Key : Interactable, IInteractable
{
    public void Iteractable()
    {
        Console.WriteLine($"You picked up a key. Use it to open the locked door.");
    }
    public Key()
    {
        item_name = "Key";
        item_id = "key_1";
    }
}
class Torch : Interactable, IInteractable
{
    public void Iteractable()
    {
        Console.WriteLine($"You picked up a torch. Use it to light up dark places.");
    }
    public Torch()
    {
        item_name = "Torch";
        item_id = "torch_1";
    }
}
class Fuse : Interactable, IInteractable
{
    public void Iteractable()
    {
        Console.WriteLine($"You picked up a fuse. Use it to fire the bomb.");
    }
    public Fuse()
    {
        item_name = "Fuse";
        item_id = "fuse_1";
    }
}
class Wrench : Interactable, IInteractable
{
    public void Iteractable()
    {
        Console.WriteLine($"You picked up a wrench. Use it to fix the generator.");
    }
    public Wrench()
    {
        item_name = "Wrench";
        item_id = "wrench_1";
    }
}
class Bomb : Interactable, IInteractable
{
    public void Iteractable()
    {
        Console.WriteLine($"You picked up a bomb. Use it to blow up the door.");
    }
    public Bomb()
    {
        item_name = "Bomb";
        item_id = "bomb_1";
    }
}
class Ladder : Interactable, IInteractable
{
    public void Iteractable()
    {
        Console.WriteLine($"You picked up a ladder. Use it to climb to the roof.");
    }
    public Ladder()
    {
        item_name = "Ladder";
        item_id = "ladder_1";
    }
}
class Password_pieces : Interactable, IInteractable
{
    public int pieces_id;
    public int number_of_pieces;
    public void Iteractable()
    {
        Console.WriteLine($"You picked up a piece of the password.");
    }
}
class First_pieces : Password_pieces
{
    public First_pieces()
    {
        item_name = "First piece of password";
        item_id = "pieces_1";
        pieces_id = 1;
    }
}
class Second_pieces : Password_pieces
{
    public Second_pieces()
    {
        item_name = "Second piece of password";
        item_id = "pieces_2";
        pieces_id = 2;
    }
}
class Third_pieces : Password_pieces
{
    public Third_pieces()
    {
        item_name = "Third piece of password";
        item_id = "pieces_3";
        pieces_id = 3;
    }
}
class Fourth_pieces : Password_pieces
{
    public Fourth_pieces()
    {
        item_name = "Fourth piece of password";
        item_id = "pieces_4";
        pieces_id = 4;
    }   
}


static class Game
{
    public static void Main()
    {
        GameState gamestate = new GameState();
        Console.WriteLine("Welcome to the game! Type 'help' to see the list of commands.");
    }
}