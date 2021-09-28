using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeOneMauriceMarvin : MonoBehaviour
{
    float size = 1.0f; //ukuran Icosahedron

    //kalkulasi sin dan cos
    double cosCal(double rotation)
    {
        var radians = Math.PI * rotation / 180.0;
        var cos = Math.Round(Math.Cos(radians), 2);
        return cos;
    }

    double sinCal(double rotation)
    {
        var radians = Math.PI * rotation / 180.0;
        var sin = Math.Round(Math.Sin(radians), 2);
        return sin;
    }
    double rotateX(double cos, double sin, double x, double y) //rotasi koordinat X
    {
        return (x * cos) - (y * sin);
    }

    double rotateY(double cos, double sin, double x, double y) //rotasi koordinat Y
    {
        return (x * sin) + (y * cos);
    }

    // Start is called before the first frame update
    void Start()
    {
        //pengaruh pada lokasi pada Z Axis, agar Z nya ditengah (0)
        float thick = (float)(Math.Sqrt(Math.Pow(size, 2) + Math.Pow(size, 2)))/-2; 

        Mesh mesh = new Mesh();
        var vertices = new Vector3[12]; //vertices
        float twidth = size; //calculated width
        float theight = size; //calculated height
        float tthick = thick; //calculated thickness
        float temp;//temporary variable
        int i, j, ver = 0;

        //berdasarkan sudut pada lingkaran/5 (segi 5) = 360/5 => 72 degree
        double cos72 = cosCal(72);
        double sin72 = sinCal(72);

        //berdasarkan titik tengah diantara sudut 72/2 = 36
        double cos36 = cosCal(36);
        double sin36 = sinCal(36);

        //pentagon 1
        j = ver + 5;
        for (i = ver; i < j; i++, ver++)
        {
            vertices[i] = new Vector3(twidth, theight, tthick);
            temp = twidth;
            twidth = (float)rotateX(cos72, sin72, (double)twidth, (double)theight);
            theight = (float)rotateY(cos72, sin72, (double)temp, (double)theight);
        }

        //panjang sisi segitiga
        double edgeLength = Math.Sqrt(
            Math.Pow(((double)twidth - (double)vertices[1].x), 2) + Math.Pow(((double)theight - (double)vertices[1].y), 2)
        );
        //jarak antara pentagon 1 dan 2
        tthick = ((float)(Math.Sqrt(3) / 2 * edgeLength)) + thick;
        //Rotasi untuk mendapatkan titik tengah antara 2 koordinat
        temp = twidth;
        twidth = (float)rotateX(cos36, sin36, (double)twidth, (double)theight);
        theight = (float)rotateY(cos36, sin36, (double)temp, (double)theight);

        //pentagon 2
        j = ver + 5;
        for (i = ver; i < j; i++, ver++)
        {
            vertices[i] = new Vector3(twidth, theight, tthick);
            temp = twidth;
            twidth = (float)rotateX(cos72, sin72, (double)twidth, (double)theight);
            theight = (float)rotateY(cos72, sin72, (double)temp, (double)theight);
        }

        //jarak antara titik (0,0) dengan titik pertama (width, height)
        double baseLength = Math.Sqrt(
            Math.Pow(((double)size - 0), 2) + Math.Pow(((double)size - 0), 2)
        );
        //jarak antara titik kutub utara/selatan dengan pentagon 2/1
        tthick = (float)Math.Sqrt(Math.Pow(edgeLength, 2) - Math.Pow(baseLength, 2));

        //titik kutub utara dan selatan
        vertices[ver++] = new Vector3(0, 0, (vertices[0].z - tthick));//selatan (dekat dengan pentagon 1) (-Z)
        vertices[ver] = new Vector3(0, 0, (vertices[5].z + tthick));//utara (dekat dengan pentagon 2) (+Z)

        mesh.vertices = vertices;

        mesh.triangles = new int[]{
            // //pentagon 1 (guide)
            // 0, 4, 3,
            // 3, 2, 1,
            // 1, 0, 3,
            // //pentagon 2 (guide)
            // 5, 6, 7,
            // 7, 8, 9,
            // 9, 5, 7,

            //samping
            0, 5, 9,
            9, 4, 0,
            9, 8, 4,
            4, 8, 3,
            3, 8, 7,
            3, 7, 2,
            2, 7, 6,
            2, 6, 1,
            1, 6, 5,
            1, 5, 0,

            //kutub pentagon1
            10, 0, 4,
            10, 4, 3,
            10, 3, 2,
            10, 2, 1,
            10, 1, 0,

            //kutub pentagon2
            11, 5, 6,
            11, 6, 7,
            11, 7, 8,
            11, 8, 9,
            11, 9, 5
        };
        GetComponent<MeshFilter>().mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
