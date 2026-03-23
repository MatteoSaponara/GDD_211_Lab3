using Unity.VisualScripting;
using UnityEngine;

namespace game
{
    public abstract class Pickup : MonoBehaviour
    {
        [SerializeField] private Collider c;
        [SerializeField] private float RotationSpeed;

        private void Update()
        {
            Debug.Log("Rotating:");
            transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Player collided with " + this);
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                collect(player);
            }
        }

        public virtual void collect(Player player)
        {
            Debug.Log("Picked up: " + this);
            Destroy(gameObject);
        }
    }

    public class HourGlass : Pickup
    {
        [SerializeField] private float extraTime;

        public override void collect(Player player)
        {
            base.collect(player);
        }
    }
}
