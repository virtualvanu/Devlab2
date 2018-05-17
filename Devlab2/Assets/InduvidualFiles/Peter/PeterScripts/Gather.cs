﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gather : Weapon {

    private Equippable tool;

    public Animator anim;

    private bool waiting;
	// Use this for initialization
	void Start () {

        tool = equippable;
        Inventory.itemInHand = tool;
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            Use();
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        //Debug.Log(col.name);

        if (anim.GetBool("using"))
        {
            if (col.transform.tag == "Resource")
            {
                col.transform.GetComponent<Resource>().Harvest();
            }
          

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
