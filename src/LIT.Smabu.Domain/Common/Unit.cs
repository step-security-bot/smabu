using LIT.Smabu.Domain.Shared;

namespace LIT.Smabu.Domain.Common
{
    public record Unit(string Key) : SimpleValueObject<string>(Key)
    {
        public static Unit None => new("");
        public static Unit Hour => new("Hour");
        public static Unit Item => new("Item");

        public string Name => Key switch
        {
            "" => "",
            "Hour" => "Stunde",
            "Item" => "Stück",
            _ => "???"
        };

        public string ShortName => Key switch
        {
            "" => "",
            "Hour" => "Std",
            "Item" => "Stk",
            _ => "???"
        };

        public static Unit[] GetAll() => [None, Hour, Item];
    }
}
