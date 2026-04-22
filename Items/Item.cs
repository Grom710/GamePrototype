using GamePrototype.Units;
using GamePrototype.Items;
namespace GamePrototype.Items.EconomicItems
{
    public abstract class Item : IUsable
    {
        public abstract bool Stackable { get; }

        public virtual uint Amount { get; protected set; }

        public string Name { get; }

        protected Item(string name)
        {
            Name = name;
            Amount = 1;
        }
        public virtual void Use(Unit user)
        {
            Console.WriteLine($"Вы использовали {Name}. Ничего не произошло.");
        }


        public bool TryStack(Item item)
        {
            if (!Stackable)
            {
                return false;
            }
            Amount++;
            return true;
        }
    }
}