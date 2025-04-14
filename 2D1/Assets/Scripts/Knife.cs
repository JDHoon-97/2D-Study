using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Knife : BaseKnife
{
    private List <Enermy> _enemies = new List<Enermy>();

    public override bool CanAttack => _enemies.Count > 0;
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enermy"))
        {
            _enemies.Add(collision.gameObject.GetComponent<Enermy>());
        }
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enermy"))
        {
            _enemies.Remove(collision.gameObject.GetComponent<Enermy>());
        }
    }

    public override void Attack()
    {
        _controller.Animator.SetTrigger("IsKnifeAttack");

        for (int i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].TakeEnermyDamage(1);
        }
    }
}
