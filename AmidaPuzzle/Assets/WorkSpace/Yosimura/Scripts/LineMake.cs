using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMake : MonoBehaviour
{

    public GameObject prefab;
    private Vector3 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
       

       

      
    }

    // Update is called once per frame
    void Update()
    {
       
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//RAY�𐶐�
            RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction); // Ray�𓊎�

            var lineRenderer = gameObject.AddComponent<LineRenderer>();

        // Debug.Log("aaa");
        if (Input.GetMouseButtonDown(0))
        {
            if (hit.collider.CompareTag("Line")) // �^�O���r
            {
                var positions = new Vector3[]
                {
                     
                         


                  };
            }
          

        }
        if (Input.GetMouseButtonUp(0))
        {
            if (hit.collider.CompareTag("Line2")) // �^�O���r
            {
                var positions = new Vector3[]
                {
                       



                  };
            }


        }

       
    }
}
