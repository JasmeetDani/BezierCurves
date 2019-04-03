using UnityEngine;
using UnityEngine.UI.Extensions;

public class BezierRenderer : MonoBehaviour
{
    [SerializeField]
    private UILineRenderer line;


    private BezierCurve curve;

    [SerializeField]
    private Transform[] controlPoints;


    void Start()
    {
        float x1 = 290, y1 = 290, x2 = 40, y2 = 100, x3 = 200, y3 = 340, x4 = 300, y4 = 450;
        
        controlPoints[0].position = new Vector2(x1, y1);
        controlPoints[1].position = new Vector2(x2, y2);
        controlPoints[2].position = new Vector2(x3, y3);
        controlPoints[3].position = new Vector2(x4, y4);

        CreateCurve();
       

        DragController.PointMoved += ()=> { CreateCurve(); };
    }


    void CreateCurve()
    {
        curve = new BezierCurve(controlPoints[0].position.x,
                               controlPoints[1].position.x,
                               controlPoints[2].position.x,
                               controlPoints[3].position.x,
                               controlPoints[0].position.y,
                               controlPoints[1].position.y,
                               controlPoints[2].position.y,
                               controlPoints[3].position.y);
        curve.Display(line);
    }
}