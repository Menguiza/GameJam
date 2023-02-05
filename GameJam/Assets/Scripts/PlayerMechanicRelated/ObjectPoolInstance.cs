using UnityEngine;

public class ObjectPoolInstance : MonoBehaviour
{
    private byte poolIndex;
    private Gun gun;
    public Rigidbody2D rb;
    
    public void SetPool(byte index, Gun storage)
    {
        poolIndex = index;
        gun = storage;
    }

    public void Return()
    {
        SoundManager.soundManager.PlaySnapShot(4);
        gun.Return(this, poolIndex);
    }

    internal void Prepare()
    {
        gameObject.SetActive(true);
    }

    internal void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground"))
        {
            Return();
        }

        if (col.collider.CompareTag("Enemy"))
        {
            col.collider.GetComponent<IEnemy>().RecieveDamage(20);
            Return();
        }
    }
}
