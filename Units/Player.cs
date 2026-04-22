using GamePrototype.Items; // <-- НОВОЕ: Для интерфейса IUsable
using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Units; // Для базового класса Unit
using GamePrototype.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GamePrototype.Units
{
    public sealed class Player : Unit
    {
        private readonly Dictionary<EquipSlot, EquipItem> _equipment = new();

        public Player(string name, uint health, uint maxHealth, uint baseDamage)
            : base(name, health, maxHealth, baseDamage)
        {
        }

        // --- НОВЫЙ МЕТОД: Использовать предмет из инвентаря ---
        // Этот метод вызывается из GameLoop, когда игрок выбирает действие "Использовать".
        public void UseInventoryItem(int itemIndex)
        {
            if (itemIndex < 0 || itemIndex >= Inventory.Items.Count)
            {
                Console.WriteLine("Предмет с таким номером не найден в инвентаре.");
                return;
            }

            var itemToUse = Inventory.Items[itemIndex];

            // Проверяем, реализует ли предмет интерфейс IUsable.
            // Теперь это может быть любой предмет, а не только EconomicItem.
            if (itemToUse is IUsable usableItem)
            {
                // Вызываем метод Use. Мы передаем 'this' (самого себя), чтобы предмет
                // мог взаимодействовать с игроком (например, починить его оружие).
                usableItem.Use(this);

                // После использования удаляем предмет из инвентаря.
                // Это стандартное поведение для большинства одноразовых предметов.
                Inventory.TryRemove(itemToUse);
            }
            else
            {
                Console.WriteLine($"Предмет {itemToUse.Name} нельзя использовать.");
            }
        }

        // --- ОБНОВЛЕННЫЙ МЕТОД: Обработка после боя ---
        // Теперь этот метод не содержит логику для конкретных предметов.
        // Он просто дает команду "использоваться" всем предметам, которые это умеют.
        public override void HandleCombatComplete()
        {
            // Создаем копию списка, чтобы безопасно удалять элементы во время цикла.
            var itemsToProcess = Inventory.Items.ToList();

            foreach (var item in itemsToProcess)
            {
                // Если предмет можно использовать...
                if (item is IUsable usableItem)
                {
                    // ...он использует сам себя на этом игроке.
                    usableItem.Use(this);

                    // И после этого исчезает из инвентаря.
                    Inventory.TryRemove(item);
                }
            }
        }

        // --- МЕТОД ВЫПОЛНЕНИЯ ДЕЙСТВИЯ ПРЕДМЕТА ---
        // Этот метод больше НЕ НУЖЕН!
        // Вся логика теперь находится внутри самих классов предметов (HealthPotion, Grindstone).
        // Мы удаляем его, чтобы не дублировать код.
        /*
        private void UseEconomicItem(EconomicItem economicItem)
        {
            // Старая логика отсюда перемещена в классы HealthPotion и Grindstone.
        }
        */

        protected override uint CalculateAppliedDamage(uint damage)
        {
            if (_equipment.TryGetValue(EquipSlot.Armour, out var item) && item is Armour armour)
            {
                if (armour.Durability > 0)
                {
                    armour.WearOut();

                    if (armour.Durability == 0)
                    {
                        Console.WriteLine($"Броня {armour.Name} сломана!");
                    }

                    float damageReduction = ((float)armour.Defence / 100f);
                    damage -= (uint)(damage * damageReduction);
                    damage = Math.Max(damage, 0);
                }
                else
                {
                    Console.WriteLine($"Броня {armour.Name} уже была сломана.");
                }
            }
            return damage;
        }

        public override uint GetUnitDamage()
        {
            if (_equipment.TryGetValue(EquipSlot.Weapon, out var item) && item is Weapon weapon)
            {
                return BaseDamage + weapon.Damage;
            }
            return BaseDamage;
        }
    }
}