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
       
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//RAYを生成
            RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction); // Rayを投射

            var lineRenderer = gameObject.AddComponent<LineRenderer>();

        // Debug.Log("aaa");
        if (Input.GetMouseButtonDown(0))
        {
            if (hit.collider.CompareTag("Line")) // タグを比較
            {
                var positions = new Vector3[]
                {
                     
                         


                  };
            }
          

        }
        if (Input.GetMouseButtonUp(0))
        {
            if (hit.collider.CompareTag("Line2")) // タグを比較
            {
                var positions = new Vector3[]
                {
                       



                  };
            }


        }

       
    }
}
