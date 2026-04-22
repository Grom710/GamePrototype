using GamePrototype.Items.EconomicItems;

namespace GamePrototype.Units
{
    public abstract class Unit
    {
        private const int INVENTORY_SIZE = 3;
        private uint _health;
        private uint _maxHealth;
        protected uint BaseDamage;
        protected Inventory Inventory;

        public string Name { get; private set; }
        public uint Health
        {
            get => _health;
            protected set => _health = value;
        }

        public uint MaxHealth => _maxHealth;

        protected Unit(string name, uint health, uint maxHealth, uint baseDamage)
        {
            Name = name;
            _health = health;
            _maxHealth = maxHealth;
            BaseDamage = baseDamage;
            Inventory = new Inventory(INVENTORY_SIZE);
        }

        
        public void ApplyDamage(uint damage)
        {
            uint damageApplied = CalculateAppliedDamage(damage);

            _health = Math.Min(_health, damageApplied) == damageApplied ? 0 : _health - damageApplied;

            DamageReceiveHandler();
        }

        protected abstract uint CalculateAppliedDamage(uint damage);

        protected virtual void DamageReceiveHandler()
        {
            
        }

        public abstract uint GetUnitDamage();

        public abstract void HandleCombatComplete();

        public virtual void AddItemToInventory(Item item)
        {
            if (!Inventory.TryAdd(item))
            {
                Console.WriteLine($"Инвентарь {Name} переполнен.");
            }
        }

        public void AddItemsFromUnitToInventory(Unit defeatedUnit)
        {
            foreach (var lootItem in defeatedUnit.Inventory.Items)
            {
                if (!Inventory.TryAdd(lootItem))
                {
                   
                    Console.WriteLine($"Инвентарь {Name} переполнен, часть лута подобрать не удалось.");
                    return;
                }
            }
        }
    }
}
