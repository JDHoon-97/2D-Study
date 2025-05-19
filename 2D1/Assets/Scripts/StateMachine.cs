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
        if (Time.time - _currentTime > _endTime)
        {
            return new WalkState();
        }

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
        if(_controller.IsDetect == false)
            return new IdleState();
        
        float distance = _controller.GetDistanceToThePlayer();
        if (distance > _attackDistance)
            return new IdleState();
        
        return this;
    }
}