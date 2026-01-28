using System.Collections;
using UnityEngine;

public class PatternSpiralShot : BossPattern
{
    [SerializeField] private int bulletPrefabIndex;
    [SerializeField] private int bulletCount;
    [SerializeField] private int patternCount;
    [SerializeField] private float patternInterval;   

    [SerializeField] private float startAngle = 0f;    
    [SerializeField] private float angleStep;
    [SerializeField] private float fireInterval;

    public override IEnumerator Execute(BossAttack attack)
    {
        float angle = startAngle;

        for(int i=0; i< patternCount; i++)
        {
            for (int j = 0; j < bulletCount; j++)
            {
                Vector3 origin = transform.position;

                Quaternion rot = Quaternion.Euler(0f, 0f, angle);
                attack.Fire(bulletPrefabIndex, origin, rot);

                angle += angleStep;

                yield return new WaitForSeconds(fireInterval);
            }
            yield return new WaitForSeconds(patternInterval);
        }
    }
}
