using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int x_index;
    public int y_index;
    private RectTransform rectTransform;
    private Vector3 originalPosition;
    private PuzzleManager puzzleManager;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
        puzzleManager = FindObjectOfType<PuzzleManager>();
    }
    public void Initialize(int x, int y)
    {
        x_index = x;
        y_index = y;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(IsInCorrectPosition())
        {
            puzzleManager.num--;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //拖动时改变位置
        rectTransform.anchoredPosition += eventData.delta/ GetComponentInParent<Canvas>().scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(IsInCorrectPosition())
        {
            rectTransform.anchoredPosition = puzzleManager.GetCorrectPosition(x_index, y_index);
            puzzleManager.num++;
        }
        else
        {
            rectTransform.anchoredPosition = originalPosition;
        }
        //错误位置返回原始位置
        //rectTransform.anchoredPosition = originalPosition;

        JudgeEnd();
    }
    private void JudgeEnd()
    {
        if(puzzleManager.num >= puzzleManager.rows * puzzleManager.columns)
        {
            Debug.Log("-------游戏结束------");
        }
    }
    private bool IsInCorrectPosition()
    {
        bool isMatch = 
            Mathf.Abs(rectTransform.anchoredPosition.x - puzzleManager.GetCorrectPosition(x_index, y_index).x) < 10f 
            && Mathf.Abs(rectTransform.anchoredPosition.y - puzzleManager.GetCorrectPosition(x_index, y_index).y) < 10f;
        return isMatch;
    }
}