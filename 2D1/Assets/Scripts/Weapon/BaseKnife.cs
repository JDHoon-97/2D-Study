using UnityEngine;

public abstract class BaseKnife : MonoBehaviour
{
    [SerializeField] protected BaseController _controller;
    public abstract bool CanAttack { get; }
    
    public abstract void OnTriggerEnter2D(Collider2D collision);
    public abstract void OnTriggerExit2D(Collider2D collision);
    public abstract void Attack();
}
