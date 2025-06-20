using Unity.VisualScripting;
using UnityEngine;

public interface IState
{
    void Enter(EnermyController controller);
    void Update();
    void Exit();
    IState ChangeState();
}
public class StateMachine
{
    private IState _state;
    private EnermyController _controller;

    public StateMachine(IState state, EnermyController controller)
    {
        _controller = controller;
        _state = state;
        _state.Enter(controller);
    }

    public void Update()
    {
        _state.Update();
        
        var newState = _state.ChangeState();

        if (_state != newState)
        {
            _state.Exit();
            _state = newState;
            _state.Enter(_controller);
        }
    }
    public string GetCurrentStateName()
    {
        return _state.GetType().Name;
    }
}

public class IdleState : IState
{
    private EnermyController _controller;
    private float _currentTime;
    private float _endTime;
    private Enermy _enermy;
    
    public void Enter(EnermyController controller)
    {
        _controller = controller;
        _currentTime = Time.time;
        _endTime = controller.IdleTime;
    }
    public void Update()
    {
        _controller.Move(true);
        _controller.GetDamage();
    }
    public void Exit()
    {
    }

    public IState ChangeState()
    {
        if (_controller._enermy._hp <= 0)
            return new DeadState();
        
        if (Time.time - _currentTime > _endTime)
            return new WalkState();
        
        if (_controller.IsDetect)
            return new ChasingState();
        
        return this;
    }
}

public class WalkState : IState
{
    private EnermyController _controller;
    private float _currentTime;
    private float _endTime;
    private Enermy _enermy;
    
    public void Enter(EnermyController controller)
    {
        _controller = controller;
        _currentTime = Time.time;
        _endTime = controller.WalkingTime;
        
        _controller.TurnDirection();
    }
    public void Update()
    {
        _controller.Move(false);
        _controller.GetDamage();
    }
    public void Exit()
    {
    }

    public IState ChangeState()
    {
        if (_controller._enermy._hp <= 0)
            return new DeadState();
        
        if (Time.time - _currentTime > _endTime)
        {
            return new IdleState();
        }

        if (_controller.IsDetect)
            return new ChasingState();
        
        return this;
    }
}

public class ChasingState : IState
{
    private EnermyController _controller;
    private float _attackDistance;
    private float _currentTime;
    private float _endTime;
    private Enermy _enermy;
    
    public void Enter(EnermyController controller)
    {
        _controller = controller;
        _attackDistance = 0.3f;
        _controller.SetDirection();
        _currentTime = Time.time; 
        _endTime = controller.SpecailAttackTime;
    }

    public void Update()
    {
        _controller.Move(false);
        _controller.GetDamage();
    }

    public void Exit()
    {
    }

    public IState ChangeState()
    {
        if (_controller._enermy._hp <= 0)
            return new DeadState();
        
        if (_controller.IsDetect == false)
        {
            return new IdleState();
        }
        
          if (Time.time - _currentTime > _endTime)
          {
              return new SpecialAttackState();
          }
        
        float distance = _controller.GetDistanceToThePlayer();
        
        if (distance < _attackDistance)
            return new AttackState();
        
        return this;
    }
}
//근접 공격 상태
//원거리 공격 상태
public class AttackState : IState
{
    private EnermyController _controller;
    private float _attackDistance;
    private float _specialAttackDistance;
    private Enermy _enermy;
    private float _currentTime;
    private float _endTime;
    public void Enter(EnermyController controller)
    {
        _controller = controller;
        _attackDistance = 0.3f;
    }

    public void Update()
    {
        //범위를 검사 > 공격 패턴 정하기
            _controller.SetDirection();
            _controller.Move(true);
            _controller.Attacking();
            _controller.GetDamage();
    }

    public void Exit()
    {
    }

    public IState ChangeState()
    {
        if (_controller._enermy._hp <= 0)
            return new DeadState();

        //TODO : 공격 애니메이션이 종료되면 Idle
        if(_controller.IsAttacking==false)
            return new IdleState();
        
        if(_controller.IsDetect == false)
            return new IdleState();
        
        float distance = _controller.GetDistanceToThePlayer();
        
        if (distance > _attackDistance)
            return new IdleState();
        
        return this;
    }
}

// 원거리 범위 내에 들어오면 한 번 SpecialAttack
// 이후 근거리 될 때까지 Chasing
public class SpecialAttackState : IState
{
    private EnermyController _controller;
    private bool _oneBubble;
    public void Enter(EnermyController controller)
    {
        _controller = controller;
        _controller.CanSpecialAttack = false;
        _oneBubble = false;
    }

    public void Update()
    {
        //범위를 검사 > 공격 패턴 정하기
        _controller.SetDirection();
        _controller.Move(true);
        if (!_oneBubble)
        {
            _controller.BubbleAttack();
            _oneBubble = true;
        }
        _controller.GetDamage();
    }

    public void Exit()
    {
    }

    public IState ChangeState()
    {
        if (_controller._enermy._hp <= 0)
            return new DeadState();
        
        if(_controller.IsDetect == false)
            return new IdleState();
        
        //TODO : 원거리 공격이 끝났을 경우 Chasing 상태로 전환
        if (_controller.IsSpecialAttacking == false)
            return new ChasingState();
        
        return this;
    }
}

public class DeadState : IState
{
    private EnermyController _controller;
    private Enermy _enermy;
    
    public void Enter(EnermyController controller)
    {
        _controller = controller;
        _enermy = controller.GetComponent<Enermy>();
        
        var collider = controller.GetComponent<Collider2D>();
        var rigid = controller.GetComponent<Rigidbody2D>();
        
        if (_enermy != null)
        {
            _enermy.Dead();
        }
        
        collider.enabled = false;
        rigid.linearVelocity = Vector2.zero;
        rigid.constraints = RigidbodyConstraints2D.FreezeAll;
        _controller.enabled = false;
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }

    public IState ChangeState()
    {
        return this;
    }
}