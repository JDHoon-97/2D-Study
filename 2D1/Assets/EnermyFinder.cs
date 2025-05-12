using System.Transactions;
using UnityEngine;

public class EnermyFinder : MonoBehaviour
{
    [SerializeField] private EnermyController _controller;
    [SerializeField] private Player _player;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == _player.gameObject)
        {
            //TODO : 추적 상태로 전환
            _controller.IsIdle = false;
            _controller.IsWalking = false;
            _controller.IsChasing = true;
            
            SetDirection();
        }
    }

    private void SetDirection()
    {
        Vector3 direction = _player.transform.position - transform.position;
        _controller.Direction = direction.x > 0 ? 1 : -1;
    }
}
