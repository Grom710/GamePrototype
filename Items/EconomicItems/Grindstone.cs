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
        public override void Use(Unit user)
        {
            if (user is Player player)
            {
                player.RepairWeaponInInventory(100);
            }
            else
            {
                Console.WriteLine("Этот предмет может использовать только игрок.");
            }
        }
    }
}