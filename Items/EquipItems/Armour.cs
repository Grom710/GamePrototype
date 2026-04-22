using GamePrototype.Utils;

namespace GamePrototype.Items.EquipItems
{
    public class Armour : EquipItem // Убрал sealed, если планируется наследование от брони
    {
        public uint Defence { get; }

        // Конструктор передает параметры в базовый класс (EquipItem)
        public Armour(uint defence, uint maxDurability, string name)
            : base(maxDurability, name)
        {
            Defence = defence;
        }

        public override EquipSlot Slot => EquipSlot.Armour;
    }
}