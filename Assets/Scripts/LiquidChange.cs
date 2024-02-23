using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiquidChange : MonoBehaviour
{
    public Image ProgressBar;
    private Material material;
    /// <summary>
    /// ��ͼ����
    /// </summary>
    private int textureCount = 3;
    private Texture[] texture;
    /// <summary>
    /// �ļ�����
    /// </summary>
    private string materialTexture = "MaterialTexture";
    void Start()
    {
        material = this.GetComponent<Renderer>().material;
        // �����ȡ��ͼ������
        texture = new Texture[textureCount];
        //��̬������ͼ
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
