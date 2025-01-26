using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 所有UI菜单的基类
/// </summary>
public class MenuBase : MonoBehaviour
{

    /// <summary>
    /// 初始话所有text，进度条等组件
    /// </summary>
    public virtual void InitItem() { }
    /// <summary>
    /// 打开UI
    /// </summary>
    public virtual void OpenMenu()
    {

        this.transform.gameObject.SetActive(true);
    }
    /// <summary>
    /// 更新
    /// </summary>
    public virtual void UpdataMenu()
    {

    }
    /// <summary>
    /// 关闭UI
    /// </summary>
    public virtual void CloseMenu()
    {
        this.transform.gameObject.SetActive(false);

    }

}
