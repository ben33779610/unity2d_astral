using UnityEngine;
using UnityEngine.UI;

public class mpValueManager : MonoBehaviour
{
    private Image Mpbar;        //魔力條


    void Start()
    {
        Mpbar = transform.GetChild(1).GetComponent<Image>();

    }

    /// <summary>
    /// 設定血量
    /// </summary>
    /// <param name="curHp"></param>
    /// <param name="Hpmax"></param>
    public void SetHpbar(float curMp, float Mpmax)
    {
        Mpbar.fillAmount = curMp / Mpmax;
    }
}
