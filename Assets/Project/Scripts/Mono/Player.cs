using Client;
using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IEnumerable<Pickup>
{
    [SerializeField] float downForce = 1f;
    [SerializeField] float velocityClamp = 2.5f;

    [Header("Effects")]
    [SerializeField] ParticleSystem warpEffect;
    [SerializeField] TrailRenderer trail;

    [Header("Player")]
    [SerializeField] Animator animator;
    [SerializeField] BoxCollider playerCollider;
    [SerializeField] Rigidbody playerRigidbody;
    [SerializeField] Transform playerObject;

    [Header("Ragdoll")]
    [SerializeField] Transform ragdollHolder;

    [Header("List")]
    [SerializeField] List<Pickup> pickuped;

    public float VelocityClamp => velocityClamp;
    public Rigidbody PlayerRigidbody => playerRigidbody;
    public Transform PlayerObject => playerObject;
    public Transform RagdollHolder => ragdollHolder;
    public TrailRenderer Trail => trail;

    private void Start()
    {
        EnableRagdoll(false);
        //  Debug.Log(Animator.StringToHash("Jump")); // 125937960
    }

    public void PlayWarpEffect()
    {
        warpEffect.Play();
    }

    public void PlayJumpAnimation()
    {
        animator.SetTrigger(125937960);
    }

    private void EnableRagdoll(bool value)
    {
        ragdollHolder.gameObject.SetActive(value);
        animator.enabled = !value;
        playerCollider.enabled = !value;
        playerRigidbody.isKinematic = value;
    }

    public bool ContainsPickuped(Pickup pickup)
    {
        return pickuped.Contains(pickup);
    }

    public void RemovePickuped(Pickup pickup)
    {
        if (IsRemovingTopCube(pickup))
        {
            PlayJumpAnimation();
        }

        pickuped.Remove(pickup);
    }

    private bool IsRemovingTopCube(Pickup pickup)
    {
        return pickuped.Count > 1 && pickuped[pickuped.Count - 1] == pickup;
    }

    public void CheckLevelComplete()
    {
        if (pickuped.Count == 0)
        {
            SetLevelLoseState();
        }
    }

    public void SetLevelLoseState()
    {
        warpEffect.Stop();
        trail.time = int.MaxValue;
        EnableRagdoll(true);
        Service<EcsWorld>.Get().ChangeState(GameState.LOSE);
    }

    public void ApplyForceToFall()
    {
        foreach (var item in pickuped)
        {
            item.ThisRigidbody.AddForce(Vector3.down * downForce, ForceMode.Impulse);
        }

        playerRigidbody.AddForce(Vector3.down * downForce, ForceMode.Impulse);
    }

    public void UpdatePickupedPlayerPosition()
    {
        playerObject.localPosition = Vector3.up * (pickuped.Count + 1);
    }

    public void AddedPickupCube(Pickup pickup)
    {
        pickup.gameObject.tag = "Pickuped";
        pickup.Parent.SetParent(transform);
        pickup.Parent.localPosition = Vector3.up * pickuped.Count;
        pickup.ThisRigidbody.isKinematic = false;
        pickup.ThisCollider.isTrigger = false;

        pickuped.Add(pickup);
    }

    public Vector3 GetNextDesiredPosition()
    {
        return transform.TransformPoint(Vector3.up * pickuped.Count);
    }

    public IEnumerator<Pickup> GetEnumerator()
    {
        foreach (var item in pickuped)
        {
            yield return item;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
