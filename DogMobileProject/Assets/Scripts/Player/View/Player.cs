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

    protected override void Update()
    {
        base.Update();
    }


}
