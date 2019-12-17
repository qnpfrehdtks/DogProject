using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViewModelExtensions;

public class MovingObject : MonoBehaviour
{
    protected Dictionary<STATE_TYPE, string> _AnimTable = new Dictionary<STATE_TYPE, string>();
    protected Dictionary<STATE_TYPE, State> _StateTable = new Dictionary<STATE_TYPE, State>();

    protected GameObject _parentObject;
    protected GameObject _model;
    protected PlayerViewModel _viewModel;
    protected Animator _objectAnimator;
    protected InputController _controller;

    protected State _CurrentState;

    private Coroutine _coroutine;


    public State CurrentState
    {
        get => _CurrentState;
        set
        {
            if (_CurrentState != null)
            {
                _CurrentState.EndAction(this);
            }

            _CurrentState = value;
            _CurrentState.StartAction(this);

        }
    }


    public PlayerViewModel ViewModel
    {
        get => _viewModel;
    }
    public InputController Controller
    {
        get => _controller;
        set
        {
            if (_controller)
                Destroy(_controller);

            _controller = value;
            _controller.Player = this;
        }
    }
    public Animator ObjectAnimator
    {
        get => _objectAnimator;
        set
        {
            _objectAnimator = value;
        }
    }

    protected virtual void Awake()
    {
       
    }

    protected virtual void Update()
    {
        if (_CurrentState == null) return;

        _CurrentState.ExcuteAction(this);
    }

    public void Initialized(PlayerViewModel viewModel)
    {
        Debug.Log("Init Object : " + gameObject.name);
        _viewModel = viewModel;

        _controller = gameObject.AddComponent<NormalInputContoller>();
        _controller.Player = this;

        _objectAnimator = GetComponentInChildren<Animator>();

        // ======================== 애니 정보 ViewModel에서 하나? =======================
        InsertAnimation(STATE_TYPE.IDLE, "Idle");
        InsertAnimation(STATE_TYPE.ACCELATING, "Run");
        InsertAnimation(STATE_TYPE.COLLISION, "Crush");
        InsertAnimation(STATE_TYPE.KNOCK_BACK, "KnockBack");

        // ======================== State 정보 ViewModel에서 하나? =======================
        InsertState(STATE_TYPE.IDLE, new IdleState());
        InsertState(STATE_TYPE.ACCELATING, new RunState());
        InsertState(STATE_TYPE.GALLOP, new GallopState());

        ChangeState(STATE_TYPE.IDLE);

    }

    public void PlayAnimation(STATE_TYPE stateType)
    {
        string str;
        if (_AnimTable.TryGetValue(stateType, out str))
        {
            _objectAnimator.Play(str);
        }
    }

    public void StopObject()
    {
        _viewModel.resetSpeed();
    }

    private void InsertAnimation(STATE_TYPE state, string str)
    {
        if (_AnimTable.ContainsKey(state)) return;

        _AnimTable.Add(state, str);
    }

    private void InsertState(STATE_TYPE type, State state)
    {
        if (_StateTable.ContainsKey(type)) return;

        _StateTable.Add(type, state);
    }

    public void ChangeState(STATE_TYPE state, float delaytime = 0.0f)
    {
        if (!_StateTable.ContainsKey(state)) return;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeState_C(state, delaytime));
    }

    IEnumerator ChangeState_C(STATE_TYPE state, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        CurrentState = _StateTable[state];
    }


}

