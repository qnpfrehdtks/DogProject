using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputController : MonoBehaviour
{
    public delegate bool ScreenAction();

    protected MovingObject _player;
    protected PlayerViewModel _viewModel;
    protected Transform _playerTransform;

    public ScreenAction _pressConditions { get; set; }
    public ScreenAction _releaseConditions { get; set; }

    public PlayerViewModel ViewModel { get; protected set; }
    public MovingObject Player
    {
        get => _player;
        set
        {
            _player = value;
            _playerTransform = _player.transform;
            _viewModel = _player.ViewModel;
        }
    }

    private void Awake()
    {
#if UNITY_EDITOR
        _pressConditions = delegate ()
        {
            if (Input.GetMouseButtonDown(0))
            {
               // PressScreen(Input.mousePosition);
                return true;
            }
            else
                return false;
        };
        _releaseConditions = delegate ()
        {
            if (Input.GetMouseButtonUp(0))
            {
               // ReleaseScreen();
                return true;
            }
            else
                return false;
        };

#elif UNITY_ANDROID

        _pressAction = delegate ()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                return (touch.phase == TouchPhase.Moved);
            }

            return false;
        };

        _releaseAction = delegate ()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                return (touch.phase == TouchPhase.Ended);
            };

            return false;
        };
#endif
    }


    public abstract void PressScreen(Vector3 screenPos);
    public abstract void ReleaseScreen();
}


class DefalutInputController : InputController
{
    public override void PressScreen(Vector3 screenPos)
    {
    }

    public override void ReleaseScreen()
    {
    }
}


class NormalInputContoller : InputController
{
    public override void PressScreen(Vector3 screenPos)
    {
        if (_viewModel == null) return;
        if (_playerTransform == null) return;

        _viewModel.UpdateSpeed();

        _playerTransform.position += _viewModel.CurrentSpeed * Time.deltaTime * _playerTransform.forward;

        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, 100.0f)) return;

        Vector3 newScreenPos = hit.point;
        newScreenPos.y = _playerTransform.position.y;
        Vector3 playerScreenPos = _playerTransform.position;

        float angle = -Mathf.Atan2(playerScreenPos.z - newScreenPos.z, playerScreenPos.x - newScreenPos.x) * Mathf.Rad2Deg;
        Vector3 rot = Vector3.Slerp(_playerTransform.forward, (newScreenPos - playerScreenPos).normalized, Time.deltaTime / (_viewModel.RotationSpeed));

        _playerTransform.forward = rot;

    }

    public override void ReleaseScreen()
    {
        _viewModel.resetSpeed();
    }
}


