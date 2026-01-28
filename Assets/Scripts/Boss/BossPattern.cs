using System.Collections;
using UnityEngine;

public abstract class BossPattern : MonoBehaviour
{
    public abstract IEnumerator Execute(BossAttack attack);
}
