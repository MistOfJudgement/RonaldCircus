using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LegionareBehavior : MonoBehaviour, IEnemyBehavior
{
    private GameObject player;
    private Rigidbody2D rb;
    public GameObject pilum;
    private PilumStab pilumStab;
    public  GameObject shield;

    public Sprite normalShield;
    public Sprite deflectedShieldd;
    private enum State { Approaching, Aim, Attack, Retreat}
    private State currentState = State.Approaching;
    public float speed = 3;
    public float stabRadius = 2f;
    public float retreatRadius = 3f;
    public float stabDistance = 1f;
    public float aimTime = 1f;
    public float attackTime = 1f;
    public float retreatTime = 1f;
    private bool m_isBlocking = true;
    public float blockTime = 2f;
    private Vector3 initPilumPos;
    private Vector3 initPilumRot;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        //StartCoroutine(Approach());
        pilumStab = pilum.GetComponent<PilumStab>();
        initPilumPos = pilum.transform.localPosition;
        initPilumRot = pilum.transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Approach()
    {
        while(Vector2.Distance(transform.position, player.transform.position) > stabRadius)
        {
            Vector2 direction = player.transform.position - transform.position;
            
            rb.velocity = direction.normalized * speed;

            yield return null;
        }
        currentState = State.Aim;
        StartCoroutine(Aim());
    }

    IEnumerator Aim()
    {
        float timer = aimTime;
        rb.velocity = Vector2.zero;
        while(timer > 0)
        {
            timer -= Time.deltaTime;
            pilum.transform.up = Vector2.Lerp(pilum.transform.up, player.transform.position - transform.position, aimTime-timer);
            yield return null;
        }
        
        currentState = State.Attack;
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        float timer = attackTime;
        pilumStab.enabled = true;
        while(timer > 0)
        {
            timer -= Time.deltaTime;
            pilum.transform.localPosition = (Vector2.Lerp(initPilumPos, pilum.transform.up * stabDistance, (attackTime-timer)/attackTime));
            yield return null;
        }
        pilumStab.enabled = false;
        StartCoroutine(ReturnPilum(initPilumPos));
    }
    IEnumerator ReturnPilum(Vector2 origin)
    {
        yield return new WaitForSeconds(0.5f);
        float timer = attackTime;
        while(timer > 0)
        {
            timer -= Time.deltaTime;
            pilum.transform.localPosition = (Vector2.Lerp(pilum.transform.localPosition, origin, (attackTime - timer) / attackTime));
            pilum.transform.localRotation = Quaternion.Lerp(pilum.transform.localRotation, Quaternion.Euler(initPilumRot), (attackTime - timer) / attackTime);
            yield return null;
        }
        pilum.transform.localPosition = origin;
        pilum.transform.localRotation = Quaternion.Euler(initPilumRot);
        currentState = State.Retreat;
        StartCoroutine(Retreat());
    }
    IEnumerator Retreat()
    {
        float timer = retreatTime;
        while(timer > 0 && Vector2.Distance(transform.position, player.transform.position) < retreatRadius)
        {
            timer -= Time.deltaTime;
            //pilum.transform.Translate(Vector2.Lerp(pilum.transform.position, Vector2.zero, retreatTime-timer));
            yield return null;
        }
        //pilum.transform.position = Vector2.zero;
        currentState = State.Approaching;
        StartCoroutine(Approach());
    }

    public void StartBehavior()
    {

        player = player != null ? player : PlayerController.instance.gameObject;
        rb = rb != null ? rb : GetComponent<Rigidbody2D>();
        enabled = true;
        StartCoroutine(Approach());
    }

    public void StopBehavior()
    {
        currentState = State.Approaching;
        StopAllCoroutines();
        this.enabled = false;
    }

    public void TakeDamage(int damage)
    {
        if (m_isBlocking)
        {
            StartCoroutine(DoBlock());
        }
        else
        {
            GetComponent<EnemyHealth>().CurrentHealth -= (damage);
        }
    }

    IEnumerator DoBlock()
    {
        m_isBlocking = false;
        SpriteRenderer sprite = shield.GetComponent<SpriteRenderer>();
        sprite.sprite = deflectedShieldd;
        sprite.transform.localPosition = new Vector3(0.4f, 0, 0);
        sprite.sortingOrder = 1;
        yield return new WaitForSeconds(blockTime);
        sprite.sprite = normalShield;
        sprite.transform.localPosition = new Vector3(0, 0, 0);
        sprite.sortingOrder = 10;
        m_isBlocking = true;

    }
}
