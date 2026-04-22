using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Units;
using GamePrototype.Utils; // <-- ВАЖНО: Добавляем using для доступа к GameBalance

namespace GamePrototype.Utils
{
    public static class UnitFactoryDemo
    {
        // Метод для создания игрока. Теперь он использует константы из GameBalance.
        public static Unit CreatePlayer(string name)
        {
            // Используем данные из "склада" GameBalance
            var player = new Player(
                name: name,
                health: GameBalance.Player_StartHealth,
                maxHealth: GameBalance.Player_MaxHealth,
                baseDamage: GameBalance.Player_BaseDamage
            );


            // Зелье (Potion)
            player.AddItemToInventory(new HealthPotion(healthRestore: GameBalance.SmallHealthPotion_HealAmount, // Или просто 30
    name: "Зелье лечения"
    ));

            return player;
        }

        // Метод для создания гоблина. Заменяем старый подход на новый.
        public static Unit CreateGoblinEnemy()
        {
            // Используем константы из GameBalance вместо "магических чисел" или старых констант
            return new Goblin(
                name: "Гоблин",
                health: GameBalance.Goblin_Health,
                maxHealth: GameBalance.Goblin_Health,
                baseDamage: GameBalance.Goblin_Damage
            );
        }
    }
}