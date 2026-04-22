using GamePrototype.Items; 
using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Units; 
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

        public void UseInventoryItem(int itemIndex)
        {
            if (itemIndex < 0 || itemIndex >= Inventory.Items.Count)
            {
                Console.WriteLine("Предмет с таким номером не найден в инвентаре.");
                return;
            }

            var itemToUse = Inventory.Items[itemIndex];

            if (itemToUse is IUsable usableItem)
            {
                usableItem.Use(this);

                Inventory.TryRemove(itemToUse);
            }
            else
            {
                Console.WriteLine($"Предмет {itemToUse.Name} нельзя использовать.");
            }
        }

        public override void HandleCombatComplete()
        {
            var itemsToProcess = Inventory.Items.ToList();

            foreach (var item in itemsToProcess)
            {
                if (item is IUsable usableItem)
                {
                    usableItem.Use(this);

                    Inventory.TryRemove(item);
                }
            }
        }
        /// <param name="amount"
        public void RepairWeaponInInventory(uint amount)
        {
            var weapon = Inventory.Items.OfType<Weapon>().FirstOrDefault();

            if (weapon != null)
            {
                weapon.Repair(amount);
                Console.WriteLine($"Вы восстановили {amount} прочности оружию {weapon.Name}.");
            } }

public List<Item> GetInventoryItemsForDisplay()
        {
            return new List<Item>(Inventory.Items);
        }

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