using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("玩家資料")]
    public PlayerData Data; 

    public Camera cam;
    [Header("玩家是否在地面")]
    private bool grouded;
    [Header("角色的動畫控制器")]
    private Animator anim;

    private int A_face = 1; //預設面向右邊

    public hpValueManager hpvaluemanger;
    public mpValueManager mpvaluemanger;
    private int curhp;


    // double the spin speed when true
    private bool fastSpin = false;


    private Rigidbody2D playerRigidbody2D;
    [Header("水平速度")]
    public float speedX;
    [Header("水平方向")]
    private float horizontaDirection;
    [Header("最大水平速度")]
    public float maxSpeedX;
    


    const string HORIZONTAL = "Horizontal";

    

    float speedY;

    
    /// <summary>
    /// 跳躍鍵
    /// </summary>
    public bool JumpKey
    {
        get
        {
            return Input.GetKeyDown(KeyCode.X);
        }
    }

    public bool AttackKey
    {
        get
        {
            return Input.GetKeyDown(KeyCode.Z);
        }
    }

    /// <summary>
    /// 技能鍵
    /// </summary>
    public bool SkillKey01
    {
        get
        {
            return Input.GetKeyDown(KeyCode.Alpha1);
        }
    }

    void TryJump()
    {
        if (grouded && JumpKey)
        {
            playerRigidbody2D.gravityScale = 1;
            playerRigidbody2D.Sleep();    //讓剛體重置
            playerRigidbody2D.AddForce(new Vector2(0, Data.yForce) );
            
        }
        
    }



    

    private void OnCollisionEnter2D(Collision2D selfbody)
    {
        //Debug.Log(gameObject.tag);
        if (selfbody.gameObject.tag == "Ground")
        {
            
            grouded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D selfbodyexit)
    {
        if (selfbodyexit.gameObject.tag == "Ground")
            grouded = false;
    }




    /*    bool IsGround
        {
            get
            {
                Vector2 start = groundCheck.position;
                Vector2 end = new Vector2(start.x, start.y - distance);
                Debug.DrawLine(start, end, Color.blue);
                grouded = Physics2D.Linecast(start, end, groundLayer);
                return grouded;
            }
        }*/

    public bool FastSpin { get => fastSpin; set => fastSpin = value; }

    public void ControlSpeed()
    {
        speedX = playerRigidbody2D.velocity.x;
        speedY = playerRigidbody2D.velocity.y;
        float newSpeedX = Mathf.Clamp(speedX, -maxSpeedX, maxSpeedX);
        playerRigidbody2D.velocity = new Vector2(newSpeedX, speedY);

    }







    private void MovementX()
    {
        horizontaDirection = Input.GetAxis(HORIZONTAL);
        if (Input.GetAxis(HORIZONTAL) > 0) Flip(1);
        else if (Input.GetAxis(HORIZONTAL) < 0) Flip(-1);
        

        

        playerRigidbody2D.AddForce (new Vector2(Data.xForce * horizontaDirection, 0));

        //playerRigidbody2D.transform.position = new Vector3(Data.xForce * horizontaDirection, 0, 0);

        playerRigidbody2D.velocity = new Vector2(Data.xForce * horizontaDirection, playerRigidbody2D.velocity.y);
    }


    



    /// <summary>
    /// 攻擊
    /// </summary>
    private void Attack()
    {
       
                                 
        anim.SetTrigger("攻擊觸發");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -1*transform.right, Data.atkLength, 1 << 10);
        
        if (hit.collider)
        {
            hit.collider.GetComponent<Enemy>().Hurt(Data.atk);
            //Debug.Log(hit.transform.name);
        }




    }

    /// <summary>
    /// 技能
    /// </summary>
    private void Skill()
    {

    }

    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage">傷害</param>
    public void Hurt(int damage)      //受傷
    {
        if (anim.GetBool("死亡開關")) return;
        curhp -= damage;
        anim.SetTrigger("受擊觸發");
        //playerRigidbody2D.AddForce(new Vector2(-1 * A_face * Data.moveForce, Data.jumpForce));
        hpvaluemanger.SetHpbar(curhp, Data.hp);
        
        if (curhp <= 0) Dead();
    }

    /// <summary>
    /// 死亡
    /// </summary>
    private void Dead()         //死亡
    {
        anim.SetBool("死亡開關", true);
        enabled = false;    //這個腳本停用
        
    }

    /// <summary>
    /// 翻轉
    /// </summary>
    /// <param name="face">面向的方向</param>
    private void Flip(int face)//翻轉角色方向
    {
        A_face = face;
        face = (face == 1) ? -1 : 1;
        if (face == 1)
            transform.eulerAngles = Vector3.zero;//*-1是跟原始方向是朝左邊
        else
            transform.eulerAngles = Vector3.up * (-180);


    }

    /// <summary>
    /// 繪製圖示:只會場景內顯示
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //前方 z transform.forward
        //右方 x transform.right
        //上方 y transform.up
        //Vector3.up= (0,1,0)
        Gizmos.DrawLine((Vector2)transform.position, (Vector2)transform.position + (new Vector2(A_face, 0)) * Data.atkLength);
        

    }


    /////////////////////////////////////////////////// 
    /////////////////////////////////////////////////// 
    void Start()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();//取得動畫控制器
        curhp = Data.hp;   //將現在血量設定為資料內血量
        //hpvaluemanger = GameObject.FindObjectOfType<hpValueManager>();//尋找在子物件的該類型
        //mpvaluemanger = GameObject.FindObjectOfType<mpValueManager>();
    }
  
    void Update()
    {
        MovementX();        //移動
        ControlSpeed();     //控制速度
        TryJump();          //跳躍
        if (AttackKey)      //攻擊
            Attack();

 /*       if (Input.GetKeyDown(KeyCode.Alpha1))
        {


            // play Bounce but start at a quarter of the way though              
            anim.SetTrigger("攻擊觸發");



        }*/
    }
}
    
