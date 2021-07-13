using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public enum PLAYER_TYPE
{
    NONE,
    PUG,
    CORGI,
    POME,
    END
}


public class PlayerManager : Singleton<PlayerManager>
{
    private MovingObject _player;

    static string XMLPath = "Data/PlayerData/";

    private Dictionary<int, PlayerViewModel> _PlayerViewModelTable = new Dictionary<int, PlayerViewModel>();
    private ObjectFactory _factory;

    public MovingObject Player
    {
        get; private set;
    }


    protected override bool Initialize()
    {
        if (!_factory)
            _factory = gameObject.AddComponent<ObjectFactory>();

        LoadXML();

        return true;
    }

    public MovingObject CreatePlayer(Vector3 _pos, Quaternion _quat, PLAYER_TYPE _type)
    {
        var vm = _PlayerViewModelTable[(int)_type];

        GameObject parentObject = Instantiate(Resources.Load<GameObject>("Prefabs/" + vm.PrefabName));
        GameObject modelObject = Instantiate(Resources.Load<GameObject>("Models/" + vm.ModelName));

        parentObject.transform.position = _pos;
        parentObject.transform.rotation = _quat;

        modelObject.transform.SetParent(parentObject.transform);
        modelObject.transform.localPosition = Vector3.zero;
        modelObject.transform.localRotation = Quaternion.identity;

        MovingObject newObj = parentObject.GetComponent<MovingObject>();

        if (newObj == null)
            newObj = parentObject.AddComponent<MovingObject>();

        newObj.Initialized(vm);

        return newObj;
    }


    void LoadXML()
    {
        TextAsset playerDataList = (TextAsset)Resources.Load(XMLPath + "PlayerData");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(playerDataList.text);

        XmlNodeList all_nodes = xmlDoc.SelectNodes("Root/text");

        foreach (XmlNode node in all_nodes)
        {
            PlayerViewModel newVM = new PlayerViewModel();
            newVM.ClassId = int.Parse(node.SelectSingleNode("ClassId").InnerText);

            newVM.MaxSpeed = float.Parse(node.SelectSingleNode("MaxSpeed").InnerText);

            newVM.ModelName = node.SelectSingleNode("ModelName").InnerText;
            newVM.PlayerName = node.SelectSingleNode("PlayerName").InnerText;
            newVM.PrefabName = node.SelectSingleNode("PrefabName").InnerText;

            newVM.Attack = float.Parse(node.SelectSingleNode("Attack").InnerText);
            newVM.Defend = float.Parse(node.SelectSingleNode("Defend").InnerText);
            newVM.MaxHP = float.Parse(node.SelectSingleNode("HP").InnerText);
            newVM.CurHP = newVM.MaxHP;
            newVM.CurrentSpeed = float.Parse(node.SelectSingleNode("StartSpeed").InnerText);
            newVM.Agility = float.Parse(node.SelectSingleNode("Agility").InnerText);
            newVM.Resistance = float.Parse(node.SelectSingleNode("Resistance").InnerText);

            newVM.MaxRotation = float.Parse(node.SelectSingleNode("MaxRotationSpeed").InnerText);
            newVM.RotationSpeed = float.Parse(node.SelectSingleNode("RotationSpeed").InnerText);
            newVM.AccelateFactor = float.Parse(node.SelectSingleNode("AccelateFactor").InnerText);

            newVM.CurrentPosition = Vector3.zero;
            newVM.CurrentRotation = Quaternion.identity;
            newVM.CurrentDirection = Vector3.forward;

            _PlayerViewModelTable.Add(newVM.ClassId, newVM);
        }
    }



}
