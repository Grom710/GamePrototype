using GamePrototype.Items.EconomicItems;
using GamePrototype.Utils;

namespace GamePrototype.Items.EquipItems
{
    public abstract class EquipItem : Item
    {
        private uint _durability;
        private uint _maxDurability;
        public uint Durability { get => _durability; protected set => _durability = value; }
        public override bool Stackable => false;

        public abstract EquipSlot Slot { get; }

        // Конструктор теперь инициализирует текущую прочность равной максимальной
        protected EquipItem(uint maxDurability, string name) : base(name)
        {
            _maxDurability = maxDurability;
            _durability = maxDurability; // Новая броня целая
        }

        // Улучшенный метод уменьшения прочности, который не дает уйти в минус
        public void ReduceDurability(uint delta)
        {
            // Используем Math.Max (или условный оператор), чтобы прочность не стала меньше 0
            _durability = _durability > delta ? _durability - delta : 0;
        }

        // Новый, более конкретный метод для нашей задачи
        public void WearOut()
        {
            // Вызываем существующий метод с параметром 1
            ReduceDurability(1);
        }

        public void Repair(uint delta)
        {
            // Исправлена логика в тернарном операторе (убрано лишнее сложение)
            _durability = (_durability + delta > _maxDurability)
                ? _maxDurability
                : _durability + delta;
        }
    }
}