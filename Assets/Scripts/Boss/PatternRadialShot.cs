using System.Collections;
using UnityEngine;

public class PatternRadialShot : BossPattern
{
    [SerializeField] private int bulletPrefabIndex;
    [SerializeField] private int bulletCount;
    [SerializeField] private int patternCount;
    [SerializeField] private float patternInterval;
    public override IEnumerator Execute(BossAttack attack)
    {
        for(int i = 0; i < patternCount; i++)
        {
            Vector3 origin = transform.position;

            for (int j = 0; j < bulletCount; j++)
            {
                float angle = (360f / bulletCount) * j;
                Quaternion rot = Quaternion.Euler(0f, 0f, angle);
                attack.Fire(bulletPrefabIndex, origin, rot);
            }

            yield return new WaitForSeconds(patternInterval);
        }
    }
}
