using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meja : MonoBehaviour
{
    [SerializeField]
    public Material cubeMaterial;
    public Texture myTexture;
    

    float y = 0.0f;
    static float tableWidth = 2.0f;

    float tableLegLength = 0.5f;
    float tableLegHeight = 2.0f;
    float tableInnerPoint = tableWidth / 2;
    float tableThickness = 0.5f;
 
    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = new Mesh();
        var vertices = new Vector3[40];
        cubeMaterial.mainTexture = myTexture;
        var uvs = new Vector2[vertices.Length];
        int j = 0;
   
        float LegHeight = y + tableLegHeight;
        float LegLength = tableInnerPoint + tableLegLength;
        float chairSeatHeight = LegHeight + tableThickness;

        //first leg
        vertices[0] = new Vector3(tableInnerPoint, y, tableInnerPoint);
        vertices[1] = new Vector3(LegLength, y, tableInnerPoint);
        vertices[2] = new Vector3(tableInnerPoint, y, LegLength);
        vertices[3] = new Vector3(LegLength, y, LegLength);

        vertices[4] = new Vector3(tableInnerPoint, LegHeight, tableInnerPoint);
        vertices[5] = new Vector3(LegLength, LegHeight, tableInnerPoint);
        vertices[6] = new Vector3(tableInnerPoint, LegHeight, LegLength);
        vertices[7] = new Vector3(LegLength, LegHeight, LegLength);

        uvs[j++] = new Vector2(0f,1f);
        uvs[j++] = new Vector2(1f,1f);
        uvs[j++] = new Vector2(0f,1f);
        uvs[j++] = new Vector2(1f,0f);
        uvs[j++] = new Vector2(0f,1f);
        uvs[j++] = new Vector2(1f,1f);
        uvs[j++] = new Vector2(0f,1f);
        uvs[j++] = new Vector2(1f,0f);

        //second leg
        vertices[8] = new Vector3(-tableInnerPoint, y, tableInnerPoint);
        vertices[9] = new Vector3(-LegLength, y, tableInnerPoint);
        vertices[10] = new Vector3(-tableInnerPoint, y, LegLength);
        vertices[11] = new Vector3(-LegLength, y, LegLength);

        vertices[12] = new Vector3(-tableInnerPoint, LegHeight, tableInnerPoint);
        vertices[13] = new Vector3(-LegLength, LegHeight, tableInnerPoint);
        vertices[14] = new Vector3(-tableInnerPoint, LegHeight, LegLength);
        vertices[15] = new Vector3(-LegLength, LegHeight, LegLength);

        uvs[j++] = new Vector2(0f,1f);
        uvs[j++] = new Vector2(1f,1f);
        uvs[j++] = new Vector2(0f,1f);
        uvs[j++] = new Vector2(1f,0f);
        uvs[j++] = new Vector2(0f,1f);
        uvs[j++] = new Vector2(1f,1f);
        uvs[j++] = new Vector2(0f,1f);
        uvs[j++] = new Vector2(1f,0f);

        // third leg
        vertices[16] = new Vector3(tableInnerPoint, y, -tableInnerPoint);
        vertices[17] = new Vector3(LegLength, y, -tableInnerPoint);
        vertices[18] = new Vector3(tableInnerPoint, y, -LegLength);
        vertices[19] = new Vector3(LegLength, y, -LegLength);

        vertices[20] = new Vector3(tableInnerPoint, LegHeight, -tableInnerPoint);
        vertices[21] = new Vector3(LegLength, LegHeight, -tableInnerPoint);
        vertices[22] = new Vector3(tableInnerPoint, LegHeight, -LegLength);
        vertices[23] = new Vector3(LegLength, LegHeight, -LegLength);

        uvs[j++] = new Vector2(0f,1f);
        uvs[j++] = new Vector2(1f,1f);
        uvs[j++] = new Vector2(0f,1f);
        uvs[j++] = new Vector2(1f,0f);
        uvs[j++] = new Vector2(0f,1f);
        uvs[j++] = new Vector2(1f,1f);
        uvs[j++] = new Vector2(0f,1f);
        uvs[j++] = new Vector2(1f,0f);

        //fourth leg
        vertices[24] = new Vector3(-tableInnerPoint, y, -tableInnerPoint);
        vertices[25] = new Vector3(-LegLength, y, -tableInnerPoint);
        vertices[26] = new Vector3(-tableInnerPoint, y, -LegLength);
        vertices[27] = new Vector3(-LegLength, y, -LegLength);

        vertices[28] = new Vector3(-tableInnerPoint, LegHeight, -tableInnerPoint);
        vertices[29] = new Vector3(-LegLength, LegHeight, -tableInnerPoint);
        vertices[30] = new Vector3(-tableInnerPoint, LegHeight, -LegLength);
        vertices[31] = new Vector3(-LegLength, LegHeight, -LegLength);

        uvs[j++] = new Vector2(0f,1f);
        uvs[j++] = new Vector2(1f,1f);
        uvs[j++] = new Vector2(0f,1f);
        uvs[j++] = new Vector2(1f,0f);
        uvs[j++] = new Vector2(0f,1f);
        uvs[j++] = new Vector2(1f,1f);
        uvs[j++] = new Vector2(0f,1f);
        uvs[j++] = new Vector2(1f,0f);
        //chair seat
        vertices[32] = new Vector3(-LegLength, chairSeatHeight, -LegLength);
        vertices[33] = new Vector3(LegLength, chairSeatHeight, -LegLength);
        vertices[34] = new Vector3(LegLength, chairSeatHeight, LegLength);
        vertices[35] = new Vector3(-LegLength, chairSeatHeight, LegLength);

        uvs[j++] = new Vector2(0.5f, 1f);
        uvs[j++] = new Vector2(0.75f, 1f);
        uvs[j++] = new Vector2(0.5f, 0.5f);
        uvs[j++] = new Vector2(0.75f, 0.5f);

        mesh.vertices = vertices;
        mesh.uv = uvs;

        mesh.triangles = new int[]{
            //first leg
            0,4,5,
            0,5,1,
            1,7,3,
            1,5,7,
            0,1,3,
            0,3,2,
            3,6,2,
            3,7,6,
            2,4,0,
            2,6,4,
            4,7,5,
            4,6,7,
            //second leg
            9,8,10,
            9,10,11,
            9,12,8,
            9,13,12,
            8,14,10,
            8,12,14,
            10,15,11,
            10,14,15,
            11,13,9,
            11,15,13,
            13,14,12,
            13,15,14,
            //third leg
            18,23,19,
            18,22,23,
            19,21,17,
            19,23,21,
            17,20,16,
            17,21,20,
            16,22,18,
            16,20,22,
            18,19,17,
            18,17,16,
            22,21,23,
            22,20,21,
            //fourth leg
            27,30,26,
            27,31,30,
            26,28,24,
            26,30,28,
            24,29,25,
            24,28,29,
            25,31,27,
            25,29,31,
            27,24,25,
            27,26,24,
            31,28,30,
            31,29,28,
            //chair seat
            32,34,33,
            32,35,34,
            31,7,15,
            31,23,7,
            31,33,23,
            31,32,33,
            23,34,7,
            23,33,34,
            15,32,31,
            15,35,32,
            35,15,7,
            35,7,34
        };

        mesh.RecalculateNormals();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().material = cubeMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}