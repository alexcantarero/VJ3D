using UnityEngine;
using System.Collections.Generic;

public class UIButtonSelectHorizontal : MonoBehaviour
{
    public List<RectTransform> panels;  // Arrastra los paneles aquí en el Inspector
    public float selectedScale = 1.2f;
    public float normalScale = 1f;

    private int currentIndex = 0;

    void Start()
    {
        HighlightSelectedPanel();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentIndex = (currentIndex + 1) % panels.Count;
            HighlightSelectedPanel();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentIndex = (currentIndex - 1 + panels.Count) % panels.Count;
            HighlightSelectedPanel();
        }
    }

    void HighlightSelectedPanel()
    {
        for (int i = 0; i < panels.Count; i++)
        {
            if (i == currentIndex)
            {
                panels[i].localScale = Vector3.one * selectedScale;
            }
            else
            {
                panels[i].localScale = Vector3.one * normalScale;
            }
        }
    }
}
