using UnityEngine;

namespace GMS.Code
{
    public class DeadZone : MonoBehaviour
    {
        [SerializeField] private Transform _RespawnPoint;
        [SerializeField] private PlayerHPUI _playerHPUI;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                collision.gameObject.transform.position = _RespawnPoint.position;
                _playerHPUI.DescountHart();
            }
        }
    }
}
