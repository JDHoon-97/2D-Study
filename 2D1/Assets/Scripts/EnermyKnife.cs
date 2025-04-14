using UnityEngine;

public class EnermyKnife : BaseKnife
{
    [SerializeField] private EnermyController _enermyController;
    
    private Player _player;

    public override bool CanAttack => _player != null;
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player = collision.gameObject.GetComponent<Player>();
        }
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player = null;
        }
    }

    public override void Attack()
    {
        _enermyController.Animator.SetTrigger("EnermyAttack");

        if (_player)
        {
            _player.TakePlayerDamage(1);
        }
    }
}
