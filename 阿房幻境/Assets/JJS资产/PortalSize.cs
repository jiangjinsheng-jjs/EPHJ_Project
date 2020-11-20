using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSize : MonoBehaviour
{
    public GameObject MyPlayer;
    public GameObject RefMirror;
        
    public GameObject GroundEffect;

   
    Vector3 lastPosition;
    Renderer rend;
    float timer = 0;
    bool SorM;

    float SSize;
    float RSize;
    Vector3 GSize;

    float Dlta;
    // Start is called before the first frame update
    void Start()
    {
        lastPosition = MyPlayer.transform.position = lastPosition;

        rend = GetComponent<Renderer>();
        //rend.material.shader = Shader.Find("Size");
    }

    // Update is called once per frame
    void Update()
    {
        SSize = rend.material.GetFloat("Size");
        RSize = RefMirror.GetComponent<Renderer>().material.GetFloat("Size");
        GSize = GroundEffect.transform.localScale;
        if (lastPosition.x < MyPlayer.transform.position.x + 0.001f &&
            lastPosition.x > MyPlayer.transform.position.x - 0.001f &&
            lastPosition.z < MyPlayer.transform.position.z + 0.001f &&
            lastPosition.z > MyPlayer.transform.position.z - 0.001f)
        {
            if (SorM)
            {
                timer = 0;
            }

            Debug.Log("S");
            timer += Time.deltaTime;
            
            if (SSize < 0.7f)
            {
                SSize += timer/10;
            }
            
            if (RSize < 0.7f)
            {
                RSize += timer / 10;
            }
            if (GSize.z < 0.3f)
            {

                GSize.z += timer / 10;
            }
            rend.material.SetFloat("Size", SSize);
            RefMirror.GetComponent<Renderer>().material.SetFloat("Size", RSize);
            GroundEffect.transform.localScale = GSize;
            SorM = false;
        }
        else
        {
            if (!SorM)
            {
                timer = 0;
            }
            Debug.Log("M");
            timer += Time.deltaTime;

            if (SSize > 0.3f)
            {
                SSize -= timer/10;
            }
            if (RSize > 0.3f)
            {
                RSize -= timer / 10;
            }
            if (GSize.z > 0)
            {

                GSize.z -= timer / 10;
            }
            rend.material.SetFloat("Size", SSize);
            RefMirror.GetComponent<Renderer>().material.SetFloat("Size", RSize);
            GroundEffect.transform.localScale=GSize;
            SorM = true;
        }
        
        lastPosition = MyPlayer.transform.position;
    }
}
