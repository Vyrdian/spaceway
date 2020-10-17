using System.Collections.Generic;
using UnityEngine;

public class HighwayPool : MonoBehaviour
{
    [SerializeField] private HighwayPiece _highwayPiece = null;
    [SerializeField] private int _initialHighwayPieces= 50;

    public float HighwayPieceLength { get; private set; }

    private Queue<HighwayPiece> _highwayPieces = new Queue<HighwayPiece>();

    private void Awake()
    {
        AddHighwayPieces(_initialHighwayPieces);
        GetHighwayPieceLength();
    }

    private void GetHighwayPieceLength()
    {
        HighwayPiece highwayPiece = GetHighwayPiece();
        HighwayPieceLength = highwayPiece.GetLength();
        ReturnToQueue(highwayPiece);
    }

    private void AddHighwayPieces(int num)
    {
        for (int i = 0; i < num; i++)
        {
            HighwayPiece highwayPiece = Instantiate(_highwayPiece, transform, true);
            highwayPiece.gameObject.SetActive(false);
            _highwayPieces.Enqueue(highwayPiece);
        }
    }

    public HighwayPiece GetHighwayPiece()
    {
        if (_highwayPieces.Count == 0)
            AddHighwayPieces(1);
        return _highwayPieces.Dequeue();
    }

    public void ReturnToQueue(HighwayPiece highwayPiece)
    {
        _highwayPieces.Enqueue(highwayPiece);
        highwayPiece.gameObject.SetActive(false);
    }
}