using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

namespace ViewModelExtensions
{
    public static class PlayerViewModelExtensions
    {
        static string XMLPath = "Data/PlayerData/";
        public static Dictionary<int, PlayerViewModel> _VMTable = new Dictionary<int, PlayerViewModel>();


        public static void Init( this PlayerViewModel vm)
        {
            TextAsset playerDataList = (TextAsset)Resources.Load(XMLPath + "PlayerData");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(playerDataList.text);

            XmlNodeList all_nodes = xmlDoc.SelectNodes("Root/text");

            foreach (XmlNode node in all_nodes)
            {
                PlayerViewModel newVM = new PlayerViewModel();
                vm.ClassId = int.Parse(node.SelectSingleNode("ClassId").InnerText);

                vm.MaxSpeed = float.Parse(node.SelectSingleNode("MaxSpeed").InnerText);

                vm.ModelName = node.SelectSingleNode("ModelName").InnerText;
                vm.PlayerName = node.SelectSingleNode("PlayerName").InnerText;
                vm.PrefabName = node.SelectSingleNode("PrefabName").InnerText;

                vm.Attack = float.Parse(node.SelectSingleNode("Attack").InnerText);
                vm.Defend = float.Parse(node.SelectSingleNode("Defend").InnerText);
                vm.MaxHP = float.Parse(node.SelectSingleNode("HP").InnerText);
                vm.CurHP = vm.MaxHP;
                vm.CurrentSpeed = float.Parse(node.SelectSingleNode("StartSpeed").InnerText);
                vm.Agility = float.Parse(node.SelectSingleNode("Agility").InnerText);
                vm.Resistance = float.Parse(node.SelectSingleNode("Resistance").InnerText);

                vm.MaxRotation = float.Parse(node.SelectSingleNode("MaxRotationSpeed").InnerText);
                vm.RotationSpeed = float.Parse(node.SelectSingleNode("RotationSpeed").InnerText);
                vm.AccelateFactor = float.Parse(node.SelectSingleNode("AccelateFactor").InnerText);

                vm.CurrentPosition = Vector3.zero;
                vm.CurrentRotation = Quaternion.identity;
                vm.CurrentDirection = Vector3.forward;

                _VMTable.Add(vm.ClassId, vm);
            }
        }
        public static void Log2(this PlayerViewModel vm)
        {
            Debug.Log("ModelName : " + vm.ModelName);
            Debug.Log("PlayerName : " + vm.PlayerName);
            Debug.Log("PrefabName : " + vm.PrefabName);

            Debug.Log("MaxRotationSpeed : " + vm.MaxRotation);
            Debug.Log("MaxSpeed : " + vm.MaxSpeed);
            Debug.Log("StartSpeed : " + vm.CurrentSpeed);

            Debug.Log("RotationSpeed : " + vm.RotationSpeed);
            Debug.Log("AccelateFactor : " + vm.AccelateFactor);

            Debug.Log("HP : " + vm.MaxHP);
            Debug.Log("Attack : " + vm.Attack);
            Debug.Log("Defend : " + vm.Defend);
            Debug.Log("Agility : " + vm.Agility);

            Debug.Log("ClassId : " + vm.ClassId);
        }

        public static void Log1(this PlayerViewModel vm)
        {
            Debug.Log("ModelName : " + vm.ModelName);
            Debug.Log("PlayerName : " + vm.PlayerName);
            Debug.Log("PrefabName : " + vm.PrefabName);

            Debug.Log("MaxRotationSpeed : " + vm.MaxRotation);
            Debug.Log("MaxSpeed : " + vm.MaxSpeed);
            Debug.Log("StartSpeed : " + vm.CurrentSpeed);

            Debug.Log("RotationSpeed : " + vm.RotationSpeed);
            Debug.Log("AccelateFactor : " + vm.AccelateFactor);

            Debug.Log("HP : " + vm.MaxHP);
            Debug.Log("Attack : " + vm.Attack);
            Debug.Log("Defend : " + vm.Defend);
            Debug.Log("Agility : " + vm.Agility);

            Debug.Log("ClassId : " + vm.ClassId);
        }
    }
}
