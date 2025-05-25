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
        
        _controller.Direction *= -1;
    }
    public void Update()
    {
        _controller.Move(false);
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
    private Enermy _enermy;
    
    public void Enter(EnermyController controller)
    {
        _controller = controller;
        _attackDistance = 0.3f;
    }

    public void Update()
    {
        _controller.SetDirection();
        _controller.Move(false);
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

        float distance = _controller.GetDistanceToThePlayer();
        if (distance < _attackDistance)
            return new AttackState();

        return this;
    }
}

public class AttackState : IState
{
    private EnermyController _controller;
    private float _attackDistance;
    private Enermy _enermy;
    public void Enter(EnermyController controller)
    {
        _controller = controller;
        _attackDistance = 0.3f;
    }

    public void Update()
    {
        _controller.SetDirection();
        _controller.Move(true);
        _controller.Attacking();
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
        
        float distance = _controller.GetDistanceToThePlayer();
        if (distance > _attackDistance)
            return new IdleState();
        
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