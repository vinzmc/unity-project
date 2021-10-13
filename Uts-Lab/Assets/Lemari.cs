using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Lemari : MonoBehaviour
{
    //base size
    public float size = 1f;

    private MeshFilter filter;
    public Material cubeMaterial;
    public Texture myTexture;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshFilter>().mesh = GenerateMesh();
        cubeMaterial.mainTexture = myTexture;
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
        var uvs = new List<Vector2>();
        //depan
        vertices.AddRange(planeF(size, size, -0.5f, 1.5f, -0.5f));//1
        vertices.AddRange(planeF(size, size, 0.5f, 1.5f, -0.5f));//2
        vertices.AddRange(planeF(size, size, -0.5f, 0.5f, -0.5f));//3
        vertices.AddRange(planeF(size, size, 0.5f, 0.5f, -0.5f));//4

        //belakang
        vertices.AddRange(planeF(size, size, -0.5f, 1.5f, 0.5f));//5
        vertices.AddRange(planeF(size, size, 0.5f, 1.5f, 0.5f));//6
        vertices.AddRange(planeF(size, size, -0.5f, 0.5f, 0.5f));//7
        vertices.AddRange(planeF(size, size, 0.5f, 0.5f, 0.5f));//8

        //kiri
        vertices.AddRange(planeR(size, size, -1f, 1.5f, 0f));//9
        vertices.AddRange(planeR(size, size, -1f, 0.5f, 0f));//10

        //kanan
        vertices.AddRange(planeR(size, size, 1f, 1.5f, 0f));//11
        vertices.AddRange(planeR(size, size, 1f, 0.5f, 0f));//12

        //atas
        vertices.AddRange(planeT(size, size, -0.5f, 2f, 0f));//13
        vertices.AddRange(planeT(size, size, 0.5f, 2f, 0f));//14

        //bawah
        vertices.AddRange(planeT(size, size, -0.5f, 0f, 0f));//15
        vertices.AddRange(planeT(size, size, 0.5f, 0f, 0f));//16
        
        mesh.SetVertices(vertices);

        //--texture--
        float x = 0.5f;
        float y = 0.5f;
        float xI = 0.25f; //x increment
        float yI = 0.5f; //y increment
        //depan 1, 2, 3, 4
        for (int i = 0; i < 4; i++)
        {
            uvs.Add(new Vector2(x, y + yI));
            uvs.Add(new Vector2(x + xI, y + yI));
            uvs.Add(new Vector2(x, y));
            uvs.Add(new Vector2(x + xI, y));
            switch (i)
            {
                case (0):
                    x = 0.75f;
                    break;
                case (1):
                    x = 0.5f;
                    y = 0f;
                    break;
                case (2):
                    x = 0.75f;
                    break;
            }
        }

        //belakang, kiri, kanan, atas, bawah
        for (int i = 0; i < 12; i++)
        {
            uvs.Add(new Vector2(0f, 0.5f));
            uvs.Add(new Vector2(0.25f, 0.5f));
            uvs.Add(new Vector2(0f, 0f));
            uvs.Add(new Vector2(0.25f, 0f));
        }

        mesh.uv = uvs.ToArray();

        //----triangle----
        //depan 
        //[[1] [2]]
        //[[3] [4]]
        for (int i = 0, j = 0; i < 4; i++)
        {
            triangles.AddRange(connectTriangle(j++, j++, j++, j++));//normal
        }//last = 15

        //belakang
        for (int i = 0, j = 16; i < 4; i++, j+=4)
        {
            triangles.AddRange(connectTriangle(j+1, j, j+3, j+2));//kebalik
        }//last = 32

        //kiri
        for (int i = 0, j = 32; i < 2; i++, j+=4)
        {
            triangles.AddRange(connectTriangle(j+1, j, j+3, j+2));//kebalik
        }//last = 40

        //kanan
        for (int i = 0, j = 40; i < 2; i++)
        {
            triangles.AddRange(connectTriangle(j++, j++, j++, j++));//normal
        }//last = 48

        //atas
        for (int i = 0, j = 48; i < 2; i++)
        {
            triangles.AddRange(connectTriangle(j++, j++, j++, j++));//normal
        }//last = 56


        //bawah
        for (int i = 0, j = 56; i < 2; i++, j+=4)
        {
            triangles.AddRange(connectTriangle(j+1, j, j+3, j+2));//kebalik
        }//last = 64

        // mesh.SetColors(colors);
        mesh.SetTriangles(triangles, 0);
        mesh.RecalculateNormals();

        return mesh;
    }
}
