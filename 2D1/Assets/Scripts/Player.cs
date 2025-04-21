using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    public void TakePlayerDamage(int damage)
    {
        base.TakeDamage(damage);
    }
}