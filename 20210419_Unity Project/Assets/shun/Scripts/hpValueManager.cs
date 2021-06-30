using UnityEngine;
using UnityEngine.UI;


public class hpValueManager : MonoBehaviour
{
    private Image Hpbar;		//血條
    





    void Start()
    {
        Hpbar = transform.GetChild(1).GetComponent<Image>();
        
    }

    /// <summary>
    /// 設定血量
    /// </summary>
    /// <param name="curHp"></param>
    /// <param name="Hpmax"></param>
    public void SetHpbar(float curHp, float Hpmax)
    {
        Hpbar.fillAmount = curHp / Hpmax;
    }
}
