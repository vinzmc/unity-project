using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class ChallengeTwoMauriceMarvin : MonoBehaviour
{
    //base size
    public float size = 0.3f;

    private MeshFilter filter;
    public Material cubeMaterial;

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

    //base = Z
    // 1 ----- 2  (plane)
    // |     / |
    // |    /  |
    // |   /   |
    // |  /    |
    // 3 ----- 4
    //  top -> bottom, left -> right

    //right plane coordinate
    Vector3[] planeR(float w, float h, float aX, float aY, float aZ)
    {
        float y = h * 0.5f;
        float z = w * 0.5f;

        Vector3[] result = new Vector3[]{
            new Vector3(aX, y+aY, -z+aZ),   //1
            new Vector3(aX, y+aY, z+aZ),    //2
            new Vector3(aX, -y+aY, -z+aZ),  //3
            new Vector3(aX, -y+aY, z+aZ)    //4
        };

        return result;
    }

    //top plane coordinate
    Vector3[] planeT(float w, float h, float aX, float aY, float aZ)
    {
        float x = w * 0.5f;
        float z = h * 0.5f;

        Vector3[] result = new Vector3[]{
            new Vector3(-x+aX, aY, z+aZ),   //6
            new Vector3(x+aX, aY, z+aZ),    //5
            new Vector3(-x+aX, aY, -z+aZ),  //2
            new Vector3(x+aX, aY, -z+aZ)    //1
        };

        return result;
    }

    //front plane coordinate
    Vector3[] planeF(float w, float h, float aX, float aY, float aZ)
    {
        float x = w * 0.5f;
        float y = h * 0.5f;

        Vector3[] result = new Vector3[]{
            new Vector3(-x+aX, y+aY, aZ), //5
            new Vector3(x+aX, y+aY, aZ),  //1
            new Vector3(-x+aX, -y+aY, aZ),//7
            new Vector3(x+aX, -y+aY, aZ)  //3
        };

        return result;
    }

    //Z
    //right/top/front   = (a, b, c, d) || (1 2 3 4) / (6 5 2 1) / (5 1 7 3)
    //left/bottom/back  = (b, a, d, c) || (2 1 4 3) / (5 6 1 2) / (1 5 3 7)
    //masukan 4 titik pada sebuah lempeng / plane
    int[] connectTriangle(int a, int b, int c, int d)
    {

        int[] result = new int[]{
            a, b, c,
            c, b, d
        };

        return result;
    }

    Mesh GenerateMesh()
    {
        Mesh mesh = new Mesh();

        var vertices = new List<Vector3>();
        var triangles = new List<int>();
        float[] binary = { 1, -1 };
        float baseY = 0;//y-axis untuk anchor. 0 = middle, 4*size = bottom

        float tempLength = 4 * size;
        float temp = (float)((2 * size) - (0.5 * size));//lokasi taplak kaki
        // -+ [0 - 7] kaki 1    | top left
        // ++ [8 - 15] kaki 2   | top right
        // -- [16 - 23] kaki 3  | bot left
        // +- [24 - 31] kaki 4  | bot right
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                vertices.AddRange(planeT(size, size, -binary[j] * temp, baseY - tempLength, binary[i] * temp));
                vertices.AddRange(planeT(size, size, -binary[j] * temp, baseY - size, +binary[i] * temp));
            }
        }
        // [32 - 35] alas duduk
        vertices.AddRange(planeT(4 * size, 3 * size, 0, baseY, 0 - (size * 0.5f)));

        //[36 - 39] sandaran
        vertices.AddRange(planeT(4 * size, size, 0, baseY + tempLength, 0 + (size * 0.5f) + size));
        mesh.SetVertices(vertices);

        //----shader----
        var colors = new Color32[vertices.Count];
        for (int i = 0; i < 40; i++)
        {
            byte valueR = (byte)(Random.Range(0.0f, 1.0f) * 255);
            byte valueB = (byte)(Random.Range(0.0f, 1.0f) * 255);
            byte valueG = (byte)(Random.Range(0.0f, 1.0f) * 255);
            colors[i] = new Color32(valueR, valueG, valueB, 255);
        }

        //----triangle----

        //kaki kursi
        for (int i = 0; i < 32; i += 8)
        {
            triangles.AddRange(connectTriangle(i + 1, i + 0, i + 3, i + 2));//bawah

            triangles.AddRange(connectTriangle(i + 6, i + 7, i + 2, i + 3));//depan
            triangles.AddRange(connectTriangle(i + 5, i + 4, i + 1, i + 0));//belakang

            triangles.AddRange(connectTriangle(i + 7, i + 5, i + 3, i + 1));//kanan
            triangles.AddRange(connectTriangle(i + 4, i + 6, i + 0, i + 2));//kiri
        }

        //alas duduk
        triangles.AddRange(connectTriangle(32, 33, 34, 35));//atas 
        triangles.AddRange(connectTriangle(22, 31, 4, 13));//bawah 

        triangles.AddRange(connectTriangle(34, 35, 22, 31));//depan 
        triangles.AddRange(connectTriangle(32, 34, 4, 22));//kiri 
        triangles.AddRange(connectTriangle(35, 33, 31, 13));//kanan 

        //sandaran
        triangles.AddRange(connectTriangle(36, 37, 38, 39));//atas

        triangles.AddRange(connectTriangle(38, 39, 32, 33));//depan
        triangles.AddRange(connectTriangle(37, 36, 13, 4));//belakang
        triangles.AddRange(connectTriangle(36, 38, 4, 32));//kiri
        triangles.AddRange(connectTriangle(39, 37, 33, 13));//kanan

        mesh.SetColors(colors);
        mesh.SetTriangles(triangles, 0);

        return mesh;
    }
}
