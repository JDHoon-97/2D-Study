using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField] private Controller _controller;
    private Enermy _enermy;

    public bool CanAttack => _enermy != null;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enermy"))
        {
            _enermy = collision.gameObject.GetComponent<Enermy>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enermy"))
        {
            _enermy = null;
        }
    }

    public void Attack()
    {
        _controller.Animator.SetTrigger("IsKnifeAttack");

        if (_enermy)
        {
            _enermy.TakeDamage(1);
            
        }
    }
}
