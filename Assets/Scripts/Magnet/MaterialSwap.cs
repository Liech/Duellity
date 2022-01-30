using UnityEngine;

[RequireComponent(typeof(Magnetic))]
public class MaterialSwap : MonoBehaviour
{
    private MagneticType _oldType;
    private const string MaterialsPath = "Materials/Characters/";

    private void Start()
    {
        _oldType = MagneticType.Undefined;
    }

    private void Update()
    {
        MeshRenderer renderer = null;
        if (GetComponent<MeshRenderer>())
            renderer = GetComponent<MeshRenderer>();
        else if (transform.childCount > 0 && transform.GetChild(0).GetComponent<MeshRenderer>())
            renderer = transform.GetChild(0).GetComponent<MeshRenderer>();

        if (GetComponent<Magnetic>().MagnetType != _oldType)
        {
            if (GetComponent<Magnetic>().MagnetType == MagneticType.Blue)
            {
                var characterSwapper = GetComponent<CharacterSwapper>();
                if (characterSwapper)
                {
                    var activeSkin = characterSwapper.GetActiveSkin();
                    changeMaterialsBlue(activeSkin);
                }
                else if (renderer)
                {
                    renderer.material = MagnetSingleton.instance.BlueMaterial;
                }
                else
                {
                    Debug.LogError("no magnetic material swap");
                }

                _oldType = MagneticType.Blue;
        if(transform.Find("PlayerColorIndication"))
        transform.Find("PlayerColorIndication").GetComponent<SpriteRenderer>().color=new Color(0, 0,255);

      }
      else
            {
                var characterSwapper = GetComponent<CharacterSwapper>();
                if (characterSwapper)
                {
                    var activeSkin = characterSwapper.GetActiveSkin();
                    changeMaterialsRed(activeSkin);
                }
                else if (renderer)
                {
                    renderer.material = MagnetSingleton.instance.RedMaterial;
                }
                else
                {
                    Debug.LogError("no magnetic material swap");
                }

                _oldType = MagneticType.Red;

        if(transform.Find("PlayerColorIndication"))
          transform.Find("PlayerColorIndication").GetComponent<SpriteRenderer>().color=new Color(255, 0, 0);
            }
        }
    }

    private void changeMaterialsBlue(GameObject activeSkin)
    {
        var child = activeSkin.transform.GetChild(0);
        var meshRenderer = child.GetComponent<SkinnedMeshRenderer>();
        var materials = meshRenderer.materials;
        for (var i = 0; i < materials.Length; i++) materials[i] = child.GetComponent<HasMagneticColors>().blue;

        meshRenderer.materials = materials;
    }

    private void changeMaterialsRed(GameObject activeSkin)
    {
        var child = activeSkin.transform.GetChild(0);
        var meshRenderer = child.GetComponent<SkinnedMeshRenderer>();
        var materials = meshRenderer.materials;
        for (var i = 0; i < materials.Length; i++) materials[i] = child.GetComponent<HasMagneticColors>().red;

        meshRenderer.materials = materials;
    }
    
}