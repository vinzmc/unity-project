using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class CustomCubeWithShader : MonoBehaviour
{
    [SerializeField]
    public Material cubeMaterial;

    //base size
    public float size = 1f;

    private MeshFilter filter;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshFilter>().mesh = GenerateMesh();
        GetComponent<MeshRenderer>().material = cubeMaterial;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        float cubeTime = Time.fixedTime;
        if (cubeTime % 2.0f == 0)
        {
            var mesh = GetComponent<MeshFilter>().mesh;
            var length = mesh.vertices.Length;
            Color32[] colors = new Color32[length];
            for (int i = 0; i < length; i++)
            {
                byte valueR = (byte)(Random.Range(0.0f, 1.0f) * 255);
                byte valueB = (byte)(Random.Range(0.0f, 1.0f) * 255);
                byte valueG = (byte)(Random.Range(0.0f, 1.0f) * 255);
                colors[i] = new Color32(valueR, valueG, valueB, 255);
            }
            mesh.colors32 = colors;
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

        var colors = new Color32[vertices.Count];
        colors[0] = new Color32(255, 0, 0, 255);
        colors[1] = new Color32(0, 255, 0, 255);
        colors[2] = new Color32(0, 0, 255, 255);
        colors[3] = new Color32(255, 0, 255, 255);
        colors[4] = new Color32(255, 0, 0, 255);
        colors[5] = new Color32(0, 255, 0, 255);
        colors[6] = new Color32(0, 0, 255, 255);
        colors[7] = new Color32(255, 0, 255, 255);

        mesh.SetColors(colors);
        mesh.SetTriangles(triangles, 0);

        return mesh;
    }
}
