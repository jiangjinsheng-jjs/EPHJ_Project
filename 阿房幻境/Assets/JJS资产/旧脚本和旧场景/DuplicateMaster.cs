using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicateMaster : MonoBehaviour
{
    // Start is called before the first frame update
    

    //public Vector3 P;
    private float x1;
    private float y1;
    private float z1;
    Vector3 n;
    float a;
    float b;
    float c;
    float d;
    public GameObject cube;
    public GameObject cubeDup;
    public GameObject cubeMi;
    //private Vector3 cTrans;
    Vector3 Up = new Vector3(0, 1.0f, 0);
    Vector3 Foward = new Vector3(1.0f, 0, 0);
    //定义方向向量
    Vector3 MP1;
    Vector3 MPX1;
    Vector3 MPY1;
    Vector3 MP2;
    Vector3 MPX2;
    Vector3 MPY2;

    private LineRenderer line1;
    private LineRenderer line2;

    void Start()
    {
        a = 0;
        b = 0;
        c = 1;
        
        /*
        line1 = this.gameObject.AddComponent<LineRenderer>();
        line1.material = new Material(Shader.Find("Particles/Additive"));
        line1.SetVertexCount(2);//设置两点
        line1.SetColors(Color.yellow, Color.red); //设置直线颜色
        line1.SetWidth(0.001f, 0.001f);//设置直线宽度
        
        line2 = this.gameObject.AddComponent<LineRenderer>();
        line2.material = new Material(Shader.Find("Particles/Additive"));
        line2.SetVertexCount(2);//设置两点
        line2.SetColors(Color.yellow, Color.red); //设置直线颜色
        line2.SetWidth(0.001f, 0.001f);//设置直线宽度
        */
        
    }


    void Update()
    {

        getMeshPoint();

        //抽象平面,并计算单位法向量

        x1 = cube.transform.position.x;
        y1 = cube.transform.position.y;
        z1 = cube.transform.position.z;
        
        
        float D = Mathf.Abs((a * x1 + b * y1 + c * z1 + d) / (Mathf.Sqrt(Mathf.Pow(a, 2) + Mathf.Pow(b, 2) + Mathf.Pow(c, 2))));
        Debug.Log("D" + D);
        //计算距离
        MP1 = new Vector3(x1, y1, z1);
        MP2 = MP1+2*D*n;
        //计算P2位置

    
        MPY1 = MP1 + Up;
        float D2 = Mathf.Abs((a * MPY1.x + b * MPY1.y + c * MPY1.z + d) / (Mathf.Sqrt(Mathf.Pow(a, 2) + Mathf.Pow(b, 2) + Mathf.Pow(c, 2))));
        MPY2 = MPY1 + 2 * D2 * n;
        Vector3 Up2 = MPY2 - MPY1;
        //计算出对称向量1

        //float angle = Vector3.Angle(Up, Up2);

        /*
        MPX1 = MP1 + Foward;
        float D3 = Mathf.Abs((a * MPX1.x + b * MPX1.y + c * MPX1.z + d) / (Mathf.Sqrt(Mathf.Pow(a, 2) + Mathf.Pow(b, 2) + Mathf.Pow(c, 2))));
        MPX2 = MPX1 + 2 * D3 * n;
        Vector3 Foward2 = MPX2 - MPX1;
        //计算出对称向量2
        */

        //DebugLine(line2,MP2, MPY2);

        cubeMi.transform.position = MP2;
        //cubeMi.transform.Rotate(angle);
        cube.transform.LookAt(MPY1, Up);
        cubeMi.transform.LookAt( MPY2,Up);
        Quaternion RoatataAn = cubeMi.transform.rotation;
        if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(cubeDup, MP2, RoatataAn);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            Vector3 MyRotateAngle= transform.rotation.eulerAngles;
            MyRotateAngle.z--;
            transform.eulerAngles = MyRotateAngle;

        }

        if (Input.GetKey(KeyCode.E))
        {
            Vector3 MyRotateAngle = transform.rotation.eulerAngles;
            MyRotateAngle.z++;
            transform.eulerAngles = MyRotateAngle;

        }

        if (Input.GetKey(KeyCode.Z))
        {
            Vector3 MyRotateAngle = transform.rotation.eulerAngles;
            MyRotateAngle.x--;
            transform.eulerAngles = MyRotateAngle;

        }

        if (Input.GetKey(KeyCode.C))
        {
            Vector3 MyRotateAngle = transform.rotation.eulerAngles;
            MyRotateAngle.x++;
            transform.eulerAngles = MyRotateAngle;

        }
    }



    void getMeshPoint()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        Vector3[] vertices = mesh.vertices;

        for (int i = 0; i < 30; i=i+8)
        {
            Debug.Log("PPP"+"vertices  " + i + "     " + vertices[i]);
            
            vertices[i] = transform.TransformPoint(vertices[i]);
            Debug.Log("vertices  "+i+"     " + vertices[i]);
        }

        Vector3 p1 = vertices[0];
        Vector3 p2 = vertices[8];
        Vector3 p3 = vertices[16];
        Debug.Log("p1  " + p1);
        Debug.Log("p2  " + p2);
        Debug.Log("p3  " + p3);


        a = (p2.y - p1.y) * (p3.z - p1.z) - (p2.z - p1.z) * (p3.y - p1.y);
        
        b = ((p2.z - p1.z) * (p3.x - p1.x) - (p2.x - p1.x) * (p3.z - p1.z));

        c =  ((p2.x - p1.x) * (p3.y - p1.y) - (p2.y - p1.y) * (p3.x - p1.x));

        d = -(a * p1.x + b * p1.y + c * p1.z);
        Debug.Log("a b c   " + a+"  "+b + "  " + c);

        //抽象平面
        
        n = new Vector3(a, b, c);
        //计算法向量
        
        n.Normalize();
        Debug.Log("Nn  " + n);
    }




    void DebugLine(LineRenderer line, Vector3 initPosition,Vector3 newPosition)
    {
        //画线
        line = this.gameObject.GetComponent<LineRenderer>();
        //只有设置了材质 setColor才有作用
        

        //设置指示线的起点和终点
        line.SetPosition(0, initPosition);
        line.SetPosition(1, newPosition);
    }

}
