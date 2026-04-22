// GamePrototype/Items/IUsable.cs

using GamePrototype.Units;

namespace GamePrototype.Items
{
    /// <summary>
    /// Интерфейс для предметов, которые можно использовать.
    /// </summary>
    public interface IUsable
    {
        /// <summary>
        /// Метод, который вызывается при использовании предмета.
        /// </summary>
        /// <param name="user">Юнит, который использует предмет.</param>
        void Use(Unit user);
    }
}