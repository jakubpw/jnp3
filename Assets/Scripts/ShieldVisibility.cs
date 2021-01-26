using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldVisibility : MonoBehaviour
{

    SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rend.enabled = GameController.Instance.IsShieldActive();
    }
}
