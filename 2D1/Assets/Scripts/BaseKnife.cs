using UnityEngine;

public abstract class BaseKnife : MonoBehaviour
{
    public abstract bool CanAttack { get; }
    
    public abstract void OnTriggerEnter2D(Collider2D collision);
    public abstract void OnTriggerExit2D(Collider2D collision);
    public abstract void Attack();
}
