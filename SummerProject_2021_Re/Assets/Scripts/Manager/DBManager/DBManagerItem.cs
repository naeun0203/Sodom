using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System;
using System.Linq;

[System.Serializable]
public struct ItemDB
{
    [SerializeField]
    public int ID;
    [SerializeField]
    public string name;
    [SerializeField]
    public float Item_Type, Hunger, Thirst, Heal, Fatigue, AD, Attack_Range, Capacity, Charge_Space;
}
[System.Serializable]
public struct Craft
{
    [SerializeField]
    public int ID;
    [SerializeField]
    public string name;
    [SerializeField]
    public int Necessary_Material_ID1, Amount_Of_Material1, Necessary_Material_ID2, Amount_Of_Material2,
                Necessary_Material_ID3, Amount_Of_Material3, Necessary_Object_ID;
}

public class DBManagerItem: DBManager
{
    [SerializeField]
    public ItemDB[] itemDB;
    public string[] item;

    [SerializeField]
    public Craft[] itemDBCraft;
    private string[] ItemCraft;

    public bool DataLoading = false;
    void Start()
    {
        StartCoroutine(ConnectItenDB());
    }

    private IEnumerator ConnectItenDB()
    {
        #region Use DBManager
        List<string> ItemDBList = new List<string>();
        Init("ItemDataFoodMedicine.php");
        yield return StartCoroutine(ConnectDB());
        ItemDBList.AddRange(queryResult.Split(';'));
        item = ItemDBList.ToArray(); 
        
        Init("ItemDataToolWeapon.php");
        yield return StartCoroutine(ConnectDB());
        ItemDBList.AddRange(queryResult.Split(';'));
        item = ItemDBList.ToArray();

        
        Init("ItemDataMaterialObject.php");
        yield return StartCoroutine(ConnectDB());
        ItemDBList.AddRange(queryResult.Split(';'));
        item = ItemDBList.ToArray();
        item = item.Where(x => !string.IsNullOrEmpty(x)).ToArray();
        #endregion
        for (int i = 0; i < itemDB.Length; i++)
        {
            itemDB[i].ID = Convert.ToInt32(GetDataValue(item[i], "ID:", "|"));
            itemDB[i].name = GetDataValue(item[i], "Name:", "|");
            itemDB[i].Item_Type = Convert.ToSingle(GetDataValue(item[i], "Item_Type:", "|"));
            if(itemDB[i].Item_Type == 0 || itemDB[i].Item_Type == 4)
            {
                itemDB[i].Hunger = Convert.ToSingle(GetDataValue(item[i], "Hunger:", "|"));
                itemDB[i].Thirst = Convert.ToSingle(GetDataValue(item[i], "Thirst:", "|"));
                itemDB[i].Heal = Convert.ToSingle(GetDataValue(item[i], "Heal:", "|"));
                itemDB[i].Fatigue = Convert.ToSingle(GetDataValue(item[i], "Fatigue:", "|"));
            }
            if(itemDB[i].Item_Type == 1 || itemDB[i].Item_Type == 2)
            {
                itemDB[i].AD = Convert.ToSingle(GetDataValue(item[i], "AD:", "|"));
                itemDB[i].Attack_Range = Convert.ToSingle(GetDataValue(item[i], "Attack_Range:", "|"));
            }
            if(itemDB[i].Item_Type == 5)
            {
                string Fatigue = GetDataValue(item[i], "Fatigue:", "|");
                string Capacity = GetDataValue(item[i], "Capacity:", "|");
                itemDB[i].Fatigue = (Fatigue == "") ? 0 : Convert.ToSingle(Fatigue);
                itemDB[i].Capacity = (Capacity == "") ? 0 : Convert.ToSingle(Capacity);
            }
            itemDB[i].Charge_Space = Convert.ToSingle(GetDataValue(item[i], "Charge_Space:", "|"));
        }

        #region CraftDB
        Init("ItemDataCraft.php");
        yield return StartCoroutine(ConnectDB());
        ItemCraft = queryResult.Split(';');
        for (int i = 0; i < itemDBCraft.Length; i++)
        {
            string Necessary_Material_ID2 = GetDataValue(ItemCraft[i], "Necessary_Material_ID2:", "|");
            string Amount_Of_Material2 = GetDataValue(ItemCraft[i], "Amount_Of_Material2:", "|");
            string Necessary_Material_ID3 = GetDataValue(ItemCraft[i], "Necessary_Material_ID3:", "|");
            string Amount_Of_Material3 = GetDataValue(ItemCraft[i], "Amount_Of_Material3:", "|");
            itemDBCraft[i].ID = Convert.ToInt32(GetDataValue(ItemCraft[i], "ID:", "|"));
            itemDBCraft[i].name = GetDataValue(ItemCraft[i], "Name:", "|");
            itemDBCraft[i].Necessary_Material_ID1 = Convert.ToInt32(GetDataValue(ItemCraft[i], "Necessary_Material_ID1:", "|"));
            itemDBCraft[i].Amount_Of_Material1 = Convert.ToInt32(GetDataValue(ItemCraft[i], "Amount_Of_Material1:", "|"));
            itemDBCraft[i].Necessary_Material_ID2 = (Necessary_Material_ID2 == "") ? 0 : Convert.ToInt32(Necessary_Material_ID2);
            itemDBCraft[i].Amount_Of_Material2 = (Amount_Of_Material2 == "") ? 0 : Convert.ToInt32(Amount_Of_Material2);
            itemDBCraft[i].Necessary_Material_ID3 = (Necessary_Material_ID3 == "") ? 0 : Convert.ToInt32(Necessary_Material_ID3);
            itemDBCraft[i].Amount_Of_Material3 = (Amount_Of_Material3 == "") ? 0 : Convert.ToInt32(Amount_Of_Material3);
            itemDBCraft[i].Necessary_Object_ID = Convert.ToInt32(GetDataValue(ItemCraft[i], "Necessary_Object_ID:", "|"));
        }
        #endregion
        DataLoading = true;
    }
}