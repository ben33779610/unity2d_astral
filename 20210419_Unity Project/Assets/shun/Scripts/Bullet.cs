using UnityEngine;

public class Bullet : MonoBehaviour
{

    /// <summary>
    /// 子彈的傷害
    /// </summary>
    public float damage;
    [Header("判斷是否為玩家射出")]
    public bool player;
    private void Start()
    {
		Debug.Log("傷害" + damage);

		Debug.Log(player);


	}


/*	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("攻擊玩家:" + damage);
		if (!player && collision.gameObject.tag == "Player" && collision.gameObject.GetComponent <Player>())
		{
			Debug.Log("攻擊玩家:" + damage);
			collision.gameObject.GetComponent<Player>().Hurt((int)damage);
			Destroy(gameObject);
		}
		else
		{
			Debug.Log("00000" );
			Destroy(gameObject);
		}
	}*/
	private void OnTriggerEnter2D(Collider2D collider)
	{
		Debug.Log("攻擊玩家:" + damage);
		if (!player && collider.gameObject.tag == "Player" && collider.gameObject.GetComponent<Player>())
		{
			Debug.Log("攻擊玩家:" + damage);
			collider.gameObject.GetComponent<Player>().Hurt((int)damage);
			Destroy(gameObject);
		}
		else if(collider.gameObject.tag != "Enemy")
		{

			Destroy(gameObject);
		}
        else
        {
			
		}
	}
}
