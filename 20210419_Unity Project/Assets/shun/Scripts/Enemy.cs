using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData data;              //敵人資料
    
    private Animator ani;               //動畫

    //////////////////////////////////////////////////////////////// 
    private float curhp;


    ////////////////////////////////////////////////////////////////
    

    private Rigidbody2D rig;//剛體
    [Header("Layers")]
    private LayerMask playerLayer;//用来開啟layer
    [Header("Collision")]
    private Collider2D coll;//碰撞器
    private Collider2D playerColl;

    private GameObject player;

    private float Timer; //計時
    private bool onHurt;
    Vector2 beg;//射線起点
    Vector2 down = new Vector2(0, -1);//控制射線角度的向量


    public float face;//朝向
    private float dis;

    private void Start()
    {
        curhp = data.hp;
        ani = GetComponent<Animator>();       
        rig = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        player = GameObject.FindWithTag("Player");
        Debug.Log(player.layer);
        face = -1;
        playerLayer = 1 << 9;
        Timer = data.cd - 0.5f;
        onHurt = false;
    }

    


    void FixedUpdate()
    {
        Debug.Log("face" + face);
        beg = transform.position;
        playerColl = IsPlayerview();
        if (!onHurt) //沒有受傷才能執行其他動作
        {
            dis = Vector2.Distance(player.transform.position, transform.position);
            if (IsBorder())//是否到邊緣
            {
                //Debug.Log("hello");

                rig.velocity = new Vector2(0, 0);
                AccordingDirectionFlip(playerColl);
                Flip();
            }
            else //不到邊缘就可以移動
            {
                AccordingDirectionFlip(playerColl);
                Timer += Time.deltaTime;
                if (dis > data.stopdis)
                {
                    Move();
                }
                else
                {
                    rig.velocity = new Vector2(0, 0);
                    if (Timer > data.cd)
                    {
                        Attack();
                        Timer = 0;
                    }
                }

            }
        }
        
    }

    
    private void AccordingDirectionFlip(Collider2D playerColl)//根据玩家是否出现在视野中，安排敌人转向
    {
        if (playerColl != null)//如果玩家出现視野中
        {
            int direction;
            if (playerColl.transform.position.x < transform.position.x)
            {
                direction = -1;//玩家在敵人的左邊
            }
            else
            {
                direction = 1;//玩家在敵人的右邊
            }
            if (direction != face)//表示方向不一致
            {
               // Debug.Log("direction" + direction);
                //Debug.Log("face" + face);
                Flip();
            }
            
        }
    }
    void Flip()//翻轉角色方向
    {
        face = (face == 1) ? -1 : 1;
        if (face == -1)
            transform.eulerAngles = Vector3.zero;//*-1是跟原始方向是朝左邊
        else
            transform.eulerAngles = Vector3.up*(-180);

        
    }


    void Move()//左右移動
    {
        rig.velocity = new Vector2(face * data.speed * Time.deltaTime, rig.velocity.y);//输入x，y向量，数值*方向
    }


    bool IsBorder()//判斷到達盡頭
    {
        
        Debug.DrawLine(beg, beg + (new Vector2(face, 0) + down) * data.radialLength, Color.red);
        if (!Physics2D.Raycast(beg, new Vector2(face, 0) + down, data.radialLength, LayerMask.GetMask("Ground")))//碰到邊界
        {
            return true ;
        }
        return false;
    }

    Collider2D IsPlayerview()
    {

        return Physics2D.OverlapCircle(transform.position, data.collisionRadius, playerLayer);
    }

    protected virtual void Attack()
    {
        
        ani.SetTrigger("攻擊觸發");
    }
    public void Hurt(int damage)      //受傷
    {
        if (ani.GetBool("死亡開關")) return;
        curhp -= damage;
        onHurt = true;
        rig.AddForce(new Vector2( -1*face*data.moveForce , data.jumpForce ));
        //rig.velocity = new Vector2(face * data.moveForce * Time.deltaTime, data.jumpForce * Time.deltaTime);
        StartCoroutine(hitFlash());
        

        if (curhp <= 0) Dead();
    }

    public IEnumerator hitFlash()
    {
        
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.2f);
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.3f);
        onHurt = false;
    }


    private void Dead()         //死亡
    {
        ani.SetBool("死亡開關", true);
        rig.velocity = new Vector2(0, 0);
        enabled = false;    //這個腳本停用
        coll.enabled = false;
        Destroy(gameObject, 3);
    }



    private void DropItem()
    {

    }

    /// <summary>
    /// 繪製圖示
    /// </summary>
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position, data.collisionRadius);//畫圓
        Gizmos.DrawLine((Vector2)transform.position, (Vector2)transform.position + (new Vector2(face, 0) + down) * data.radialLength);//射線

    }

}
