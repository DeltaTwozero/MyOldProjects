using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hero : MonoBehaviour
{
    //Creating variables for player.
	public Rigidbody2D rb;
	public Animator animator;

    [SerializeField]
    GameObject magnet;
    [SerializeField]
    GameObject key;
    [SerializeField]
    GameObject kappa;
    [SerializeField]
    Image healthImage;
    [SerializeField]
    Text coinAmmountText;
    public int coinAmmount;

    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    GameObject bombPrefab;
    [SerializeField]
    Transform bulletRight;
    [SerializeField]
    Transform bombRight;


    [SerializeField]
    GameObject coinsPrefab;
    [SerializeField]
    Transform coinsSpawn;
    [SerializeField]

	public float speed = 30f;
	float move;
    
    public bool isGround;
    public bool isPlatform;
	public bool isJump;
    public bool isDead;
    public bool isMelee;
    public bool isDMG;
    public bool isMagActive;
    public bool isGodmode;

	public float jumpForce;

    public int currentHealth;
    public int maxHealth;
    public int random;
    public int magInt;
    public int godInt;
    public int keyInt;

    //Creating insctance of this code so I could access it's variables from another code.
    public static Hero instance;

	void Awake()
	{
        instance = this;
		rb = this.GetComponent<Rigidbody2D> ();
        animator = this.GetComponent<Animator>();
    }

    //Setting up variables.
	void Start ()
	{
        maxHealth = 100;
        currentHealth = maxHealth;
        //UI's image is displayed according to hp.
        healthImage.fillAmount = (float)maxHealth / 100f;
        GetComponent<SpriteRenderer>().flipX = false;
	}

	void Update ()
	{
        //Changing animations.
        if (Mathf.Abs(move) > 0)
        {
            animator.SetBool("isRun", true);
        }
        else if (Mathf.Abs(move) == 0)
        {
            animator.SetBool("isRun", false);

            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                Shoot();
                animator.SetBool("isShoot", true);
            }
            if (Input.GetKeyUp(KeyCode.KeypadEnter))
            {
                animator.SetBool("isShoot", false);
            }

            if (Input.GetKey(KeyCode.V))
            {
                isMelee = true;
                animator.SetBool("isMelee", true);
            }
            if (Input.GetKeyUp(KeyCode.V))
            {
                isMelee = false;
                animator.SetBool("isMelee", false);
            }

        }

        //Power Up's activation.
        if (Input.GetKeyDown(KeyCode.R) && magInt == 1)
        {
            magInt--;
            isMagActive = true;
            StartCoroutine(ActivateMagnet());
        }

        if (Input.GetKeyDown(KeyCode.I) && godInt == 1)
        {
            godInt--;
            isGodmode = true;
            StartCoroutine(ActivateGodmode());
        }

        //Making my player jump.
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
        {
            isJump = true;
        }

        //Checking if dead.
        if (currentHealth <= 0)
        {
            isDead = true;
        }

        //Check to prevent overhealing.
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        //I'm activating power ups if their quantity is = 1.
        if (magInt == 1)
        {
            magnet.SetActive(true);
        }
        else if(magInt == 0)
        {
            magnet.SetActive(false);
        }

        if (godInt == 1)
        {
            kappa.SetActive(true);
        }
        else if(godInt == 0)
        {
            kappa.SetActive(false);
        }

        //Key is needed for opening next level.
        if (keyInt >= 1)
        {
            key.SetActive(true);
        }
        else if(keyInt == 0)
        {
            key.SetActive(false);
        }

        //Displaying ammount of coins in UI.
        coinAmmountText.text = coinAmmount.ToString();
	}

	void FixedUpdate()
	{
        //If player is dead I'm dissabling all controlls.
        if (isDead == false)
        {
            move = Input.GetAxis("Horizontal");
        }
        else
        {
            return;
        }

		rb.velocity = new Vector2 (move * speed, rb.velocity.y);

        //changing sprites from right to left and vice versa.
		if (move > 0)
		{
			this.GetComponent<SpriteRenderer> ().flipX = false;
        }
        if (move < 0)
		{
			this.GetComponent<SpriteRenderer> ().flipX = true;
		}

        //checking for button. If true then method is used.
        if (isJump)
        {
            Jump(jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            ThrowBomb();
        }
	}

    //Checking for a various collisions. Taking accoding actions.
	void OnCollisionEnter2D(Collision2D col)
	{
        //Making sure that player moves with platform.
        if (col.gameObject.tag == "Platform")
        {
            this.transform.parent = col.transform;
        }

        //Checking for immortality.
        if (isGodmode == false)
        {
            if (col.gameObject.tag == "Arrow")
            {
                TakeDamage(10);
            }

            if (col.gameObject.tag == "Enemy")
            {
                TakeDamage(30);
            }
        }

        //Making money.
        if (col.gameObject.tag == "Coin")
        {
            coinAmmount++;
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "Medkit")
        {
            GiveHealth(20);
        }
	}

    //Checking if player left platform.
	void OnCollisionExit2D(Collision2D col)
	{
        if (col.gameObject.tag == "Platform")
        {
            this.transform.parent = null;
        }
	}

    //Player is constantly takes damage near enemies.
    void OnTriggerStay2D(Collider2D col)
    {
        if (isGodmode == false)
        {
            if (col.gameObject.tag == "Enemy")
            {
                TakeDamage(1);
            }
        }
    }

    //This part was implemented in order to teleport player to another level.
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Key")
        {
            keyInt++;
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "Lock" && keyInt >= 1 && lvl1.activeSelf && !lvl2.activeSelf && !lvl3.activeSelf)
        {
            keyInt = 0;
            lvl1.SetActive(false);
            lvl2.SetActive(true);
        }
        else if (col.gameObject.tag == "Lock" && keyInt == 1 && !lvl1.activeSelf && lvl2.activeSelf && !lvl3.activeSelf)
        {
            lvl2.SetActive(false);
            lvl3.SetActive(true);
        }
        //Making money.
        if(col.gameObject.tag == "Coin")
        {
            coinAmmount++;
            Destroy(col.gameObject);
        }
    }

    //Height of the jump.
	void Jump(float force)
	{
		rb.AddForce (new Vector2 (0, force));
        isJump = false;
	}

    //Creating bullet.
    void Shoot()
    {
        GameObject bullet;
        bullet = (Instantiate(bulletPrefab, bulletRight.position, transform.rotation)) as GameObject;
        StartCoroutine(DestroyBullet(bullet));
        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(5000,200));
    }

    //Killing bullet in 2 second if it hits nothing.
    IEnumerator DestroyBullet(GameObject temp)
    {
        yield return new WaitForSeconds(2f);
        Destroy(temp.gameObject);
    }

    //Throwing bomb.
    void ThrowBomb()
    {
        GameObject bomb;
        bomb = (Instantiate(bombPrefab, bombRight.position, transform.rotation)) as GameObject;
        StartCoroutine(DestroyBomb(bomb));
        bomb.GetComponent<Rigidbody2D>().AddForce(new Vector2(500,250));
    }

    //Destroying bomb.
    IEnumerator DestroyBomb(GameObject temp)
    {
        yield return new WaitForSeconds(3f);
        Destroy(temp.gameObject);
    }

    //Taking damage and checking if player is dead.
    void TakeDamage(int dmg)
    {
        currentHealth = currentHealth - dmg;
        CheckGameOver();
        healthImage.fillAmount = (float)currentHealth / 100;
    }

    //I NEED HEALING starts here.
    void GiveHealth(int heal)
    {
        currentHealth = currentHealth + heal;
        healthImage.fillAmount = (float)currentHealth / 100;
        Destroy(GameObject.FindWithTag("Medkit"));
    }

    //Changing animations to dead.
    void CheckGameOver()
    {
        if (currentHealth <= 0)
        {
            animator.SetBool("isDead", true);
            animator.SetBool("isGround", true);
        }
    }

    //Power Ups.
    IEnumerator ActivateMagnet()
    {
        yield return new WaitForSeconds(2f);
        isMagActive = false;
    }

    IEnumerator ActivateGodmode()
    {
        yield return new WaitForSeconds(5f);
        isGodmode = false;
    }
}
