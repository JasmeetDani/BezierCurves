
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class BezierCurve
{
    public class BezierCurveSegment
    {
        public float x1, x2, x3, x4, y1, y2, y3, y4;

        public BezierCurveSegment next;


        public BezierCurveSegment() {}

        public BezierCurveSegment(float px1, float px2, float px3, float px4,
                                  float py1, float py2, float py3, float py4)
        {
            x1 = px1; x2 = px2; x3 = px3; x4 = px4;
            y1 = py1; y2 = py2; y3 = py3; y4 = py4;

            next = null;
        }
    }


    BezierCurveSegment first;


    public BezierCurve(float px1, float px2, float px3, float px4, float py1, float py2, float py3, float py4)
    {
        first = new BezierCurveSegment(px1, px2, px3, px4, py1, py2, py3, py4);
    }
    
    public void Display(UILineRenderer line)
    {
        BezierCurveSegment tmp = first;

        float[,] param = new float[4, 4];

        int nsteps = 50, i;

        float dt = .02f;
        float temp, temp1, ox, oy;

        List<Vector2> points = new List<Vector2>();

        while(tmp != null)
        {  
            param[0,0] = -tmp.x1 + 3 * tmp.x2 - 3 * tmp.x3 + tmp.x4;                    // ax
            param[0,1] = 3 * tmp.x1 - 6 * tmp.x2 + 3 * tmp.x3;                          // bx
            param[0,2] = -3 * tmp.x1 + 3 * tmp.x2;                                      // cx
            param[0,3] = tmp.x1;                                                        // dx
            param[1,0] = - tmp.y1 + 3 * tmp.y2 - 3 * tmp.y3 + tmp.y4;                   // ay
            param[1,1] = 3 * tmp.y1 - 6 * tmp.y2 + 3 * tmp.y3;                          // by
            param[1,2] = -3 * tmp.y1 + 3 *tmp.y2;                                       // cy
            param[1,3] = tmp.y1;                                                        // dy
            temp = dt * dt;
            temp1 = temp * dt;
            param[2,0] = param[0,3];                                                    // x
            param[2,1] = param[0,0] * temp1 + param[0,1] * temp+param[0,2] * dt;        // d1x
            param[2,2] = 6 * param[0,0] * temp1 + 2 * param[0,1] * temp;                // d2x
            param[2,3] = 6 * param[0,0] * temp1;                                        // d3x
            param[3,0] = param[1,3];                                                    // y
            param[3,1] = param[1,0] * temp1 + param[1,1] * temp + param[1,2] * dt;      // d1y
            param[3,2] = 6 * param[1,0] * temp1 + 2 * param[1,1] * temp;                // d2y
            param[3,3] = 6 * param[1,0] * temp1;                                        // d3y
           
            for(i = 0;i < nsteps;i++)
            {
                ox = param[2, 0]; oy = param[3,0];

                param[2, 0] += param[2, 1]; param[2, 1] += param[2, 2]; param[2,2]+=param[2,3];
                param[3, 0] += param[3, 1]; param[3, 1] += param[3, 2]; param[3,2]+=param[3,3];

                points.Add(new Vector2(ox, oy));
                points.Add(new Vector2(param[2,0], param[3,0]));
            }

            tmp = tmp.next;
        }
        
        line.Points = points.ToArray();
    }
}