using GamePrototype.Utils;

namespace GamePrototype.Items.EquipItems
{
    public class Armour : EquipItem 
    {
        public uint Defence { get; }

        public Armour(uint defence, uint maxDurability, string name)
            : base(maxDurability, name)
        {
            Defence = defence;
        }

        public override EquipSlot Slot => EquipSlot.Armour;
    }
}