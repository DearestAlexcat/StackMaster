using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] Collider thisCollider;
    [SerializeField] Rigidbody thisRigidbody;
    [SerializeField] Transform parent;

    public Rigidbody ThisRigidbody => thisRigidbody;
    public Collider ThisCollider => thisCollider;
    public Transform Parent => parent;
}
