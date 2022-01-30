using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SimplifiedTest : MonoBehaviourPunCallbacks, IPunObservable
{
    public float movementX;
    public float movementY;
    Rigidbody2D rb;
    public float movSpeed;
    public bool isPositive;
    PhotonView PV;
    MeshRenderer meshRenderer;
    public Material blue;
    public Material red;
    public MeshRenderer childMeshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PV = GetComponent<PhotonView>();
        meshRenderer = GetComponent<MeshRenderer>();
        childMeshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
        if (!PV.IsMine)
        {
            Destroy(transform.parent.GetComponentInChildren<Camera>().gameObject);
            Destroy(transform.parent.GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>().gameObject);
            Destroy(rb);
        }
    }
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(isPositive);
        }
        else if (stream.IsReading)
        {
            isPositive = (bool)stream.ReceiveNext();
        }
    }
    // Update is called once per frame
    void Update()
    {
        MagnetColor();
        if (!PV.IsMine)
        {
            return;
        }
        PlayerInput();
        
    }
    private void FixedUpdate()
    {
        if (!PV.IsMine)
        {
            return;
        }
        
    }
    void PlayerInput()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(movementX * movSpeed, movementY * movSpeed);
        if (Input.GetButtonDown("Jump"))
        {
            PV.RPC("RPC_MagnetColor", RpcTarget.All);
        }
    }
    void MagnetColor()
    {
        
    }
    [PunRPC]
    void RPC_MagnetColor()
    {
        GetComponent<Magnetic>().toggle();
        isPositive = !isPositive;
        if (!isPositive)
        {
            meshRenderer.material = blue;
            childMeshRenderer.material = blue;
        }
        else if (isPositive)
        {
            meshRenderer.material = red;
            childMeshRenderer.material = red;
        }
    }
}
