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
    public float intensity = 1;     //��ת����
    public MouseUI mouseUI;
    private bool isPressMouseButton;
    public Image Tips;
    public Image ProgressBar1;
    public Image ProgressBar2;
    private Vector3 lastMousePos; //�ж�������һ֡�ƶ�
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
    /// ������ת
    /// </summary>
    private void SelfRotation()
    {
        betal1.transform.Rotate(Vector3.up * intensity);
        betal2.transform.Rotate(Vector3.up * intensity);
    }

    /// <summary>
    /// �ж�����߼�
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

                //1�����������������תΪ��Ļ���� (��Ȼ�ᱣ��z����)
                currPosition = Camera.main.WorldToScreenPoint(transform.position);

                //2������������Ļ����ϵ��x,y
                currPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, currPosition.z);

                //3������Ļ����תΪ��������
                newPosition = Camera.main.ScreenToWorldPoint(currPosition);

                //4������������������꣬y�᲻�䣨�߶Ȳ��䣩
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
            // ��������Ա�д������ײ�¼��Ĵ���
            Vector3 currentMousePos = Input.mousePosition;

            if (currentMousePos != lastMousePos&& isPressMouseButton)
            {
                // ����ڳ����ƶ��Ĵ����߼�
                ProgressBar1.gameObject.SetActive(true);
                ProgressBar2.fillAmount+=Time.deltaTime/7;
                particle.gameObject.SetActive(true);
                Tips.gameObject.SetActive(false);
                
            }
            else
            {
                // ���ֹͣ�ƶ���δ�ƶ��Ĵ����߼�
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
    /// ����
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
