using UnityEngine;
using UnityEngine.Serialization;

public class EnermyController : BaseController
{
    [SerializeField] private SpriteRenderer Renderer;
    [SerializeField] private Player _player;
    [SerializeField] public Enermy _enermy;
    
    [SerializeField] private float _idletime;
    [SerializeField] private float _walkingtime;
    
    private StateMachine _stateMachine;
    
    public float IdleTime => _idletime;
    public float WalkingTime => _walkingtime;
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
    
}

