using GamePrototype.Units;
using GamePrototype.Items;
namespace GamePrototype.Items.EconomicItems
{
    // 1. Добавляем наследование интерфейса IUsable
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

        // 2. Изменяем сигнатуру метода, чтобы он соответствовал интерфейсу IUsable
        // Вместо Player user теперь Unit user.
        // Также делаем метод virtual, чтобы его можно было переопределить.
        public virtual void Use(Unit user)
        {
            // По умолчанию предмет ничего не делает.
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