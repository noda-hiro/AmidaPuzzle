using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMake : MonoBehaviour
{
     LineRenderer lineRenderer;

    GameObject Line;

    GameObject Line2;


    private Vector3 mousePosition;



    // Start is called before the first frame update
    void Start()
    {
        this.Line = GameObject.Find("Line");


        this.Line = GameObject.Find("Line2");

        this.lineRenderer = GetComponent<LineRenderer>();

        this.lineRenderer.positionCount = 2;


    }

    // Update is called once per frame
    void Update()
    {


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//RAYを生成
        RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction); // Rayを投射

       
        // Debug.Log("aaa");

       // LineRenderの開始点を取る
         if (Input.GetMouseButtonDown(0))
          {
              Vector3 mousePosition = Input.mousePosition;

              lineRenderer.SetPosition(0, mousePosition);

          }

          //LineRenderの終着点を取る
          if (Input.GetMouseButtonUp(0))
          {
              Vector3 mousePosition = Input.mousePosition;
              //LineRenderの終着点を取る
              lineRenderer.SetPosition(1, mousePosition);
          }

    }
    
    }