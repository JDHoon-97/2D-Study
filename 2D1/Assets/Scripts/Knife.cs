using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Knife : BaseKnife
{
    private List <Enermy> _enemies = new List<Enermy>();

    public override bool CanAttack => _enemies.Count > 0;
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enermy")||collision.gameObject.CompareTag("Crab"))
        {
            var enemy = collision.GetComponent<Enermy>();
            if (enemy != null && !enemy.IsDead && !_enemies.Contains(enemy))
            {
                _enemies.Add(enemy);
            }
        }
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enermy")||collision.gameObject.CompareTag("Crab"))
        {
            _enemies.Remove(collision.gameObject.GetComponent<Enermy>());
        }
    }

    public override void Attack()
    {
        _controller.Animator.SetTrigger("IsKnifeAttack");

        for (int i = 0; i < _enemies.Count; i++)
        {
            if (_enemies[i] == null || _enemies[i].IsDead)
            {
                _enemies.RemoveAt(i);
                continue;
            }
            
            _enemies[i].TakeDamage(1);
        }
    }
}
