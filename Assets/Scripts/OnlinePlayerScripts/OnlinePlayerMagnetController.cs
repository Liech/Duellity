using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using Photon.Pun;

public class OnlinePlayerMagnetController : MonoBehaviourPunCallbacks, IPunObservable
{
    public KeyCode key = KeyCode.Space;
    Magnetic magnet;
    MagneticType oldType;
    MeshRenderer mr;
    //MagneticForce
    public float MaxStrength = 0.05f;
    public float MinStrength = 0.01f;
    public float MaxRange = 5;
    public bool DrawGizmo = true;
    public AnimationCurve curve;
    //MagnetImpulse
    public float cooldownDuration = 2;
    public KeyCode activationKey = KeyCode.E;

    public bool FollowCharacter = true;
    public float range = 5;
    public float impulseFactor = 1.5f;
    public float impulseAdd = 0.5f;
    public float reflectPercentage = 0.5f;
    public float directPercentage = 0.5f;
    private bool ready = true;
    private Rigidbody2D rb;
    PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        mr = GetComponentInChildren<MeshRenderer>();
        magnet = GetComponent<Magnetic>();
        oldType = MagneticType.Undefined;
    }
    public void ToggleMagnet(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        magnet.toggle();//_playerInputDirection=ctx.ReadValue<Vector2>();
    }
    // Update is called once per frame
    void Update()
    {
        //Materialswap
        if (GetComponent<MeshRenderer>())
            mr = GetComponent<MeshRenderer>();
        else if (transform.childCount > 0 && transform.GetChild(0).GetComponent<MeshRenderer>())
            mr = transform.GetChild(0).GetComponent<MeshRenderer>();

        if (mr && GetComponent<Magnetic>().MagnetType != oldType)
        {
            if (GetComponent<Magnetic>().MagnetType == MagneticType.Blue)
            {
                mr.material = GameManagr.Instance.BlueMaterial;
                oldType = MagneticType.Blue;
            }
            else
            {
                mr.material = GameManagr.Instance.RedMaterial;
                oldType = MagneticType.Red;
            }

        }
        //MagneticForce
        foreach (var obj in GameObject.FindObjectsOfType<AffectedFromMagnetic>())
        {
            var diff = obj.transform.position - transform.position;
            if (diff.magnitude > MaxRange)
                continue;

            float magneticDir = (GetComponent<Magnetic>().MagnetType == obj.GetComponent<Magnetic>().MagnetType) ? -1 : 1;
            var time = (diff.magnitude / MaxRange);
            var value = curve.Evaluate(time);

            var strength = (value - 1) * MaxStrength + (value) * MinStrength;
            var dir = diff.normalized;
            obj.GetComponent<Rigidbody2D>().AddForce(dir * strength * magneticDir);
        }
        if(!PV.IsMine)
        {
            return;
        }
        if (Input.GetButtonDown("Jump"))
        {
            PV.RPC("RPC_MagnetToggle", RpcTarget.All);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PV.RPC("RPC_FireImpulse", RpcTarget.All);
        }
    }
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            
        }
        else if (stream.IsReading)
        {
            
        }
    }
    public void FireImpulse()
    {
        
    }

    void impulse()
    {
        //SoundSingleton.instance.playImpulse();
        foreach (var obj in GameObject.FindObjectsOfType<AffectedFromMagnetic>())
        {
            var body = obj.GetComponent<Rigidbody2D>();
            var diff = obj.transform.position - transform.position;
            if (diff.magnitude > range)
                continue;

            float magneticDir = (GetComponent<Magnetic>().MagnetType == obj.GetComponent<Magnetic>().MagnetType) ? 1 : -1;
            Vector2 reflect = Vector2.Reflect(obj.GetComponent<Rigidbody2D>().velocity, -diff.normalized).normalized;
            Vector2 dir = diff.normalized;
            float speed = body.velocity.magnitude * impulseFactor + impulseAdd;
            var newVel = reflect * reflectPercentage * speed + directPercentage * speed * dir;
            obj.GetComponent<Rigidbody2D>().velocity = newVel * magneticDir;
        }

    }

    IEnumerator cooldown()
    {
        ready = false;
        yield return new WaitForSeconds(cooldownDuration);
        ready = true;
    }
    private void OnDrawGizmos()
    {
        if (ready)
            Gizmos.DrawWireSphere(transform.position, range);
        if (DrawGizmo)
        {
            Gizmos.DrawWireSphere(transform.position, MaxRange);
        }
    }
    [PunRPC]
    void RPC_MagnetToggle()
    {
        magnet.toggle();
    }
    [PunRPC]
    void RPC_FireImpulse()
    {
        if (!ready)
            return;
        StartCoroutine(cooldown());
        impulse();
        GameObject g;
        GameObject vorlage;
        if (GetComponent<Magnetic>().MagnetType == MagneticType.Blue)
            vorlage = GameManagr.Instance.ImpulseEffectBlue;// Instantiate(, transform.position, transform.rotation);
        else
            vorlage = GameManagr.Instance.ImpulseEffectRed;// Instantiate(, transform.position, transform.rotation);
        g = Instantiate(vorlage, transform.position, transform.rotation);
        if (FollowCharacter)
            g.transform.parent = transform;

        if (g)
        {
            var system = g.GetComponent<ParticleSystem>();
            var shape = system.sizeOverLifetime;
            float magicFudgeFactor = (5f / 2.43f);
            shape.sizeMultiplier = range * magicFudgeFactor;
        }
    }
}
