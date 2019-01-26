using UnityEngine.EventSystems;
using UnityEngine;
//using CodeMonkey_TestMouseOverUI_Final;
//using CodeMonkey.Utils;

public class GameHandler : MonoBehaviour
{
    //public UnitHandler unitHandler;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsMouseOverUI())
        {
            //SetUnitTargetPosition(UtilsClass.GetMouseWorldPositionZeroZ());
        }
    }



    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    

    //Unit Functions
    private void SetUnitTargetPosition(Vector3 targetPositin)
    {
        //unitHandler.SetTargetPosition(targetPosition);
    }
}