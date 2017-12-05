using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hero : MonoBehaviour
{
    /*#region Physics2D.OverlapCircle for ground detection
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    #endregion*/

	public Rigidbody2D rb;
	public Animator animator;

    #region Levels
    public GameObject lvl1;
    public GameObject lvl2;
    public GameObject lvl3;
    #endregion

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
//    Sprite closedChest;
//    [SerializeField]
//    Sprite openChest;

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

    public static Hero instance;

	void Awake()
	{
        instance = this;
		rb = this.GetComponent<Rigidbody2D> ();
        animator = this.GetComponent<Animator>();
    }

	void Start ()
	{
        maxHealth = 100;
        currentHealth = maxHealth;
        healthImage.fillAmount = (float)maxHealth / 100f;
        GetComponent<SpriteRenderer>().flipX = false;
        lvl2.SetActive (false);
        lvl3.SetActive (false);
	}

	void Update ()
	{
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

        if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
        {
            isJump = true;
        }

        if (currentHealth <= 0)
        {
            isDead = true;
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

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

        if (keyInt >= 1)
        {
            key.SetActive(true);
        }
        else if(keyInt == 0)
        {
            key.SetActive(false);
        }

        coinAmmountText.text = coinAmmount.ToString();
	}

	void FixedUpdate()
	{
        if (isDead == false)
        {
            move = Input.GetAxis("Horizontal");
        }
        else
        {
            return;
        }

		rb.velocity = new Vector2 (move * speed, rb.velocity.y);

		if (move > 0)
		{
			this.GetComponent<SpriteRenderer> ().flipX = false;
        }
        if (move < 0)
		{
			this.GetComponent<SpriteRenderer> ().flipX = true;
		}

        if (isJump)
        {
            Jump(jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            ThrowBomb();
        }

        //this.isGround = Physics2D.OverlapCircle(this.groundCheck.position, this.groundCheckRadius, this.whatIsGround);
        //this.isPlatform = Physics2D.OverlapCircle(this.groundCheck.position, this.groundCheckRadius, this.whatIsPlatform);
        //Debug.LogFormat("{0}, {1}, {2}, {3}", isGround, this.groundCheck.position, this.groundCheckRadius, this.whatIsGround);

        //animator.SetBool("isGround",  this.isGround);
        //animator.SetFloat("vSpeed", this.rb.velocity.y);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
        //This was replaced by Physics2D.OverlapCircle
//		if (col.gameObject.tag == "Ground")
//		{
//			this.isGround = true;
//		}

//        if (isPlatform == true)
//        {
//            this.transform.parent = col.transform;
//        }

        if (col.gameObject.tag == "Platform")
        {
            this.transform.parent = col.transform;
        }

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

	void OnCollisionExit2D(Collision2D col)
	{
        //This was replaced by Physics2D.OverlapCircle
//		if (col.gameObject.tag == "Ground")
//		{
//			this.isGround = false;
//		}

        if (col.gameObject.tag == "Platform")
        {
            this.transform.parent = null;
        }
	}

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
        if(col.gameObject.tag == "Coin")
        {
            coinAmmount++;
            Destroy(col.gameObject);
        }
    }

	void Jump(float force)
	{
		rb.AddForce (new Vector2 (0, force));
        isJump = false;
	}
        
    void Shoot()
    {
        GameObject bullet;
        bullet = (Instantiate(bulletPrefab, bulletRight.position, transform.rotation)) as GameObject;
        StartCoroutine(DestroyBullet(bullet));
        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(5000,200));
    }

    IEnumerator DestroyBullet(GameObject temp)
    {
        yield return new WaitForSeconds(2f);
        Destroy(temp.gameObject);
    }

    void ThrowBomb()
    {
        GameObject bomb;
        bomb = (Instantiate(bombPrefab, bombRight.position, transform.rotation)) as GameObject;
        StartCoroutine(DestroyBomb(bomb));
        bomb.GetComponent<Rigidbody2D>().AddForce(new Vector2(500,250));
    }

    IEnumerator DestroyBomb(GameObject temp)
    {
        yield return new WaitForSeconds(3f);
        Destroy(temp.gameObject);
    }

    void TakeDamage(int dmg)
    {
        currentHealth = currentHealth - dmg;
        CheckGameOver();
        healthImage.fillAmount = (float)currentHealth / 100;
    }

    void GiveHealth(int heal)
    {
        currentHealth = currentHealth + heal;
        healthImage.fillAmount = (float)currentHealth / 100;
        Destroy(GameObject.FindWithTag("Medkit"));
    }

    void CheckGameOver()
    {
        if (currentHealth <= 0)
        {
            animator.SetBool("isDead", true);
            animator.SetBool("isGround", true);
        }
    }

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
