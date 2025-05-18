using UnityEngine;
using UnityEngine.Serialization;

public class EnermyController : BaseController
{
    [SerializeField] private SpriteRenderer Renderer;
    [SerializeField] private Player _player;
    
    [SerializeField] private float _idletime;
    [SerializeField] private float _walkingtime;
    
    private float _currentwalkingtime;
    private float _currentidletime;
    
    public bool IsAttackPlayer { get; set; }
    public bool IsIdle { get; set; }
    public bool IsWalking { get; set; }
    public bool IsChasing { get; set; }
    
    protected override void Awake()
    {
        base.Awake();

        _transform = transform;
        _currentwalkingtime = Time.time;
        Direction = 1.0f;
        IsIdle = true;
    }
    
    protected override void Update()
    {
        base.Update();

        if (IsIdle)
        {
            _xMovement = 0;
            
            if (Time.time - _currentidletime > _idletime)
            {
                Direction *= -1;
                _currentwalkingtime = Time.time;
                IsWalking = true;
                IsIdle = false;

            }
        }
        else if (IsWalking)
        {
            _xMovement = _moveSpeed * Direction;
            
            if (Time.time - _currentwalkingtime > _walkingtime)
            {
                //Idle 플래그 On
                _currentidletime = Time.time;
                IsWalking = false;
                IsIdle = true;
            }
        }
        else if (IsChasing)
        {
            float distance = Vector2.Distance(transform.position, _player.transform.position);
            //적을 발견하고 추적
            SetDirection();
            
            if (_player._hp <= 0)
            {
                IsChasing = false;
                IsIdle = true;
                _currentidletime = Time.time;
                return;
            }
            
            if (distance < 0.3f) 
            {
                _xMovement = 0;
                Attacking();
            }
            
            else if (distance > 3.0f)
            {
                IsChasing = true;
                IsIdle = false;
            }
            
            else
            {
                _xMovement = _moveSpeed * Direction;
            }
        }
    }

    public override void Attacking()
    {
        if (!IsAttacking)
        {
            IsAttacking = true;

            if (_knife.CanAttack)
                _knife.Attack();
        }
    }
    
    public void SetDirection()
    {
        Vector3 direction = _player.transform.position - transform.position;
        Direction = direction.x > 0 ? 1 : -1;
    }
    
}

