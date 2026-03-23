using UnityEngine;

namespace game
{
    public class EnergyDrink : Pickup
    {
        [SerializeField] private static float speedMultiplier;
        public override void collect(Player player)
        {
            player.SpeedBoost();
            base.collect(player);
        }
    }
}
