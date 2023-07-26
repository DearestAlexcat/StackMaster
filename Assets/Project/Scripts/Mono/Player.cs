using Client;
using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IEnumerable<Pickup>
{
    [SerializeField] float downForce = 1f;
    [SerializeField] float velocityClamp = 2.5f;

    [SerializeField] ParticleSystem warpEffect;
    [SerializeField] TrailRenderer trail;

    [SerializeField] Pickup pickupedPlayer;
    [SerializeField] Animator animator;

    [SerializeField] Transform ragdollTransform;
    [SerializeField] Rigidbody ragdollRigidBody;
    [SerializeField] BoxCollider playerCollider;

    [SerializeField] List<Pickup> pickuped;

    public float VelocityClamp => velocityClamp;
    public Pickup PickupedPlayer => pickupedPlayer;
    public Transform RagdollTransform => ragdollTransform;
    public Rigidbody RagdollRigidBody => ragdollRigidBody;
    public List<Pickup> Pickuped => pickuped;


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
        ragdollTransform.gameObject.SetActive(value);
        animator.enabled = !value;
        playerCollider.enabled = !value;


    }

    public bool ContainsPickuped(Pickup pickup)
    {
        return pickuped.Contains(pickup);
    }

    public void RemovePickuped(Pickup pickup)
    {
        if(CheckRemovedLast(pickup))
        {
            PlayJumpAnimation();
        }

        pickuped.Remove(pickup);
    }

    private bool CheckRemovedLast(Pickup pickup)
    {
        if(pickuped.Count > 1)
        {
            if (pickuped[pickuped.Count - 1] == pickup)
            {
                return true;
            }
        }

        return false;
    }

    public void CheckLevelComplete()
    {
        if(pickuped.Count == 0)
        {
            trail.time = int.MaxValue;
            //warpEffect.Stop();
            EnableRagdoll(true);

            Service<EcsWorld>.Get().ChangeState(GameState.LOSE);
        }
    }

    public void SetLevelLoseState()
    {
        trail.time = int.MaxValue;
        //warpEffect.Stop();
        EnableRagdoll(true);
        Service<EcsWorld>.Get().ChangeState(GameState.LOSE);
    }

    public void ApplyForceToFall()
    {
        foreach (var item in pickuped)
        {
            item.ThisRigidbody.AddForce(Vector3.down * downForce, ForceMode.Impulse);
        }

        pickupedPlayer.ThisRigidbody.AddForce(Vector3.down * downForce, ForceMode.Impulse);
    }

    public void UpdatePickupedPlayerPosition()
    {
        pickupedPlayer.Parent.localPosition = Vector3.up * (pickuped.Count + 1);
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
