using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductItem : MonoBehaviour
{
    public GameObject ProduceButton;
    private bool product;
    public bool Product
    {
        get { return product; }
        set
        {
            product = value;
            if(product) { this.gameObject.GetComponent<SpriteRenderer>().sprite = itemData.itemDB[item.ID-1].ItemImage; }
        }
    }
    private bool useItem;
    public bool UseItem
    {
        get { return useItem; }
        set 
        { 
            useItem = value;
            if(useItem) { StartCoroutine(CheckItem()); }
        }
    }
    private Item item;
    private ItemCraft Water;
    private GameObject box;
    private GameObject bonfire;
    private float distance;
    private GameObject Player;
    private DBManagerItem itemData;
    private void Start()
    {
        item = this.gameObject.GetComponent<Item>();
        Water = GameObject.Find("Water").GetComponent<ItemCraft>();
        box = GameObject.Find("MainUI").transform.Find("Box").gameObject;
        bonfire = GameObject.Find("MainUI").transform.Find("Bonfire").gameObject;
        Player = GameObject.FindWithTag("Player");
        itemData = GameObject.FindGameObjectWithTag("Manager").GetComponent<DBManagerItem>();
    }
    private void Update()
    {
        distance = Vector2.Distance(Player.transform.position, transform.position);
        if (distance < 200)
        {
            ProduceButton.SetActive(true);
        }
        else
        {
            ProduceButton.SetActive(false);
        }
    }
    IEnumerator CheckItem()
    {
        while(UseItem)
        {
            switch(item.ID)
            {
                case 19:
                    Water.CraftItem = true;
                    break;
                case 20:
                    bonfire.SetActive(true);
                    break;
                case 21:
                    box.SetActive(true);
                    break;
                case 22:
                    break;
            }
            UseItem = false;
            yield return null;
        }
    }
}
