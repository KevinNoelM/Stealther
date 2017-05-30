﻿using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField]
    protected AudioClip sfxClip;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        OnCollision(other.gameObject);
    }
    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        OnCollision(other.gameObject);
    }

    protected virtual void OnCollision(GameObject other)
    {
        var ropeNode = other.GetComponent<RopeNode>();
        if (ropeNode)
        {
            ropeNode.CutRope(this);
        }
        var ninja = other.GetComponent<NinjaController>();
        if (ninja != null && ninja.IsKillable)
        {
            NinjaKilled(ninja);
        }
    }

    protected virtual void NinjaKilled(NinjaController killedNinja)
    {
        Vector2 position = killedNinja.transform.position;
        killedNinja.Killed();
        OnKilledNinja(killedNinja);
        GameManager.Instance.NinjaKilled(killedNinja, position);
    }
    

    protected virtual void OnKilledNinja(NinjaController ninja)
    {
        if (audioSource != null && sfxClip != null)
        {
            audioSource.PlayOneShot(sfxClip);
        }
    }
}
