using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViewModelExtensions;

public class MovingObject : MonoBehaviour
{
    protected GameObject _parentObject;
    protected GameObject _model;
    protected PlayerViewModel _viewModel;
    protected Animator _objectAnimator;
    protected InputController _contoller;

    public PlayerViewModel ViewModel
    {
        get => _viewModel;
    }
    public InputController Controller
    {
        get => _contoller;
        set
        {
            _contoller = value;
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
        _objectAnimator = GetComponentInChildren<Animator>();
    }

    public void Initialized(PlayerViewModel viewModel)
    {
        _viewModel = viewModel;

#if UNITY_EDITOR
        _contoller = gameObject.AddComponent<EditorInputController>();
#elif UNITY_ANDRIOD
        _contoller = gameObject.AddComponent<MobileInputController>();
#endif
        _contoller.Player = this;
    }



}

