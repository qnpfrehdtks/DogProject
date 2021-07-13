using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViewModelExtensions;

public class Player : MovingObject
{
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
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

    public float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }


    private void Update()
    {

    }



}
