using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViewModelExtensions;

public class Player : MonoBehaviour
{
    PlayerViewModel m_playerViewModel;
    // Start is called before the first frame update
    void Start()
    {
        m_playerViewModel = new PlayerViewModel();

        m_playerViewModel = PlayerViewModelExtensions._VMTable[1];
        m_playerViewModel.Log2();

        m_playerViewModel.CurrentPosition = transform.position;
        m_playerViewModel.CurrentRotation = transform.rotation;

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(Screen.width, (int)(Screen.width * (16.0f / 9.0f)), true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Wall")
            Destroy(gameObject);
    }

    private void Update()
    {
#if UNITY_EDITOR 
        if (Input.GetMouseButton(0))
        {
            m_playerViewModel.UpdatePosition( transform.forward);
            transform.rotation = m_playerViewModel.CurrentRotation;
            transform.position = m_playerViewModel.CurrentPosition;

        }
#elif UNITY_ANDROID 
        if (Input.touchCount > 0)
        {
           Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Moved)
            {
                m_playerViewModel.UpdatePosition(transform.forward);
                transform.rotation = m_playerViewModel.CurrentRotation;
                transform.position = m_playerViewModel.CurrentPosition;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                m_playerViewModel.ResetSpeed();
            }
        }
#endif
    }



}
