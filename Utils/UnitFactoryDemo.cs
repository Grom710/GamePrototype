using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Units;
using GamePrototype.Utils;

namespace GamePrototype.Utils
{
    public static class UnitFactoryDemo
    {
        public static Unit CreatePlayer(string name)
        {
            var player = new Player(
                name: name,
                health: GameBalance.Player_StartHealth,
                maxHealth: GameBalance.Player_MaxHealth,
                baseDamage: GameBalance.Player_BaseDamage
            );


            player.AddItemToInventory(new HealthPotion(healthRestore: GameBalance.SmallHealthPotion_HealAmount, // Или просто 30
    name: "Зелье лечения"
    ));

            return player;
        }

        public static Unit CreateGoblinEnemy()
        {
            return new Goblin(
                name: "Гоблин",
                health: GameBalance.Goblin_Health,
                maxHealth: GameBalance.Goblin_Health,
                baseDamage: GameBalance.Goblin_Damage
            );
        }
    }
}