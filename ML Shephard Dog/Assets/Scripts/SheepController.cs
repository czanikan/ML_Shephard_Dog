using System.Collections;
using UnityEngine;
using Unity.MLAgents;

public class SheepController : MonoBehaviour
{
    private Animator anim;
    private CharacterController cc;
    public Transform dogPosition;
    private AudioSource beeSFX;
    private Agent dogAgent;

    public float fearDistance = 7f;
    public float moveSpeed = 10f;
    public float rotSpeed = 10f;

    public bool canMove;

    void Start()
    {
        beeSFX = GetComponent<AudioSource>();
        fearDistance = Random.Range(4f, 7f);
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        StartCoroutine(SheepBee());

        canMove = true;
        dogAgent = dogPosition.gameObject.GetComponent<DogAgentController>();
    }

    void Update()
    {
        if (canMove)
        {
            if (Vector3.Distance(transform.position, dogPosition.position) <= fearDistance)
            {
                Vector3 lookinAt = -(dogPosition.position - transform.position);
                Quaternion lookinRot = Quaternion.LookRotation(lookinAt);
                lookinRot.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, lookinRot.eulerAngles.y, transform.rotation.eulerAngles.z);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookinRot, Time.deltaTime * rotSpeed);
                Vector3 moveDirection = transform.TransformDirection(Vector3.forward) * moveSpeed;
                cc.Move(moveDirection * Time.deltaTime);
                anim.SetBool("mustRun", true);

                dogAgent.AddReward(0.001f);
            }
            else
            {
                anim.SetBool("mustRun", false);
            }
        }
    }

    IEnumerator SheepBee()
    {
        yield return new WaitForSeconds(Random.Range(2, 30));
        beeSFX.pitch = Random.Range(0.90f, 1.1f);
        beeSFX.Play();
        StartCoroutine(SheepBee());
    }
}
