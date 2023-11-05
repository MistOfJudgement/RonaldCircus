using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IHitter<EnemyController>
{
    public float damage = 1f;
    public bool isSwinging = false;
    public float swingDistance = 2f;
    public bool facingRight = true;
    public void DoHit(EnemyController hittable)
    {
        Debug.Log("Sword hit");
        hittable.behavior.TakeDamage((int)damage);
        hittable.OnHit();
    }
    private void Update()
    {
        if(!isSwinging)
        {
            CheckFacing();
        }
    }

    private void CheckFacing()
    {
        if(isSwinging)
        {
            return;
        }

        //if mouse is to the left of the player
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
        {
            //if player is facing right
            if (facingRight)
            {
                //flip player
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                transform.localPosition = new Vector3(-transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
                facingRight = false;
            }
        }
        else
        {
            //if player is facing left
            if (!facingRight)
            {
                //flip player
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                transform.localPosition = new Vector3(-transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
                facingRight = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Sword hit");
        if (other.gameObject.TryGetComponent(out EnemyController enemy))
        {
            DoHit(enemy);
        }
    }
    public void Swing()
    {
        if(isSwinging)
        {
            return;
        }
        isSwinging = true;
        StartCoroutine(SwingCoroutine());
    }
    public IEnumerator SwingCoroutine()
    {
        int direction = facingRight ? 1 : -1;
        float time = 0f;
        GetComponent<Collider2D>().enabled = true;
        while (time < 0.25f)
        {
            time += Time.deltaTime;
            transform.Rotate(0, 0, -360f * Time.deltaTime *direction);
            transform.Translate(swingDistance * Time.deltaTime*direction,0, 0);
            yield return null;
        }
        //rotate back
        GetComponent<Collider2D>().enabled = false;
        yield return null;
        while (time < 0.5f)
        {
            time += Time.deltaTime;
            transform.Rotate(0, 0, 360f * Time.deltaTime * direction);
            transform.Translate(-swingDistance * Time.deltaTime * direction,0, 0);
            yield return null;
        }
        isSwinging = false;
    }
}
