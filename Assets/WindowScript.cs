using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowScript : MonoBehaviour
{
    //linked objects
    [Header("Window - Linked Objects")]
    [SerializeField] private BoxCollider2D box_collider;

    public void ShatterWindow()
    {
        //play shatter animation
        //remove sprite
        //test comment
        box_collider.enabled = false;
    }
}
