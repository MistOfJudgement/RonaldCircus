using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerController playerController;
    public float speed = 5;

    private bool m_isDodgeRolling = false;
    public bool IsDodgeRolling { get { return m_isDodgeRolling; } }
    public float dodgeRollBurstSpeed = 15f;
    private float dodgeRollCurrentSpeed = 0f;
    public float dodgeRollTime = 0.75f;
    public float dodgeRollInvulnerabilityTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsDodgeRolling)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(DodgeRoll());
        }
        Move();
    }

    void Move()
    {

        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = movement.normalized * speed;
    }

    IEnumerator DodgeRoll()
    {
        float timer = dodgeRollTime;
        m_isDodgeRolling = true;
        dodgeRollCurrentSpeed = dodgeRollBurstSpeed;
        playerController.GrantInvulerability(dodgeRollInvulnerabilityTime);
        while (timer > 0)
        {
            Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            rb.velocity = movement.normalized * dodgeRollCurrentSpeed;
            dodgeRollCurrentSpeed -= dodgeRollBurstSpeed * Time.deltaTime / dodgeRollTime;
            timer -= Time.deltaTime;
            yield return null;
        }
        m_isDodgeRolling = false;

    }

}
