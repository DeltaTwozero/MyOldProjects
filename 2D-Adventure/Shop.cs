using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour
{
    [SerializeField]
    int qtyMagnet;
    [SerializeField]
    int qtyGodmode;

    [SerializeField]
    int priceMag;
    [SerializeField]
    int priceGod;

    [SerializeField]
    GameObject shopMenu;
    private bool isShop;

	void Start ()
    {
        qtyMagnet = 2;
        qtyGodmode = 1;

        priceMag = 15;
        priceGod = 30;
	}
        
    public void BuyGodmode()
    {
        if (Hero.instance.godInt == 0 && qtyGodmode > 0 && Hero.instance.coinAmmount >= priceGod)
        {
            qtyGodmode--;
            Hero.instance.godInt++;
            Hero.instance.coinAmmount -= priceGod;
        }
    }

    public void BuyMagnet()
    {
        if (Hero.instance.magInt == 0 && qtyMagnet > 0 && Hero.instance.coinAmmount >= priceMag)
        {
            qtyMagnet--;
            Hero.instance.magInt++;
            Hero.instance.coinAmmount -= priceMag;
        }
    }

    public void CloseShop()
    {
        shopMenu.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isShop = true;
        }
//
//        if (isShop && shopMenu.active == true)
//        {
//            shopMenu.SetActive(false);
//        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isShop = false;
            shopMenu.SetActive(false);
        }
    }

    void Update()
    {
//        Debug.Log(isShop);
//        Debug.Log(shopMenu.activeSelf);

        if (isShop)
        {
            if (Input.GetKeyDown(KeyCode.X))
                shopMenu.SetActive(true);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.X))
                shopMenu.SetActive(false);
        }
    }
}
