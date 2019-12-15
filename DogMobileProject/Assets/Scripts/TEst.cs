using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEst : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerManager.Instance.CreatePlayer(Vector3.zero, Quaternion.identity, PLAYER_TYPE.POME);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
