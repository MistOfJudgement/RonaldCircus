using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour, IHittable
{
    public static PlayerController instance;
    public PlayerMovement playerMovement;
    public PlayerHealth playerHealth;

    private Rigidbody2D rb;

    public float InvulnerabilityTime = 1f;
    private float invulnerabilityTimer = 0f;

    public bool isInvulnerable = false;
    public GameObject bulletPrefab;
    private Camera mainCamera;

    public Transform gunHolder;
    public Transform gunSprite;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this; // this is the current instance of the script
        }
        else
        {
            //there can only be one
            Destroy(gameObject);
        }

        playerMovement = GetComponent<PlayerMovement>();
        playerHealth = GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(invulnerabilityTimer > 0)
        {
            invulnerabilityTimer -= Time.deltaTime;
            if(invulnerabilityTimer <= 0)
            {
                isInvulnerable = false;
            }
        }

        if (playerMovement.IsDodgeRolling)
        {
            return;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            FireBullet();
        }

        //Stuff for making the player aim at the cursor
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure the z-component is at the same level as the player (0 in 2D games).

        // Calculate the direction to aim
        Vector3 aimDirection = (mousePosition - transform.position).normalized;

        // Rotate the player to aim at the cursor
        gunHolder.up = aimDirection;   
    }

    
    private void FireBullet()
    {
        //Quaternion quaternion = Quaternion.EulerRotation(aimStorage.x, aimStorage.y, 0);
        GameObject bullet = Instantiate(bulletPrefab, gunSprite.position, gunHolder.rotation);
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (rb == null)
    //    {
    //        return;
    //    }
        
    //    if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //        HandleHit(collision.gameObject);
    //    }
    //}
    //public void HandleHit(GameObject enemy) {
    //    if(isInvulnerable)
    //    {
    //        return;
    //    }
    //    playerHealth.CurrentHealth -= 5;
    //    //invulnerabilityTimer = InvulnerabilityTime;
    //    //StartCoroutine(InvulnerabilityFlash());
    //    GrantInvulerability(InvulnerabilityTime);
    //}
    public void GrantInvulerability(float time)
    {
        invulnerabilityTimer = time;
        isInvulnerable = true;
        StartCoroutine(InvulnerabilityFlash());
    }
    IEnumerator InvulnerabilityFlash()
    {
        while (invulnerabilityTimer > 0)
        {
            //GetComponent<SpriteRenderer>().enabled = false;
            //yield return new WaitForSeconds(0.1f);
            //GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(0.1f);
        }
        GetComponent<SpriteRenderer>().color = Color.white;
        yield return null;
    }
    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
        {
            return;
        }
        playerHealth.CurrentHealth -= damage;
        GrantInvulerability(InvulnerabilityTime);
    }
    
    public void OnHit()
    {

    }
}
