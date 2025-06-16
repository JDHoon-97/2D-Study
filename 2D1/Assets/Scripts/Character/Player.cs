using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    private void Start()
    {
        GameManager.Instance.Player = this;
    }
    public void TakePlayerDamage(int damage)
    {
        base.TakeDamage(damage);
    }
}