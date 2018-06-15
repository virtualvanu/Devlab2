﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoe : Weapon {
    public Animator anim;
    public Material farmLandMat;
    private bool waiting;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Use();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ThisTile>())
        {
            other.GetComponent<Renderer>().material = farmLandMat;
            other.gameObject.layer = 14;
        }
    }

    public override void Use()
    {
        if (!waiting)
        {
            anim.SetBool("using", true);
            beingUsed = true;
            StartCoroutine(WaitForAnim());
        }
    }

    private IEnumerator WaitForAnim()
    {
        waiting = true;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        anim.SetBool("using", false);
        beingUsed = false;
        waiting = false;
    }
}