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

        public void RepairWeaponInInventory(uint amount)
        {
            var weapon = Inventory.Items.OfType<Weapon>().FirstOrDefault();

            if (weapon != null)
            {
                weapon.Repair(amount);
                Console.WriteLine($"Вы восстановили {amount} прочности оружию {weapon.Name}.");
            }
        }

        public List<Item> GetInventoryItemsForDisplay()
        {
            return new List<Item>(Inventory.Items);
        }

        public override void AddItemToInventory(Item item)
        {
            if (item is EquipItem equipItem)
            {
                if (_equipment.TryGetValue(equipItem.Slot, out var oldEquipItem))
                {
                    base.AddItemToInventory(oldEquipItem);

                    _equipment[equipItem.Slot] = equipItem;

                    DisplayEquipMessage(equipItem.Slot.ToString(), equipItem.Name);
                    return; 
                }
                else
                {
                    _equipment[equipItem.Slot] = equipItem;
                    Console.WriteLine($"Вы надели {equipItem.Name} в слот {equipItem.Slot}.");
                    return;
                }
            }

            if (!Inventory.TryAdd(item))
            {
                Console.WriteLine($"Инвентарь {Name} переполнен.");
            }
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

            if (_equipment.TryGetValue(EquipSlot.Helmet, out var helmetItem) && helmetItem is Helmet helmet)
            {
                if (helmet.Durability > 0)
                {
                    float helmetReduction = ((float)helmet.Defence / 100f);
                    damage -= (uint)(damage * helmetReduction);
                    damage = Math.Max(damage, 0);

                    helmet.WearOut(); 

                    if (helmet.Durability == 0)
                    {
                        Console.WriteLine($"Шлем {helmet.Name} сломан!");
                    }
                }
            }


            return damage;
        }


        public override uint GetUnitDamage()
        {
            uint totalDamage = BaseDamage;

            if (_equipment.TryGetValue(EquipSlot.Weapon, out var item) && item is Weapon weapon)
            {
                totalDamage += weapon.Damage;
            }

            if (_equipment.TryGetValue(EquipSlot.RangeWeapon, out var rangeItem) && rangeItem is RangeWeapon rangeWeapon)
            {
                totalDamage += rangeWeapon.Damage;
            }

            return totalDamage;
        }


        private void DisplayEquipMessage(string slotName, string newItemName)
        {
            Console.WriteLine($"Экипировка в слоте '{slotName}' заменена на '{newItemName}'. Старая экипировка перемещена в инвентарь.");
        }
    }
}