namespace LIT.Smabu.Shared.Entities.Business.CustomerAggregate
{
    public class CustomerNumber : BusinessNumber
    {
        public CustomerNumber(long value) : base (value) { }

        public override string ShortForm => "CUS";

        public override int Digits => 4;
    }
}