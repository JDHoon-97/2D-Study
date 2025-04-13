using UnityEngine;

public class EnermyKnife : MonoBehaviour
{
    [SerializeField] private EnermyController _enermyController;
    
    private Player _player;

    public bool CanAttack => _player != null;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player = collision.gameObject.GetComponent<Player>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player = null;
        }
    }

    public void Attack()
    {
        _enermyController.Animator.SetTrigger("EnermyAttack");

        if (_player)
        {
            _player.TakePlayerDamage(1);
        }
    }
}
