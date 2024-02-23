using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MouseUI : MonoBehaviour
{
    public Image Mouse;
    void Start()
    {
        Mouse.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Open();
        ToHideCursor();
    }

    private void Open()
    {
        
        Mouse.transform.position = Input.mousePosition;
    }

    /// <summary>
	/// “˛≤ÿ Û±Í
	/// </summary>
	void ToHideCursor()
    {
        Cursor.visible = false;
    }
    /// <summary>
    /// œ‘ æ Û±Í
    /// </summary>
    void ToShowCursor()
    {
        Cursor.visible = true;
    }
}
