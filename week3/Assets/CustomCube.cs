using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class CustomCube : MonoBehaviour
{
    [SerializeField]
    public Material cubeMaterial;

    //base size
    public float size = 1f;

    private MeshFilter filter;

    // Start is called before the first frame update
    void Start()
    {
        filter = GetComponent<MeshFilter>();
        filter.mesh = GenerateMesh();
        GetComponent<MeshRenderer>().material = cubeMaterial;
        cubeMaterial.color = new Color(0.33f, 0.61f, 0.17f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        float cubeTime = Time.time;
        if(cubeTime % 2.0f == 0){
            var valueR = Random.Range(0.0f, 1.0f);
            var valueG = Random.Range(0.0f, 1.0f);
            var valueB = Random.Range(0.0f, 1.0f);
            GetComponent<MeshRenderer>().material.color = new Color(valueR, valueG, valueB);
        }
    }

    Vector3[] cubeCoordinator(float n)
    {
        float x = n * 0.5f;
        float y = x;
        float z = x;

        Vector3[] result = new Vector3[]{
            new Vector3(x, y, z),
            new Vector3(x, y, -z),
            new Vector3(x, -y, z),
            new Vector3(x, -y, -z),
            
            new Vector3(-x, y, z),
            new Vector3(-x, y, -z),
            new Vector3(-x, -y, z),
            new Vector3(-x, -y, -z)
        };

        return result;
    }
    Mesh GenerateMesh()
    {
        Mesh mesh = new Mesh();

        var vertices = new List<Vector3>();
        var triangles = new List<int>();

        float x = size * 0.5f;
        float y = x;
        float z = x;

        vertices.AddRange(cubeCoordinator(size));

        triangles.AddRange(new int[] {
            2, 1, 0,
            2, 3, 1,
            6, 3, 2,
            6, 7, 3,
            4, 7, 6,
            4, 5, 7,
            0, 5, 4,
            0, 1, 5,
            6, 0, 4,
            6, 2, 0,
            3, 5, 1,
            3, 7, 5
        });

        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);

        return mesh;
    }
}
