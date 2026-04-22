using GamePrototype.Items.EquipItems;
using GamePrototype.Units;

namespace GamePrototype.Items.EconomicItems
{
    public sealed class Grindstone : EconomicItem // Убедитесь, что EconomicItem наследуется от Item
    {
        public override bool Stackable => false;

        public Grindstone(string name) : base(name)
        {
        }

        // --- ИСПРАВЛЕННЫЙ МЕТОД ---
        // 1. Используем 'new' вместо 'override', так как сигнатура отличается от базового Item.Use(Unit)
        // 2. Приводим Unit к Player, чтобы получить доступ к Inventory
        public new void Use(Unit user)
        {
            // Проверяем, является ли юнит игроком (у монстров нет Inventory)
            if (user is Player player)
            {
                // Теперь 'player' имеет доступ к Inventory
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

                // Удаляем точильный камень из инвентаря игрока
                player.Inventory.TryRemove(this);
            }
            else
            {
                // Если вдруг этот предмет использует не игрок (например, по ошибке)
                Console.WriteLine("Этот предмет может использовать только игрок.");
            }
        }
    }
}