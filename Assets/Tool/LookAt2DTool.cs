using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LookAt2DTool 
{
    //使用eularAngle+=这个函数
    public static Vector3 LookAt2DAddRotation(Transform from, Vector3 to,float offSet=0)
    {
        float dx = to.x - from.transform.position.x;
        float dy = to.y - from.transform.position.y;
        float rotationZ = Mathf.Atan2(dy, dx) * 180 / Mathf.PI;
        rotationZ -= offSet;

        float originRotationZ = from.eulerAngles.z;
        float addRotationZ = rotationZ - originRotationZ;
        if (addRotationZ > 180)
            addRotationZ -= 360;
        if (addRotationZ < -180)
            addRotationZ += 360;

        return new Vector3(0, 0, addRotationZ);
    }

    //提供方向向量，而非世界坐标
    public static Vector3 LookTo2DWithDirectionAddRotation(Transform from, Vector3 direction, float offSet = 0)
    {
        direction += from.position;
        return LookAt2DAddRotation(from, direction, offSet);
    }

    //下面两个直接改旋转，一步到位
    public static void LookAt2DWithWorldPosition(Transform from,Vector3 to,float offset)
    {
        from.eulerAngles += LookAt2DAddRotation(from, to, offset);
    }
    public static void LookAt2DWithDirection(Transform from,Vector3 direciton,float offSet = 0)
    {
        from.eulerAngles += LookTo2DWithDirectionAddRotation(from, direciton, offSet);
    }
    //根据z轴旋转获得正前方的方向向量
    public static Vector2 GetFaceDirection(Transform trans,float offset = 90)
    {
        float z = trans.rotation.eulerAngles.z + offset;
        Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, z);
        Vector2 dir = rotation * Vector2.right;
        return dir;
    }
}
