using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewTurretData", menuName = "Data/TurretData")]

public class TurretData : ScriptableObject
{
    public GameObject bulletPrefab;
    public float reloadDelay;
    public BulletData bulletData;
}
