using UnityEngine;

public class EnermyKnife : BaseKnife
{
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
        _controller.Animator.SetTrigger("IsKnifeAttack");

        if (_player)
        {
            _player.TakePlayerDamage(1);
        }
    }
}
