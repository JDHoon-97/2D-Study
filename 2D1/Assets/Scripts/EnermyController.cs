using UnityEngine;
using UnityEngine.Serialization;

public class EnermyController : BaseController
{
    [SerializeField] private SpriteRenderer Renderer;

    [SerializeField] private float _idletime;
    [SerializeField] private float _walkingtime;
    
    private float _currentwalkingtime;
    private float _currentidletime;

    public bool IsIdle { get; set; }
    public bool IsWalking { get; set; }
    public bool IsChasing { get; set; }
    
    protected override void Awake()
    {
        base.Awake();

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
            //적을 발견하고 추적
            _xMovement = _moveSpeed * Direction;
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
    
    
}

