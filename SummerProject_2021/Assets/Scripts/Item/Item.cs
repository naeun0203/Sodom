using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    #region DB
    [SerializeField]
    public Craft[] itemCraft;

    public enum ItemTypeEnum { Material, Food, Medicine, Tool, Weapon, Product };
    public enum ItemName
    {
        Wood, Stone, Cloth, Iron,
        Brown_Water, Water, Raw_Meat, Fried_Meat, Vegetable, Vegetable_Soup, Bandage, Pill,
        Crowbar, Shovel, Dagger, Sword,
        Bonfire, Bed, Bag, Water_Purifier, Box
    };

    public ItemName CurrentItem = ItemName.Wood;
    public ItemTypeEnum CurrentItemType = ItemTypeEnum.Material;
    public float ID, ItemType, Hunger, Thirst, Heal, Fatigue, AD, Attack_Range, Capacity, Charge_Space;
    public Sprite itemImage;
    private DBManagerItem itemData;
    // Start is called before the first frame update
    public bool useItem = false;
    public bool ItemEquip = false;
    private bool dataLoading;
    #endregion
    public int ItemCount;
    private CharacterValue playerValue;
    void Start()
    {
        playerValue = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterValue>();
        itemData = GameObject.Find("DBManager").GetComponent<DBManagerItem>();
    }
    void Update()
    {
        if (useItem)
        {
            CheckUseItem();
        }
        if (itemData.DataLoading)
        {
            DataSet();
        }
    }
    private void DataSet()
    {
        for (int i = 0; i < itemData.itemDB.Length; i++)
        {
            if (CurrentItem.ToString() == itemData.itemDB[i].name)
            {
                ID = itemData.itemDB[i].ID;
                ItemType = itemData.itemDB[i].Item_Type;
                Hunger = itemData.itemDB[i].Hunger;
                Thirst = itemData.itemDB[i].Thirst;
                Heal = itemData.itemDB[i].Heal;
                Fatigue = itemData.itemDB[i].Fatigue;
                AD = itemData.itemDB[i].AD;
                Attack_Range = itemData.itemDB[i].Attack_Range;
                Capacity = itemData.itemDB[i].Capacity;
                Charge_Space = itemData.itemDB[i].Charge_Space;
                break;
            }
        }

        for (int i = 0; i < itemData.itemDBCraft.Length; i++)
        {
            if (itemData.itemDBCraft[i].ID == ID)
            {
                itemCraft[0] = itemData.itemDBCraft[i];
                break;
            }
        }
    }
    private void CheckUseItem()
    {
        if (ItemType == 0 || ItemType == 4)
        {
            playerValue.HpChanged(Heal);
            playerValue.HungerChanged(Hunger);
            playerValue.ThirstChanged(Thirst);
            playerValue.FatigueChanged(Fatigue);
        }
        else if (ItemType == 1 || ItemType == 2)
        {
            if (ItemEquip)
            {
                playerValue.Atk_PowChanged(-AD);
                playerValue.Atk_RangeChanged(-Attack_Range);
                ItemEquip = false;
                Debug.Log("������ ��������");
            }
            else if (ItemEquip == false)
            {
                playerValue.Atk_PowChanged(AD);
                playerValue.Atk_RangeChanged(Attack_Range);
                ItemEquip = true;
                Debug.Log("������ ����");
            }
        }
        useItem = false;
    }
}
