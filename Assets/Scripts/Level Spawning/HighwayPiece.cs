using UnityEngine;

public class HighwayPiece : MonoBehaviour
{
    private MeshRenderer _highwayPieceMesh;

    private void Awake() => _highwayPieceMesh = GetComponentInChildren<MeshRenderer>();

    public float GetLength() => _highwayPieceMesh.bounds.max.z - _highwayPieceMesh.bounds.min.z;
}
