using System;
using System.Collections.Generic;
using System.Linq;

interface ICommand //команды
{
    public abstract void PrintCommand();


}
// interface IInteractable //взаимодействие
// {
//     public abstract void Iteractable();
// }
// interface ICondition //условия
// {
//     public bool And(bool a, bool b)
//     {
//         if (a == false | (b == false)){
//             return false;}
//         else {return true;}
        
//     }
//     public bool Or(bool a, bool b)
//     {
//         if (a == true | (b == true)){
//             return true;}
//         else {return false;}
        
//     }
//     public bool Not(bool a)
//     {
//         if (a == true){
//             return false;}
//         else {return true;}
        
//     }

// }
interface IEffect //эффекты
{
    public abstract void Effect();
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
public static Key it_key;
public static Torch it_torch;
public static Fuse it_fuse;
public static Wrench it_wrench;
public static Bomb it_bomb;
public static Ladder it_ladder;
public static First_pieces it_first_pieces;
public static Second_pieces it_second_pieces;
public static Third_pieces it_third_pieces;
public static Fourth_pieces it_fourth_pieces;
public static List <CommandBase> commands;
public static List <Interactable> inventory; 
public static LocationBase current_location;
public static int health;
public static List <string> journal;
public static void Log(string entry)
{
    if (journal == null)
        journal = new List<string>();
    journal.Add(entry);
}
// public static 

public GameState()
    {
        it_bomb = new Bomb();
        it_door = new Door();
        it_fuse = new Fuse();
        it_key = new Key();
        it_ladder = new Ladder();
        it_first_pieces = new First_pieces();
        it_second_pieces = new Second_pieces();
        it_third_pieces = new Third_pieces();
        it_fourth_pieces = new Fourth_pieces();
        it_torch = new Torch();
        it_wrench = new Wrench();

        loc_hall = new Hall();
        loc_dark_corridor = new DarkCorridor();
        loc_generator_room = new GeneratorRoom();
        loc_labyrinth = new Labyrinth();
        loc_roof = new Roof();
        loc_storage = new Storage();
        loc_trap_room = new TrapRoom();

        loc_hall.Exits.Add(loc_dark_corridor);
        loc_hall.Exits.Add(loc_labyrinth);
        loc_hall.Exits.Add(loc_roof);
        loc_hall.Items_in_location.Add(it_first_pieces);
        loc_hall.Items_in_location.Add(it_door);

        loc_dark_corridor.Exits.Add(loc_trap_room);
        loc_dark_corridor.Exits.Add(loc_generator_room);
        loc_dark_corridor.Exits.Add(loc_hall);
        loc_dark_corridor.Items_in_location.Add(it_fourth_pieces);

        loc_generator_room.Exits.Add(loc_dark_corridor);

        loc_roof.Exits.Add(loc_hall);
        loc_roof.Items_in_location.Add(it_fuse);
        loc_roof.Items_in_location.Add(it_torch);

        loc_labyrinth.Exits.Add(loc_hall);
        loc_labyrinth.Exits.Add(loc_storage);
        loc_labyrinth.Items_in_location.Add(it_wrench);
        loc_labyrinth.Items_in_location.Add(it_second_pieces);

        loc_storage.Exits.Add(loc_labyrinth);
        loc_storage.Items_in_location.Add(it_key);
        loc_storage.Items_in_location.Add(it_third_pieces);
        loc_storage.Items_in_location.Add(it_bomb);
        loc_storage.Items_in_location.Add(it_ladder);

        loc_trap_room.Exits.Add(loc_dark_corridor);

        inventory = new List<Interactable>();
        current_location = loc_hall;
        health = 100;
        commands = new List<CommandBase>()
        {
            new LookCommand(),
            new GoCommand(),
            new InteractCommand(),
            new InventoryCommand(),
            new HelpCommand(),
            new StatusCommand()
        };
        journal = new List<string>();

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
    public static TrapRoom Loc_Trap_room {
        get{return loc_trap_room;}
    }
    public static Door It_Door {
        get{return it_door;}
    }
    public static Key It_Key {
        get{return it_key;}
    }
    public static Torch It_Torch {
        get{return it_torch;}
    }
    public static Fuse It_Fuse {
        get{return it_fuse;}
    }
    public static Wrench It_Wrench {
        get{return it_wrench;}
    }
    public static Bomb It_Bomb {
        get{return it_bomb;}
    }
    public static Ladder It_Ladder {
        get{return it_ladder;}
    }
    public static First_pieces It_First_pieces {
        get{return it_first_pieces;}
    }
    public static Second_pieces It_Second_pieces {
        get{return it_second_pieces;}
    }
    public static Third_pieces It_Third_pieces {
        get{return it_third_pieces;}
    }
    public static Fourth_pieces It_Fourth_pieces {
        get{return it_fourth_pieces;}
    }
    public static LocationBase Current_location {
        get{return current_location;}
        set{current_location = value;}
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
    public abstract void PrintCommand();
    
}
class LookCommand : CommandBase, ICommand
{
    public override void PrintCommand()
    {
        if (GameState.current_location == null)
        {
            Console.WriteLine("Текущая локация не установлена.");
            return;
        }

        Console.WriteLine(GameState.current_location.Description);
        var visibleItems = GameState.current_location.Items_in_location?
            .Where(item => item != null && !item.is_in_inventory)
            .ToList();

        if (visibleItems != null && visibleItems.Count > 0)
        {
            Console.WriteLine("Предметы в локации:");
            Console.WriteLine(string.Join(", ", visibleItems.Select(item => item.item_name)));
        }
        else
        {
            Console.WriteLine("В этой локации нет предметов.");
        }
    }
    public LookCommand()
    {
        name = "Осмотреться";
        terminal_name = "осмотреться";
    }
}
class GoCommand : CommandBase, ICommand
{
    private bool HasItem(string itemId)
    {
        return GameState.inventory != null && GameState.inventory
            .Any(item => item != null && item.item_id.Equals(itemId, StringComparison.OrdinalIgnoreCase));
    }

    private bool CanEnter(LocationBase from, LocationBase target, out string failMessage)
    {
        failMessage = null;
        if (from is Hall && target is Roof && !HasItem("ladder_1"))
        {
            failMessage = "Чтобы попасть на крышу, вам нужна лестница.";
            return false;
        }

        if (from is Hall && target is DarkCorridor && !HasItem("key_1"))
        {
            failMessage = "Чтобы войти в тёмный коридор, вам нужен ключ.";
            return false;
        }

        if (from is TrapRoom && target is DarkCorridor)
        {
            if (!HasItem("bomb_1") && !HasItem("fuse_1"))
            {
                failMessage = "Выйти из ловушки нельзя без бомбы и фитиля.";
                return false;
            }
            if (!HasItem("bomb_1"))
            {
                failMessage = "Выйти из ловушки нельзя без бомбы.";
                return false;
            }
            if (!HasItem("fuse_1"))
            {
                failMessage = "Выйти из ловушки нельзя без фитиля.";
                return false;
            }
        }

        return true;
    }

    public override void PrintCommand()
    {
        if (GameState.current_location == null)
        {
            Console.WriteLine("Текущая локация не установлена.");
            return;
        }

        if (GameState.current_location.Exits == null || GameState.current_location.Exits.Count == 0)
        {
            Console.WriteLine("Из этой локации нет выходов.");
            return;
        }

        Console.WriteLine("Куда хотите пойти?");
        foreach (var exit in GameState.current_location.Exits)
        {
            if (exit != null)
                Console.WriteLine($" - {exit.Location_name}");
        }

        string answ = Console.ReadLine()?.Trim();
        if (string.IsNullOrEmpty(answ))
        {
            Console.WriteLine("Вы не ввели название локации.");
            return;
        }

        var target = GameState.current_location.Exits
            .FirstOrDefault(exit => exit != null && exit.Location_name.Equals(answ, StringComparison.OrdinalIgnoreCase));

        if (target == null)
        {
            Console.WriteLine("Здесь нет такого места, попробуйте снова.");
            return;
        }

        if (!CanEnter(GameState.current_location, target, out string failMessage))
        {
            Console.WriteLine(failMessage);
            return;
        }

        GameState.current_location = target;
        GameState.Log($"Перешёл в локацию: {target.Location_name}");
        Console.WriteLine($"Вы перешли в локацию: {target.Location_name}.");
        Console.WriteLine(target.Description);

        CheckDarkCorridorDanger(target);
    }

    private void CheckDarkCorridorDanger(LocationBase target)
    {
        if (target is DarkCorridor &&
            !HasItem("torch_1") &&
            !HasItem("fuse_1"))
        {
            GameState.health = 0;
            Console.WriteLine("В темноте вы потеряли ориентацию, и тьма забрала ваше здоровье.");
            Console.WriteLine("Вы проиграли.");
            Environment.Exit(0);
        }
    }

    public GoCommand()
    {
        name = "Идти";
        terminal_name = "идти";
    }
}
class InteractCommand : CommandBase, ICommand
{
    public override void PrintCommand()
    {
        if (GameState.current_location == null)
        {
            Console.WriteLine("Текущая локация не установлена.");
            return;
        }

        var visibleItems = GameState.current_location.Items_in_location?
            .Where(item => item != null && !item.is_in_inventory)
            .ToList();

        if (visibleItems == null || visibleItems.Count == 0)
        {
            Console.WriteLine("Здесь нет предметов для взаимодействия.");
            return;
        }

        Console.WriteLine("С каким предметом хотите взаимодействовать?");
        foreach (var item in visibleItems)
        {
            Console.WriteLine($" - {item.item_name}");
        }

        string answ = Console.ReadLine()?.Trim();
        if (string.IsNullOrEmpty(answ))
        {
            Console.WriteLine("Вы не ввели название предмета.");
            return;
        }

        var targetItem = visibleItems
            .FirstOrDefault(item => item.item_name.Equals(answ, StringComparison.OrdinalIgnoreCase));

        if (targetItem == null)
        {
            Console.WriteLine("Здесь нет такого предмета, попробуйте снова.");
            return;
        }

        targetItem.Iteractable();

        if (targetItem.is_in_inventory)
        {
            Console.WriteLine($"Предмет {targetItem.item_name} уже находится в инвентаре.");
            GameState.current_location.Items_in_location.Remove(targetItem);
            return;
        }

        if (targetItem.CanPickUp)
        {
            targetItem.is_in_inventory = true;
            GameState.inventory.Add(targetItem);
            GameState.current_location.Items_in_location.Remove(targetItem);
            GameState.Log($"Взял предмет: {targetItem.item_name}");
            Console.WriteLine($"Предмет {targetItem.item_name} добавлен в инвентарь.");
        }
        else
        {
            Console.WriteLine($"Вы не можете забрать {targetItem.item_name}.");
        }
    }
    public InteractCommand()
    {
        name = "Взаимодействовать";
        terminal_name = "взаимодействовать";
    }
}
class InventoryCommand : CommandBase, ICommand
{
    public override void PrintCommand()
    {
        if (GameState.inventory == null || GameState.inventory.Count == 0)
        {
            Console.WriteLine("Ваш инвентарь пуст.");
            return;
        }

        Console.WriteLine("У вас есть следующие предметы в инвентаре:");
        Console.WriteLine(string.Join(", ", GameState.inventory.Select(item => item.item_name)));
    }
    public InventoryCommand()
    {
        name = "Инвентарь";
        terminal_name = "инвентарь";
    }
}
class HelpCommand : CommandBase, ICommand
{
    public override void PrintCommand()
    {
        Console.WriteLine("Помощь — список команд, \nОсмотреться— описание текущей локации, \nИдти — переход в другую локацию, \nВзаимодействовать — взаимодействие с объектом, \nИнвентарь — просмотр инвентаря, \nСтатус — информация о статусе игрока.");
    }  
    public HelpCommand()
    {
        name = "Помощь";
        terminal_name = "помощь";
    }
}
class StatusCommand : CommandBase, ICommand
{
    public override void PrintCommand()
    {
        string journalText = "Журнал пуст.";
        if (GameState.journal != null && GameState.journal.Count > 0)
        {
            journalText = string.Join("; ", GameState.journal);
        }

        Console.WriteLine($"Здоровье: {GameState.health}");
        Console.WriteLine($"Текущая локация: {GameState.current_location.Location_name}");
        Console.WriteLine($"Журнал: {journalText}");
    }  
    public StatusCommand()
    {
        name = "Статус";
        terminal_name = "статус";
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
    public bool HasItem(Interactable item) {
        for (int i = 0; i < GameState.inventory.Count; i++)
        {
            if (item.item_name == GameState.inventory[i].item_name)
            {
                return true;
            }
        }
        return false;

    }
}
class FlagCondition : ConditionBase
{
    public bool flag_value;
}
// class DoorCondition : FlagCondition
// {
//     public DoorCondition(bool )
//     {
//         flag_value = flag_value;
//     }
// }
class HealthCondition : ConditionBase
{
    
}
class AndCondition : ConditionBase
{
    public bool And(bool a, bool b)
    {
        if (a == false | (b == false)){
            return false;}
        else {return true;}
        
    }
}
class OrCondition : ConditionBase
{
    public bool Or(bool a, bool b)
    {
        if (a == true | (b == true)){
            return true;}
        else {return false;}
        
    }
}
class NotCondition : ConditionBase
{
    public bool Not(bool a)
    {
        if (a == true){
            return false;}
        else {return true;}
        
    }
}

abstract class EffectBase
{
    public List<ConditionBase> itmes; //объекты класса ConditionBase
    public List<ConditionBase> flags; //объекты класса ConditionBase
    public List<ConditionBase> healths;
    public List<string> list; // журнал
    public List<LocationBase> locations;
    public EffectBase()
    {
        itmes = new List<ConditionBase>();
        flags = new List<ConditionBase>();
        healths = new List<ConditionBase>();
        list = new List<string>();
        locations = new List<LocationBase>();
    }
    // public EffectBase(как тут лучше обыграть со списком элементов, и как его сохранить)
    // {
    //     itmes = new List<ConditionBase>();
    //     flags = new List<ConditionBase>();
    //     healths = new List<ConditionBase>();
    //     list = new List<string>();
    //     locations = new List<LocationBase>();
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
// class AddItemEffect : EffectBase
// {
//     GameState.inventory.Add();
// }
// class RemoveItemEffect : EffectBase
// {
//     GameState.inventory.Remove();
// }
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
// class ChangeLocationEffect : EffectBase
// {
    
// }

abstract class LocationBase
{
    public List<LocationBase> exits;
    public List<Interactable> items_in_location;
    public string description;
    public string location_name;
    public List<GameEventBase> events;
    public LocationBase()
    {
        exits = new List<LocationBase>();
        items_in_location = new List<Interactable>();
        description = "Noting";
        location_name = "Noname";
        events = new List<GameEventBase>();
    }
    public LocationBase(string descr, string nam)
    {
        exits = new List<LocationBase>();
        items_in_location = new List<Interactable>();
        description = descr;
        location_name = nam;
        events = new List<GameEventBase>();
    }
    public List<LocationBase> Exits{get{return exits;}}
    public List<Interactable> Items_in_location{get{return items_in_location;}}
    public string Description{get{return description;}}
    public string Location_name{get{return location_name;}}
    public List<GameEventBase> Events{get{return events;}}
}
class Hall : LocationBase
{
    public Hall()
    {
        description = "Холл. Это начальная точка игры. Отсюда можно пойти в 4 направления: в лабиринт, на крышу, в темный коридор и выйти из игры. Чтобы выйти из игры, нужно найти 4 части пароля и ввести их в планшет рядом с выходом.";
        location_name = "Холл";
    }
}
class Storage : LocationBase
{
    public Storage()
    {
        description = "Склад, заполненный различными предметами и инструментами.";
        location_name = "Склад";
    }
}
class DarkCorridor : LocationBase
{
    public DarkCorridor()
    {
        description = "Темный коридор. Здесь очень темно, и вы не видите ничего вокруг. Будьте осторожны, здесь может быть опасно.";
        location_name = "Темный коридор";
    }
}
class GeneratorRoom : LocationBase
{
    public GeneratorRoom()
    {
        description = "Комната с генератором. Здесь находится генератор, который обеспечивает энергией все здание. Возможно, его можно починить и использовать для своих целей.";
        location_name = "Комната 2";
    }
}
class Roof : LocationBase
{
    public Roof()
    {
        description = "Крыша. Здесь вы можете видеть небо и город. На крыше есть много предметов, возможно, они будут полезны для чего-либо.";
        location_name = "Крыша";
    }
}
class Labyrinth : LocationBase
{
    public Labyrinth()
    {
        description = "Лабиринт. Вам нужно найти выход из лабиринта. Есть много путей, но только один ведет к выходу. Будьте осторожны.";
        location_name = "Лабиринт";
    }
}
class TrapRoom : LocationBase
{
    public TrapRoom()
    {
        description = "Вы попали в комнату, полную ловушек. Каждый шаг может быть последним. Вы будете спасены, только если у вас есть бомба.";
        location_name = "Комната 1";
    }
}

abstract class GameEventBase
{
    public List<ConditionBase> condition; //условия
    public List<EffectBase> effects;//объекты класса Effects
    public bool one_time; //признак одноразового события
    public GameEventBase()
    {
        condition = new List<ConditionBase>();
        effects = new List<EffectBase>();
        one_time = false;
    }
    public GameEventBase(bool time_1)
    {
        condition = new List<ConditionBase>();
        effects = new List<EffectBase>();
        one_time = time_1;
    }
    public List<ConditionBase> Condition{get{return condition;}}
    public List<EffectBase> Effects{get{return effects;}}
    public bool One_time{get{return one_time;}}
}
class OnEnterLocationEvent : GameEventBase
{
    public OnEnterLocationEvent()
    {
        condition = new List<ConditionBase>();
        effects = new List<EffectBase>();
        one_time = false;
    }
}
class OnTurnEvent : GameEventBase
{
    public OnTurnEvent()
    {
        condition = new List<ConditionBase>();
        effects = new List<EffectBase>();
        one_time = false;
    }
}
class OneTimeEvent : GameEventBase
{
    public OneTimeEvent()
    {
        condition = new List<ConditionBase>();
        effects = new List<EffectBase>();
        one_time = false;
    }
}

abstract class Interactable
{
    public string item_name;
    public string item_id;
    public bool is_in_inventory;
    public virtual bool CanPickUp => true;
    public abstract void Iteractable();
}
class Door : Interactable
{
    public override bool CanPickUp => false;
    public override void Iteractable()
    {
        bool hasPiece1 = GameState.inventory.Any(item => item != null && item.item_id.Equals("pieces_1", StringComparison.OrdinalIgnoreCase));
        bool hasPiece2 = GameState.inventory.Any(item => item != null && item.item_id.Equals("pieces_2", StringComparison.OrdinalIgnoreCase));
        bool hasPiece3 = GameState.inventory.Any(item => item != null && item.item_id.Equals("pieces_3", StringComparison.OrdinalIgnoreCase));
        bool hasPiece4 = GameState.inventory.Any(item => item != null && item.item_id.Equals("pieces_4", StringComparison.OrdinalIgnoreCase));

        if (hasPiece1 && hasPiece2 && hasPiece3 && hasPiece4)
        {
            Console.WriteLine("Поздравляем! Вы собрали все четыре части пароля и открыли выход через дверь.");
            Console.WriteLine("Игра окончена. Вы выиграли!");
            Environment.Exit(0);
            return;
        }

        Console.WriteLine("Дверь заперта. Чтобы открыть её, нужно найти все четыре части пароля.");
    }
    public Door()
    {
        item_name = "дверь";
        item_id = "door_1";
        is_in_inventory = false;
    }
}
class NPC : Interactable
{
    public override bool CanPickUp => false;
    public override void Iteractable()
    {
        Console.WriteLine("Здесь нет ничего, что можно взять.");
    }
    public NPC()
    {
        item_name = "терминал";
        item_id = "npc_1";
        is_in_inventory = false;
    }
}
class Trap : Interactable
{
    public override bool CanPickUp => false;
    public override void Iteractable()
    {
        Console.WriteLine("Вы наступили на ловушку! Вы получили урон.");
    }
    public Trap()
    {
        item_name = "ловушка";
        item_id = "trap_1";
        is_in_inventory = false;
    }
}
class Key : Interactable
{
    public override void Iteractable()
    {
        Console.WriteLine($"Вы подобрали ключ. Используйте его, чтобы открыть дверь.");
    }
    public Key()
    {
        item_name = "ключ";
        item_id = "key_1";
        is_in_inventory = false;
    }
}
class Torch : Interactable
{
    public override void Iteractable()
    {
        Console.WriteLine($"Вы подобрали факел. Используйте его, чтобы осветить темные места.");
    }
    public Torch()
    {
        item_name = "факел";
        item_id = "torch_1";
        is_in_inventory = false;    
    }
}
class Fuse : Interactable
{
    public override void Iteractable()
    {
        Console.WriteLine($"Вы подобрали фитиль. Используйте его, чтобы поджечь бомбу.");
    }
    public Fuse()
    {
        item_name = "фитиль";
        item_id = "fuse_1";
        is_in_inventory = false;
    }
}
class Wrench : Interactable
{
    public override void Iteractable()
    {
        Console.WriteLine($"Вы подобрали гаечный ключ. Используйте его, чтобы отремонтировать генератор.");
    }
    public Wrench()
    {
        item_name = "гаечный ключ";
        item_id = "wrench_1";
        is_in_inventory = false;
    }
}
class Bomb : Interactable
{
    public override void Iteractable()
    {
        Console.WriteLine($"Вы подобрали бомбу.");
    }
    public Bomb()
    {
        item_name = "бомба";
        item_id = "bomb_1";
        is_in_inventory = false;
    }
}
class Ladder : Interactable
{
    public override void Iteractable()
    {
        Console.WriteLine($"Вы подобрали лестницу. Используйте ее, чтобы забраться на крышу.");
    }
    public Ladder()
    {
        item_name = "лестница";
        item_id = "ladder_1";
        is_in_inventory = false;
    }
}
class Password_pieces : Interactable
{
    public int pieces_id;
    public int number_of_pieces;
    public override void Iteractable()
    {
        Console.WriteLine($"Вы подобрали часть пароля.");
    }
}
class First_pieces : Password_pieces
{
    public First_pieces()
    {
        item_name = "Кусок пароля 1";
        item_id = "pieces_1";
        pieces_id = 1;
        is_in_inventory = false;
    }
}
class Second_pieces : Password_pieces
{
    public Second_pieces()
    {
        item_name = "Кусок пароля 2";
        item_id = "pieces_2";
        pieces_id = 2;
        is_in_inventory = false;
    }
}
class Third_pieces : Password_pieces
{
    public Third_pieces()
    {
        item_name = "Кусок пароля 3";
        item_id = "pieces_3";
        pieces_id = 3;
        is_in_inventory = false;
    }
}
class Fourth_pieces : Password_pieces
{
    public Fourth_pieces()
    {
        item_name = "Кусок пароля 4";
        item_id = "pieces_4";
        pieces_id = 4;
        is_in_inventory = false;
    }   
}


static class Game
{
    public static void Main()
    {
        GameState gamestate = new GameState();
        Console.WriteLine("Добро пожаловать в игру! Введите 'помощь' для просмотра списка команд.");
        Console.WriteLine($"Вы находитесь в {GameState.current_location.Location_name}. {GameState.current_location.Description}");
        while (true)
        {
            string input = Console.ReadLine();
            bool command_found = false;
            for (int i = 0; i < GameState.commands.Count; i++)
            {
                if (input == GameState.commands[i].Terminal_name)
                {
                    GameState.Log($"Команда: {GameState.commands[i].Terminal_name}");
                    GameState.commands[i].PrintCommand();
                    command_found = true;
                    break;
                }
            }
            if (!command_found)
            {
                Console.WriteLine("Неизвестная команда, попробуйте снова.");
            }
        }
        
    }
}