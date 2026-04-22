namespace GamePrototype.Items.EconomicItems
{
    public sealed class HealthPotion : EconomicItem
    {
        public uint HealthRestore { get; }

        public HealthPotion(uint healthRestore, string name) : base(name)
        {
            HealthRestore = healthRestore;
        }

        public override bool Stackable => false;
    }
}