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

abstract class CommandBase : ICommand 
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
    public override void PrintCommmand(){}
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
        locations = [];
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


abstract class LocationBase
{
    public List<???> exits; // объекты класса Location
    public List<???> items_in_location; //объект класса Interactable
    public string description; //
    public string location_name; //
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
    public List<???> Exits{get{return exits;}}
    public List<???> Items_in_location{get{return items_in_location;}}
    public string Description{get{return description;}}
    public string Location_name{get{return location_name;}}
    public List <GameEventBase> Events{get{return events;}}
}

abstract class GameEventBase
{
    public ??? condition; //условия
    public List <???> effects;//объекты класса Effects
    public bool one_time; //признак одноразового события
    public GameEventBase()
    {
        condition = ;
        effects = [];
        one_time = false;
    }
    public GameEventBase(bool time_1)
    {
        condition = ;
        effects = [];
        one_time = time_1;
    }
    public ??? Condition{get{return conditions;}}
    public List <???> Effects{get{return effects;}}
    public bool One_time{get{return one_time;}}
}
class TypeEvent : GameEventBase
{
    
}

