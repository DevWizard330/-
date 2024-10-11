using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    public GameObject targetImage;
    public Texture2D originalImage;
    public GameObject puzzlePiecePrefab;
    public int rows = 3;
    public int columns = 3;
    public Vector3[,] rightPos;
    public int num;//当前已经拼好的数量
    // Start is called before the first frame update
    void Start()
    {
        rightPos = new Vector3[rows, columns];
        CreatePuzzle();
    }

    void CreatePuzzle()
    {
        int pieceWidth = originalImage.width / columns;
        int pieceHeight = originalImage.height / rows;
        for (int y = 0; y < rows; y++)
        {
            for(int x = 0; x < columns; x++)
            {
                Texture2D piece = new Texture2D(pieceWidth, pieceHeight);
                piece.SetPixels(originalImage.GetPixels(x * pieceWidth, y * pieceHeight, pieceWidth, pieceHeight));
                piece.Apply();
                GameObject puzzlePiece = Instantiate(puzzlePiecePrefab, transform);
                puzzlePiece.name = $"Piece_{x}_{y}";
                puzzlePiece.GetComponent<Image>().sprite = Sprite.Create(piece, new Rect(0, 0, pieceWidth, pieceHeight), new Vector2(0.5f, 0.5f));
                puzzlePiece.GetComponent<PuzzlePiece>().Initialize(x,y);
                puzzlePiece.GetComponent<RectTransform>().anchoredPosition = 
                    new Vector2(Random.Range(0f, 250f), Random.Range(-150f, 150f));
                RecordRightPos(x,y);
            }
        }
    }
    void RecordRightPos(int posindexX, int posindexY)
    {
        Vector2 anchoredPosition = targetImage.GetComponent<RectTransform>().anchoredPosition;
        Vector2 targetPos = new Vector2(anchoredPosition.x - 100,
            anchoredPosition.y - 100);
        rightPos[posindexX, posindexY] = new Vector3(
            targetPos.x + posindexX * 100,
            targetPos.y + posindexY * 100, 0);
    }
    public Vector3 GetCorrectPosition(int x, int y)
    {
        return rightPos[x, y];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
