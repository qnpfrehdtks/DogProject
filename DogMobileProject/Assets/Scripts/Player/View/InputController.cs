using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputController : MonoBehaviour
{
    private delegate bool pressAction();

    protected MovingObject _player;
    protected PlayerViewModel _viewModel;
    protected Transform _playerTransform;
    protected Animator _playerAnimator;

    pressAction _pressAction;

    public PlayerViewModel ViewModel { get; protected set; }
    public MovingObject Player
    {
        get => _player;
        set
        {
            _player = value;
            _playerTransform = _player.transform;
            _viewModel = _player.ViewModel;
            _playerAnimator = _player.ObjectAnimator;
        }
    }
    protected abstract void UpdateController();

    protected void ObjectMove(Vector3 screenPos)
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

        if (_playerAnimator != null)
            _playerAnimator.Play("Run");
    }

    protected virtual void Update()
    {
        UpdateController();
    }

}

public class EditorInputController : InputController
{
    protected override void UpdateController()
    {
        if (Input.GetMouseButton(0))
        {
            ObjectMove(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _viewModel.resetSpeed();
            _playerAnimator.Play("Idle");
        }
    }
}

public class MobileInputController : InputController
{

    protected override void UpdateController()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                ObjectMove(touch.position);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                _viewModel.resetSpeed();
                _playerAnimator.Play("Idle");
            }
        }
    }

}
