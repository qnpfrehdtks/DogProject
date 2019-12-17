using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFactory : MonoBehaviour
{
    public T CreateObject<T>(Vector3 pos, Quaternion quat, PLAYER_TYPE type, PlayerViewModel viewModel) where T : MovingObject
    {
        
        GameObject parentObject = Instantiate(Resources.Load<GameObject>("Prefabs/" + viewModel.PrefabName));
        GameObject modelObject = Instantiate(Resources.Load<GameObject>("Models/" + viewModel.ModelName));

        parentObject.transform.position = pos;
        parentObject.transform.rotation = quat;

        modelObject.transform.SetParent(parentObject.transform);
        modelObject.transform.localPosition = Vector3.zero;
        modelObject.transform.localRotation = Quaternion.identity;
        modelObject.transform.localScale = Vector3.one * 1.2f;
        T newObj = parentObject.GetComponent<T>();

        if (newObj == null)
            newObj = parentObject.AddComponent<T>();

        newObj.Initialized(viewModel);

        return newObj;
    }
}
