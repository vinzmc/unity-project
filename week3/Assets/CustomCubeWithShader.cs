using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class CustomCubeWithShader : MonoBehaviour
{
    [SerializeField]
    public Material cubeMaterial;

    //Spin speed and Rotation Amount
    public int spinSpeed;
    public Vector3 RotateAmount;

    //base size
    public float size = 1f;

    private MeshFilter filter;

    // Start is called before the first frame update
    void Start()
    {
        spinSpeed = 4;
        RotateAmount = new Vector3(0.0f, 50.0f, 0.0f);
        GetComponent<MeshFilter>().mesh = GenerateMesh();
        GetComponent<MeshRenderer>().material = cubeMaterial;
        // foreach(Vector3 normal in GetComponent<MeshFilter>().mesh.normals){
        //     Debug.Log(normal.x + " " + normal.y + " " + normal.z);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        //spin the cube
        transform.Rotate(RotateAmount * Time.deltaTime / spinSpeed);
    }

    void FixedUpdate()
    {

    }

    Vector3[] cubeCoordinator(float n)
    {
        float x = n * 0.5f;//width
        float y = x;//height
        float z = x;//thick

        Vector3[] result = new Vector3[]{
            //x+
            new Vector3(x, y, z),
            new Vector3(x, -y, z),
            new Vector3(x, y, -z),
            new Vector3(x, -y, -z),

            //y+
            new Vector3(x, y, z),
            new Vector3(-x, y, z),
            new Vector3(x, y, -z),
            new Vector3(-x, y, -z),

            //z+
            new Vector3(x, y, z),
            new Vector3(-x, y, z),
            new Vector3(x, -y, z),
            new Vector3(-x, -y, z),

            //x-
            new Vector3(-x, y, z),
            new Vector3(-x, -y, z),
            new Vector3(-x, y, -z),
            new Vector3(-x, -y, -z),

            //y-
            new Vector3(x, -y, z),
            new Vector3(-x, -y, z),
            new Vector3(x, -y, -z),
            new Vector3(-x, -y, -z),

            //z-
            new Vector3(x, y, -z),
            new Vector3(-x, y, -z),
            new Vector3(x, -y, -z),
            new Vector3(-x, -y, -z)
        };
        return result;
    }

    Color32[] colorsLooper(byte x, byte y, byte z)
    {
        Color32[] result = new Color32[4];
        for (int i = 0; i < 4; i++)
        {
            result[i] = new Color32(x, y, z, 255);
        }
        return result;
    }

    Vector3[] normalsLooper(float x, float y, float z)
    {
        Vector3[] result = new Vector3[4];
        for (int i = 0; i < 4; i++)
        {
            result[i] = new Vector3(x, y, z);
        }
        return result;
    }

    Mesh GenerateMesh()
    {
        Mesh mesh = new Mesh();

        var vertices = new List<Vector3>();
        var colors = new List<Color32>();
        var triangles = new List<int>();
        //var normals = new List<Vector3>();

        float x = size * 0.5f;
        float y = x;
        float z = x;

        //vertices
        vertices.AddRange(cubeCoordinator(size));
        mesh.SetVertices(vertices);

        //colors Tutorial modul
        // colors.AddRange(colorsLooper(255, 0, 0));//x+
        // colors.AddRange(colorsLooper(0, 255, 0));//y+
        // colors.AddRange(colorsLooper(0, 0, 255));//z+
        // colors.AddRange(colorsLooper(255, 255, 0));//x-
        // colors.AddRange(colorsLooper(0, 255, 255));//y-
        // colors.AddRange(colorsLooper(255, 0, 255));//z-
        
        //challenge
        colors.AddRange(colorsLooper(220,220,220));//x+
        colors.AddRange(colorsLooper(211,211,211));//y+
        colors.AddRange(colorsLooper(220,220,220));//z+
        colors.AddRange(colorsLooper(211,211,211));//x-
        colors.AddRange(colorsLooper(220,220,220));//y-
        colors.AddRange(colorsLooper(211,211,211));//z-
        mesh.SetColors(colors);

        //triangles
        triangles.AddRange(new int[] {
            2,0,1,
            2,1,3,
            6,7,5,
            4,6,5,
            8,9,11,
            8,11,10,
            12,14,13,
            14,15,13,
            19,18,17,
            18,16,17,
            21,20,23,
            20,22,23
        });
        mesh.SetTriangles(triangles, 0);

        //normals
        // normals.AddRange(normalsLooper(1f, 0f, 0f));//x+
        // normals.AddRange(normalsLooper(0f, 1f, 0f));//y+
        // normals.AddRange(normalsLooper(0f, 0f, 1f));//z+
        // normals.AddRange(normalsLooper(-1f, 0f, 0f));//x-
        // normals.AddRange(normalsLooper(0f, -1f, 0f));//y-
        // normals.AddRange(normalsLooper(0f, 0f, -1f));//z-
        // mesh.SetNormals(normals);
        mesh.RecalculateNormals();
        return mesh;
    }
}
