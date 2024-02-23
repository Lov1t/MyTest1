using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class MainMove : MonoBehaviour
{
    public GameObject blender;
    private Vector3 blenderIniPositon;
    private Vector3 blenderIniRotation;

    public GameObject betal1;
    public GameObject betal2;
    public float intensity = 1;     //旋转力度
    public MouseUI mouseUI;
    private bool isPressMouseButton;
    public Image Tips;
    public Image ProgressBar1;
    public Image ProgressBar2;
    private Vector3 lastMousePos; //判断鼠标最后一帧移动
    private bool once = true;
    public ParticleSystem particle;
    public ParticleSystem full;
    void Start()
    {
        lastMousePos = Input.mousePosition;
        blenderIniPositon = blender.transform.position;
        blenderIniRotation = blender.transform.eulerAngles;
       // particle.Stop();
       // full.Stop();
    }

    void Update()
    {
        MouseClick();
        Clear();
        
    }

    /// <summary>
    /// 打蛋器自转
    /// </summary>
    private void SelfRotation()
    {
        betal1.transform.Rotate(Vector3.up * intensity);
        betal2.transform.Rotate(Vector3.up * intensity);
    }

    /// <summary>
    /// 判断鼠标逻辑
    /// </summary>
    private void MouseClick()
    {
     
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            isPressMouseButton = true;
           
        }
        if (Input.GetMouseButtonUp(0))
        {
            isPressMouseButton = false;
           
        }

        if (Physics.Raycast(ray, out hit)&&isPressMouseButton)
        {
            if (hit.transform.tag == "TestGameObj")
            {
                mouseUI.Mouse.gameObject.SetActive(false);

                Vector3 currPosition;
                Vector3 newPosition;

                //1：把物体的世界坐标转为屏幕坐标 (依然会保留z坐标)
                currPosition = Camera.main.WorldToScreenPoint(transform.position);

                //2：更新物体屏幕坐标系的x,y
                currPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, currPosition.z);

                //3：把屏幕坐标转为世界坐标
                newPosition = Camera.main.ScreenToWorldPoint(currPosition);

                //4：更新物体的世界坐标，y轴不变（高度不变）
                transform.position = new Vector3(newPosition.x, newPosition.y, -0.46f);

                SelfRotation();
                blender.transform.eulerAngles = new Vector3(0,0,0);

                if(once)
                Tips.gameObject.SetActive(true);
                once = false;
            }
        }
        else if(isPressMouseButton == false)
        {
            mouseUI.Mouse.gameObject.SetActive(true);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Liquid"))
        {
            // 在这里可以编写处理碰撞事件的代码
            Vector3 currentMousePos = Input.mousePosition;

            if (currentMousePos != lastMousePos&& isPressMouseButton)
            {
                // 鼠标在持续移动的处理逻辑
                ProgressBar1.gameObject.SetActive(true);
                ProgressBar2.fillAmount+=Time.deltaTime/7;
                particle.gameObject.SetActive(true);
                Tips.gameObject.SetActive(false);
                
            }
            else
            {
                // 鼠标停止移动或未移动的处理逻辑
                //  Tips.gameObject.SetActive(true);
               
            }

            lastMousePos = currentMousePos;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Liquid"))
        {
          //  particle.Stop();
            particle.gameObject.SetActive(false);
        }
    }


    /// <summary>
    /// 清理
    /// </summary>
    private void Clear()
    {
        if (ProgressBar2.fillAmount == 1)
        {
            blender.transform.position = blenderIniPositon;
            blender.transform.eulerAngles = blenderIniRotation;
            ProgressBar1.gameObject.SetActive(false);
            particle.gameObject.SetActive(false);
            full.gameObject.SetActive(true);
        }
    }
}
