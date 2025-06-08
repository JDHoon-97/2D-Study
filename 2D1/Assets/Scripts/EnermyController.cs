using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnermyController : BaseController
{
    [SerializeField] private SpriteRenderer Renderer;
    [SerializeField] private Player _player;
    [SerializeField] public Enermy _enermy;
    [SerializeField] private SpecialWeapon _specailWeapon;
    
    [SerializeField] private float _idletime;
    [SerializeField] private float _walkingtime;
    [SerializeField] private float _attacktime;
    [SerializeField] private float _specialattacktime;
    [SerializeField] private Bubble _bubble;
    [SerializeField] private Transform _bubblePoint;
    private List<bool> _bubbleQueue = new List<bool>();

    public bool CanSpecialAttack { get; set; } = true;
    
    private StateMachine _stateMachine;
    
    private float _currnethp;
    private float _previoushp;
    public float IdleTime => _idletime;
    public float WalkingTime => _walkingtime;
    public float AttackTime => _attacktime;
    public float SpecailAttackTime => _specialattacktime;
    
    public bool IsDetect { get; set; }

    protected override void Awake()
    {
        base.Awake();

        _transform = transform;
        Direction = 1.0f;
        
        _stateMachine = new StateMachine(new IdleState(), this);
    }
    
    protected override void Update()
    {
        base.Update();
        Debug.Log($"현재 상태: {_stateMachine.GetCurrentStateName()}");
        //플레이어는 항상 상태 한개를 기본적으로 가짐.
        //상태는 항상 한개 >> 유한 상태 기체 (Finite State Machine)
        //FSM은 상태 패턴 중의 하나.
        //상태 패턴 > 상태를 클래스화 >> 객체화를 한다.

        _stateMachine.Update();
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

    public void Move(bool stop)
    {
        if(stop)
            _xMovement = 0;
        else
            _xMovement = _moveSpeed * Direction;
    }

    public float GetDistanceToThePlayer()
    {
        float distance = Vector2.Distance(transform.position, _player.transform.position);
        return distance;
    }

    public void GetDamage()
    {
        _currnethp = _enermy._hp;
        
        if (_currnethp < _previoushp && _enermy._hp != 0)
        {
            _animator.SetTrigger("GetDamage");
            _previoushp = _currnethp;
            Move(true);
        }
        
        _previoushp = _enermy._hp;

    }

    public void BubbleAttack()
    {
            if (_bubbleQueue.Count < 2)
                _bubbleQueue.Add(true);
            if (IsSpecialAttacking == false && _bubbleQueue.Count > 0)
            {
                _bubbleQueue.RemoveAt(0);

                IsSpecialAttacking = true;
                if (_specailWeapon.CanSpecailAttack)
                    BubbleShoot();
            }
    }
    

    private void BubbleShoot()
    {
        _animator.SetTrigger("IsBubbleAttack");
        
        Transform bubble = Instantiate(_bubble).transform;
        bubble.position = _bubblePoint.position;
        bubble.rotation = _bubblePoint.rotation;
    }

    public void SetSpecialAttackPossible()
    {
        IsSpecialAttacking = false;
    }
    
}

