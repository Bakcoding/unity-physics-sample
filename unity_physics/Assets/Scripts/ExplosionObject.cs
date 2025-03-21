using UnityEngine;

public class ExplosionObject : MonoBehaviour
{
    [SerializeField] float duration = 2f;

    private void OnEnable()
    {
        Destroy(gameObject, duration);
    }
}
