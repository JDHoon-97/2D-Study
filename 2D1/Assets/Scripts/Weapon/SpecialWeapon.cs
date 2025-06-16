using UnityEngine;

public class SpecialWeapon : MonoBehaviour
{
    private Player _player;

    public bool CanSpecialAttack => _player != null;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player = collision.gameObject.GetComponent<Player>();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player = null;
        }
    }
}
