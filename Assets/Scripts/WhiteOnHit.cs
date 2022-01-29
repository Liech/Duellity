using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class WhiteOnHit : MonoBehaviour {

  public float whiteDuration = 0.1f;
  private void Start() {
  }

  private void OnCollisionEnter2D(Collision2D collision) {
    if (collision.collider.gameObject.GetComponent<IsDangerous>())
      StartCoroutine(cooldown());
  }

  IEnumerator cooldown() {
    Material oldMaterial;
    MeshRenderer renderer = null;
    if(GetComponent<MeshRenderer>())
      renderer=GetComponent<MeshRenderer>();
    else if(transform.childCount>0&&transform.GetChild(0).GetComponent<MeshRenderer>())
      renderer=transform.GetChild(0).GetComponent<MeshRenderer>();

    if(renderer && renderer.sharedMaterial.name!= MagnetSingleton.instance.GotHitMaterial.name) {
      oldMaterial=renderer.sharedMaterial;
      renderer.sharedMaterial=MagnetSingleton.instance.GotHitMaterial;
      yield return new WaitForSeconds(whiteDuration);
      if(MagnetSingleton.instance.GotHitMaterial.name==renderer.sharedMaterial.name)
        renderer.material=oldMaterial;      
    }
  }

}
