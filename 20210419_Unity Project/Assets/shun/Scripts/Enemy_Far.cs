using UnityEngine;
using System.Collections;

public class Enemy_Far : Enemy

{

    [Header("子彈")]
    public GameObject bullet;

    private Vector3 bulletpos; // 子彈位置


    protected override void Attack()
    {
        base.Attack();
		Debug.Log("敵人攻擊02");
		StartCoroutine(Createbullet());
	}

	/// <summary>
	/// 生成子彈
	/// </summary>
	/// <returns></returns>
	private IEnumerator Createbullet()
	{
		yield return new WaitForSeconds(data.atkdelay);
		bulletpos = transform.position ;
		GameObject temp = Instantiate(bullet, bulletpos, transform.rotation);
		Debug.Log("面相" + face);
		temp.GetComponent<Rigidbody2D>().AddForce(-1*transform.right  * data.bulletspeed* 1.5f);//增加向前推力
		temp.AddComponent<Bullet>();
		Debug.Log("傷害" + data.atk);
		temp.GetComponent<Bullet>().damage = data.atk;
		temp.GetComponent<Bullet>().player = false;
		Destroy(temp, 5);
	}

	protected override void OnDrawGizmos()
	{
		base.OnDrawGizmos();

		Gizmos.color = Color.blue;

		Gizmos.DrawLine((Vector2)transform.position, (Vector2)transform.position + (new Vector2(face, 0)) * data.atkLength);

	}
}