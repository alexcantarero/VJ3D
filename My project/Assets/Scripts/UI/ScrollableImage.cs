using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollableImage : MonoBehaviour
{
    [SerializeField] RawImage scrollableImage;
    [SerializeField] float scrollSpeed;
    [SerializeField] Vector2 scrollDirection; 
    Rect rect;
    void Start()
    {
        rect = scrollableImage.uvRect;
    }

    void Update()
    {
        rect.x += Time.deltaTime * scrollSpeed * scrollDirection.x;
        rect.y += Time.deltaTime * scrollSpeed * scrollDirection.y;
        scrollableImage.uvRect = rect;
    }
}
