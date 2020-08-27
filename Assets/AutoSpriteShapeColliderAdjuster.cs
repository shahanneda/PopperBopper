using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[ExecuteInEditMode]
public class AutoSpriteShapeColliderAdjuster : MonoBehaviour

{
    private SpriteShapeController spriteShapeController;
    private PolygonCollider2D col;


    public float thicknessOfCollider = 1.0f;

    /// <summary>
    ///  this is to auto generate the collider of the sprite spline.
    /// </summary>
    void Start()
    {
        UpdateCollider();
    }
    void OnEnable() {
        UpdateCollider();
    }


    private void OnValidate()
    {
        UpdateCollider();
        
    }
    void UpdateCollider()
    {
        print("Updated Collider");
        spriteShapeController = GetComponent<SpriteShapeController>();
        col = GetComponent<PolygonCollider2D>();

        Spline spline = spriteShapeController.spline;
        //col.points = new Vector2[0];
        

        Vector2[] colPoints = new Vector2[spline.GetPointCount() * 2 ];

        int currentArrayPointIndex = 0; // since we want to put in the point in order, this will go up twice as fast as i 
        bool firstOne = true; // we need the points to be Lr rL, so in the second loop this wil lbe false
        for(int i = 0; i< spline.GetPointCount(); i++) {
            Vector3 pos = spline.GetPosition(i);

            Vector3 targetDir = spline.GetPosition(1) - spline.GetPosition(0);
            float angle = Vector3.Angle(targetDir.normalized, transform.right);
            angle = angle * Mathf.PI / 180;
            print(angle);
            print("sin " + Mathf.Sin(angle ));


            Vector2 point1 = new Vector3(pos.x + Mathf.Sin(angle)*thicknessOfCollider, pos.y + (Mathf.Cos(angle))*thicknessOfCollider);
            Vector2 point2 = new Vector3(pos.x - Mathf.Sin(angle)*thicknessOfCollider, pos.y - (Mathf.Cos(angle))*thicknessOfCollider);

            if (firstOne) { 
              colPoints[currentArrayPointIndex] = point1;
              colPoints[currentArrayPointIndex + 1] = point2;
            
            }
            else { 
              colPoints[currentArrayPointIndex] = point2;
              colPoints[currentArrayPointIndex + 1] = point1;
            
            }
            firstOne = !firstOne;

            currentArrayPointIndex += 2; // 2 since we put in 2 points
        }
       

        col.points = colPoints;
    }
}
