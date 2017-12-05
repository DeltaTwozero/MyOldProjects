using UnityEngine;
using System.Collections;

public class TrasureChest : MonoBehaviour
{
    [SerializeField]
    Sprite closedChest;
    [SerializeField]
    Sprite openChest;

    [SerializeField]
    GameObject coinsPrefab;
    [SerializeField]
    Transform coinsSpawn;
    int random;

	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.V) && this.GetComponent<SpriteRenderer>().sprite == closedChest)
        {
            this.GetComponent<SpriteRenderer>().sprite = openChest;
            for (int i = 0; i < 5; i++)
            {
                Chest();
            }
        }
    }

    void Chest()
    {
        random = Random.Range(-300, 300);
        GameObject coins;
        coins = (Instantiate(coinsPrefab, coinsSpawn.position, transform.rotation)) as GameObject;
        coins.GetComponent<CircleCollider2D>().isTrigger = true;
        coins.GetComponent<Rigidbody2D>().AddForce(new Vector2(random,500));
        StartCoroutine(DisableTrigger(coins));
    }

    IEnumerator DisableTrigger(GameObject go)
    {
        yield return new WaitForSeconds(.5f);
        go.GetComponent<CircleCollider2D>().isTrigger = false;
    }
}
