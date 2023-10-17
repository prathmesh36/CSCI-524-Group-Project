using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierFollow : MonoBehaviour
{
    [SerializeField]
    private Transform[] routes;
    private int routeToGo;
    private float tParam;
    private Vector2 BlackholePosition;
    private float speedModifier;
    private bool coroutioneAllowed;
    void Start()
    {
        routeToGo=0;
        tParam=0f;
        speedModifier=0.25f;
        coroutioneAllowed=true;
    }

    // Update is called once per frame
    void Update()
    {
        if(coroutioneAllowed)
            StartCoroutine(GoByTheRoute(routeToGo));
    }
    private IEnumerator GoByTheRoute(int routeNumber)
    {
        coroutioneAllowed=false;
        Vector2 p0=routes[routeNumber].GetChild(0).position;
        Vector2 p1=routes[routeNumber].GetChild(1).position;
        Vector2 p2=routes[routeNumber].GetChild(2).position;
        Vector2 p3=routes[routeNumber].GetChild(3).position;
        while(tParam<1){
            tParam+=Time.deltaTime*speedModifier;
            BlackholePosition=Mathf.Pow(1-tParam,3)*p0+
                3*Mathf.Pow(1-tParam,2)*tParam*p1+
                3*(1-tParam)*Mathf.Pow(tParam,2)*p2+
                Mathf.Pow(tParam,3)*p3;
            transform.position=BlackholePosition;
            yield return new WaitForEndOfFrame();
        }
        tParam=0f;
        routeToGo+=1;
        if(routeToGo>routes.Length-1)
            routeToGo=0;
        coroutioneAllowed=true;
    }
}
