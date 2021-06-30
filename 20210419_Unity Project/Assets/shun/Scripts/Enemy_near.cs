using System.Collections;
using UnityEngine;

public class Enemy_near : Enemy
{

    protected override void Attack()
    {
        base.Attack();
        StartCoroutine(AttackDelay());
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(data.atkdelay);
        RaycastHit2D hit = Physics2D.Raycast(transform.position,-1* transform.right,  data.atkLength, 1 << 9);
        
        if (hit.collider)
        {
            hit.collider.GetComponent<Player>().Hurt((int)data.atk);
            Debug.Log(hit.transform.name);
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.blue;

        Gizmos.DrawLine((Vector2)transform.position, (Vector2)transform.position + (new Vector2(face, 0)) * data.atkLength);
     
    }
}
