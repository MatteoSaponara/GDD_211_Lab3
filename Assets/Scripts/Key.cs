using UnityEngine;

namespace game
{
    public class Key : Pickup
    {
        public override void collect(Player player)
        {
            GameManager.Instance.CollectKey();
            base.collect(player);
        }
    }
}
