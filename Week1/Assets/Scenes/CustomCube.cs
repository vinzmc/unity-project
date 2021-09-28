using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCube : MonoBehaviour
{

    float width = 1.0f;
    float height = 1.0f;
    float thick = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        Mesh mesh = new Mesh();
        var vertices = new Vector3[8];

        vertices[i] = new Vector3(-width, -height, thick);//-1, -1, 1
        vertices[++i] = new Vector3(-width, height, thick);//-1, 1, 1
        vertices[++i] = new Vector3(width, height, thick);//1, 1, 1
        vertices[++i] = new Vector3(width, -height, thick);

        vertices[++i] = new Vector3(-width, -height, -thick);
        vertices[++i] = new Vector3(-width, height, -thick);
        vertices[++i] = new Vector3(width, height, -thick);
        vertices[++i] = new Vector3(width, -height, -thick);

        mesh.vertices = vertices;

        mesh.triangles = new int[]{
            2, 1, 0,//1
            3, 2, 0,
            3, 0, 4,//2
            4, 7, 3,
            3, 6, 2,//3
            3, 7, 6,
            6, 5, 2,//4
            5, 1, 2,
            5, 6, 4,//5
            6, 7, 4,
            1, 5, 4,//6
            4, 0, 1
        };
        GetComponent<MeshFilter>().mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
