using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Item_Base
{
    void Update()
    {
        if (useItem)
        {
            StartCoroutine(CheckUseItem());
        }

    }
    IEnumerator CheckUseItem()
    {
        switch (CurrentItemType)
        {
            case ItemType.Ingredient:

            case ItemType.Food:
                break;
            case ItemType.Medicine:
                break;
            case ItemType.Weapon:
                if (ItemEquip)
                {
                    ItemEquip = false;
                    Debug.Log("������ ��������");
                }
                else if (ItemEquip == false)
                {
                    ItemEquip = true;
                    Debug.Log("������ ����");
                }
                break;
            case ItemType.Tool:
                if (ItemEquip)
                {
                    ItemEquip = false;
                    Debug.Log("������ ��������");
                }
                else if (ItemEquip == false)
                {
                    ItemEquip = true;
                    Debug.Log("������ ����");
                }
                break;
        }
        useItem = false;
        yield return null;
    }
}
