using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiquidChange : MonoBehaviour
{
    public Image ProgressBar;
    private Material material;
    /// <summary>
    /// 贴图数量
    /// </summary>
    private int textureCount = 3;
    private Texture[] texture;
    /// <summary>
    /// 文件夹名
    /// </summary>
    private string materialTexture = "MaterialTexture";
    void Start()
    {
        material = this.GetComponent<Renderer>().material;
        // 定义获取贴图的数量
        texture = new Texture[textureCount];
        //动态加载贴图
        for (int i = 0; i < texture.Length; i++)
        {
            texture[i] = Resources.Load(materialTexture + "/batter_mix_0" + (i + 1)) as Texture;
        }
    }

    void Update()
    {
        if(ProgressBar.fillAmount > 0.3 && ProgressBar.fillAmount <= 0.5)
        {
            material.SetTexture("_MainTex", texture[0]);
        }
        if (ProgressBar.fillAmount > 0.5 && ProgressBar.fillAmount <= 0.7)
        {
            material.SetTexture("_MainTex", texture[1]);
        }
        if (ProgressBar.fillAmount == 1)
        {
            material.SetTexture("_MainTex", texture[2]);
        }
    }
}
