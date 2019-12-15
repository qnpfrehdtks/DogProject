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

                _VMTable.Add(newVM.ClassId, newVM);
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
