using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class CustomCubeWithShader : MonoBehaviour
{
    [SerializeField]
    public Texture myTexture;
    public Material cubeMaterial;
    //base size
    public float size = 1f;

    private MeshFilter filter;
    

    // Start is called before the first frame update
    void Start()
    {
        //texture
        myTexture = Resources.Load<Texture>("Textures/ruangan");
        cubeMaterial.mainTexture = myTexture;
        
        GetComponent<MeshFilter>().mesh = GenerateMesh();
        GetComponent<MeshRenderer>().material = cubeMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myTexture.filterMode = SwitchFilterModes();

            Debug.Log("Filter Mode : " + myTexture.filterMode);
        }
    }

    FilterMode SwitchFilterModes()
    {
        switch (myTexture.filterMode)
        {
            case FilterMode.Bilinear:
                myTexture.filterMode = FilterMode.Point;
                break;
            case FilterMode.Point:
                myTexture.filterMode = FilterMode.Trilinear;
                break;
            case FilterMode.Trilinear:
                myTexture.filterMode = FilterMode.Bilinear;
                break;
        }
        return myTexture.filterMode;
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


        float x = size * 0.5f;
        float y = x;
        float z = x;

        //vertices
        vertices.AddRange(cubeCoordinator(size));
        mesh.SetVertices(vertices);

        //uvs
        Vector2[] uvs = new Vector2[vertices.Count];

        //challenge
        int i = 0;
        float a = 0.25f;
        float b = 0.33f;
        float x1 = 0f;
        float x2 = 0.25f;
        float y1 = 0.33f;
        float y2 = 0.66f;
        //x+
        uvs[i++] = new Vector2(x2, y2); //1
        uvs[i++] = new Vector2(x2, y1); //2
        uvs[i++] = new Vector2(x1, y2); //3
        uvs[i++] = new Vector2(x1, y1); //4

        //y+
        
        uvs[i++] = new Vector2(x1 + (2 * a), y2 + b); //3
        uvs[i++] = new Vector2(x1 + (2 * a), y1 + b); //4
        uvs[i++] = new Vector2(x2 + (2 * a), y2 + b); //1
        uvs[i++] = new Vector2(x2 + (2 * a), y1 + b); //2

        //z+
        uvs[i++] = new Vector2(x1 + (a), y2); //3
        uvs[i++] = new Vector2(x2 + (a), y2); //1
        uvs[i++] = new Vector2(x1 + (a), y1); //4
        uvs[i++] = new Vector2(x2 + (a), y1); //2
        
        //x-
        uvs[i++] = new Vector2(x1 + (2 * a), y2); //3
        uvs[i++] = new Vector2(x1 + (2 * a), y1); //4
        uvs[i++] = new Vector2(x2 + (2 * a), y2); //1
        uvs[i++] = new Vector2(x2 + (2 * a), y1); //2

        //y-  
        uvs[i++] = new Vector2(x1 + (2 * a), y2 - b); //3
        uvs[i++] = new Vector2(x1 + (2 * a), y1 - b); //4
        uvs[i++] = new Vector2(x2 + (2 * a), y2 - b); //1
        uvs[i++] = new Vector2(x2 + (2 * a), y1 - b); //2
        
        //z-
        uvs[i++] = new Vector2(x2 + (3 * a), y2); //2
        uvs[i++] = new Vector2(x1 + (3 * a), y2); //1
        uvs[i++] = new Vector2(x2 + (3 * a), y1); //4
        uvs[i++] = new Vector2(x1 + (3 * a), y1); //3

        /* Mapping texture tutorial
        float a = 0f;
        float b = 0.25f;
        float c = -0.5f;
        float d = 0f;

        for (int i = 0; i < 6; i++)
        {
            //2 4 6
            //1 3 5
            if ((i + 1) % 2 == 0)
            {//remove +1 to start from +1
                a += 0.25f;
                b += 0.25f;
            }
            c = c == 0.5f ? -0.5f : c;
            c += 0.5f;
            d = d == 1f ? 0f : d;
            d += 0.5f;

            // 0 - 1 (normal)
            // uvs[i * 4] = new Vector2(1, 1);
            // uvs[(i * 4) + 1] = new Vector2(0, 1);
            // uvs[(i * 4) + 2] = new Vector2(1, 0);
            // uvs[(i * 4) + 3] = new Vector2(0, 0);

            //1- > 3, 2 -> 1, 4-> 2, 3->4 (Z)
            uvs[(i * 4) + 1] = new Vector2(b, d);
            uvs[(i * 4) + 3] = new Vector2(b, c);
            uvs[i * 4] = new Vector2(a, d);
            uvs[(i * 4) + 2] = new Vector2(a, c);
        }
        */
        mesh.SetUVs(0, uvs);

        //gray
        colors.AddRange(colorsLooper(220, 220, 220));//x+
        colors.AddRange(colorsLooper(211, 211, 211));//y+
        colors.AddRange(colorsLooper(220, 220, 220));//z+
        colors.AddRange(colorsLooper(211, 211, 211));//x-
        colors.AddRange(colorsLooper(220, 220, 220));//y-
        colors.AddRange(colorsLooper(211, 211, 211));//z-
        mesh.SetColors(colors);

        //triangles
        // triangles.AddRange(new int[] {
        //     2,0,1,
        //     2,1,3,
        //     6,7,5,
        //     4,6,5,
        //     8,9,11,
        //     8,11,10,
        //     12,14,13,
        //     14,15,13,
        //     19,18,17,
        //     18,16,17,
        //     21,20,23,
        //     20,22,23
        // });

        // reverse cube
        triangles.AddRange(new int[] {
            1,0,2,
            3,1,2,
            5,7,6,
            5,6,4,
            11,9,8,
            10,11,8,
            13,14,12,
            13,15,14,
            17,18,19,
            17,16,18,
            23,20,21,
            23,22,20
        });
        mesh.SetTriangles(triangles, 0);

        // normals
        // var normals = new List<Vector3>();
        // normals.AddRange(normalsLooper(-1f, 0f, 0f));//x+
        // normals.AddRange(normalsLooper(0f, -1f, 0f));//y+
        // normals.AddRange(normalsLooper(0f, 0f, -1f));//z+
        // normals.AddRange(normalsLooper(1f, 0f, 0f));//x-
        // normals.AddRange(normalsLooper(0f, 1f, 0f));//y-
        // normals.AddRange(normalsLooper(0f, 0f, 1f));//z-
        // mesh.SetNormals(normals);
        mesh.RecalculateNormals();
        return mesh;
    }
}