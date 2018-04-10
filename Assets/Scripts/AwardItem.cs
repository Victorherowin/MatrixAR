using UnityEngine;
using System.Collections;

public enum AwardType
{
    Gun,
    DualSword
}


public class AwardItem : MonoBehaviour
{
    public AwardType type;
    public float speed = 8;
    private bool startMove = false;
    private Transform player;
    public AudioClip pickupClip;

    void Start()
    {
        //GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        Vector3 position = new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3));
        transform.localPosition = position;
    }

    void Update()
    {
        if (startMove)
        {
        transform.position = Vector3.Lerp(transform.position, player.position + Vector3.up, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, player.position + Vector3.up) < 0.5f)
            {
                //Debug.unityLogger.Log(2);
                //TODO:没有加武器
                
                player.GetComponent<PlayerAward>().GetAward(type);
                //Debug.unityLogger.Log(1);
                //Destroy(this.gameObject);
                Destroy(gameObject);
         // AudioSource.PlayClipAtPoint(pickupClip, transform.position, 1f);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == Tags.ground)
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;
            //GetComponent<Rigidbody>().velocity = Vector3.zero;
            SphereCollider col = this.GetComponent<SphereCollider>();
            col.isTrigger = true;
            col.radius = 2;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == Tags.player)
        {
            startMove = true;
            player = col.transform;
        }
    }
}
