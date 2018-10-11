using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //斧、SpeCialManに当たったら、消える
        if (collision.gameObject.tag == "SpecialMan" || collision.gameObject.tag == "Ax" || collision.gameObject.tag == "Green" || collision.gameObject.tag == "Red")
        {
            Destroy(gameObject);
        }
    }
}
