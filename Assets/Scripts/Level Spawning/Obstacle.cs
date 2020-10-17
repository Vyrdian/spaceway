using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Obstacle : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
            other.GetComponent<PlayerController>().Collided();
    }

    protected virtual void Start() => GetComponent<BoxCollider>().isTrigger = true;

}
