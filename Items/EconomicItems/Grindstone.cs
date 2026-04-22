using GamePrototype.Items.EquipItems;
using GamePrototype.Units;

namespace GamePrototype.Items.EconomicItems
{
    public sealed class Grindstone : EconomicItem 
    {
        public override bool Stackable => false;

        public Grindstone(string name) : base(name)
        {
        }

        public new void Use(Unit user)
        {
            if (user is Player player)
            {
                var weaponInInventory = player.Inventory.Items
                    .OfType<Weapon>()
                    .FirstOrDefault();

                if (weaponInInventory != null)
                {
                    weaponInInventory.Repair(100);
                    Console.WriteLine($"Вы наточили {weaponInInventory.Name}! Прочность восстановлена.");
                }
                else
                {
                    Console.WriteLine("В инвентаре нет оружия, которое можно наточить.");
                }

                player.Inventory.TryRemove(this);
            }
            else
            {
                Console.WriteLine("Этот предмет может использовать только игрок.");
            }
        }
    }
}