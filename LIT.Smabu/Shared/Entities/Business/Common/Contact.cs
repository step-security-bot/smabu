namespace LIT.Smabu.Shared.Entities.Business.Common
{
    public class Contact : Entity<ContactId>
    {
        public Contact(ContactId id, Person person, Address mainAddress, Communication communication)
        {
            Id = id;
            Person = person;
            MainAddress = mainAddress;
            Communication = communication;
        }

        public override ContactId Id { get; }
        public Person Person { get; private set; }
        public Address MainAddress { get; private set; }
        public Communication Communication { get; private set; }

        public void EditMainAddress(Address mainAddress)
        {
            MainAddress = mainAddress;
        }

        public void EditCommunication(Communication communication)
        {
            Communication = communication;
        }

        public static Contact Create(ContactId contactId, string name1)
        {
            return new Contact(contactId, new Person("", "", name1, ""), 
                new Address(name1, "", "", "", "", "", "", ""), 
                new Communication("", "", "", ""));
        }
    }
}
