using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField] private Controller _controller;
    [SerializeField] private EnermyController _enermyController;
    
    private Enermy _enermy;
    private Player _player;

    public bool CanAttack => _enermy|| _player != null;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enermy"))
        {
            _enermy = collision.gameObject.GetComponent<Enermy>();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            _player = collision.gameObject.GetComponent<Player>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enermy"))
        {
            _enermy = null;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            _player = null;
        }
    }

    public void Attack()
    {
        _controller.Animator.SetTrigger("IsKnifeAttack");
        _enermyController.Animator.SetTrigger("EnermyAttack");
        
        if (_enermy)
        {
            _enermy.TakeDamage(1);
            
        }

        if (_player)
        {
            _player.TakePlayerDamage(1);
        }
    }
}
