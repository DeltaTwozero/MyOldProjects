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

    //Setting up merchandise and prices.
	void Start ()
    {
        qtyMagnet = 2;
        qtyGodmode = 1;

        priceMag = 15;
        priceGod = 30;
	}
    
    //Buy methods.
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

    //Open shop if player enters.
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isShop = true;
        }
    }

    //Close shop if player leaves.
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isShop = false;
            shopMenu.SetActive(false);
        }
    }

    //Open/close shop with a button.
    void Update()
    {
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
