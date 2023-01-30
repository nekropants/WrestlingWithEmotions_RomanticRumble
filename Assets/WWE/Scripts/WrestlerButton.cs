using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using WWE;

public class WrestlerButton : MonoBehaviour
{

    Sprite defaultSprite;
    public Sprite mouseOverSprite;
    public Sprite selectedSprite;

    public SpriteRenderer sprite;
    public SpriteRenderer placement;

    public Sprite first;
    public Sprite second;
    public Sprite third;

    public int placementNumber = -1;

    public WrestlerInfo wrestler;
    public Characters character;


    public void Setup(Characters _character, Sprite _defaultSprite, Sprite _mouseOverSprite, Sprite _selectedSprite)
    {
        character = _character;
        defaultSprite = _defaultSprite;
        mouseOverSprite = _mouseOverSprite;
        selectedSprite = _selectedSprite;
        sprite.sprite = defaultSprite;

        print(character + "   " + defaultSprite+ "   " + mouseOverSprite+ "   " + selectedSprite) ;
    }

    // Use this for initialization
    void Start ()
    {
        //defaultSprite = sprite.sprite;
    }

    private bool selected= false;
	
	// Update is called once per frame


    void Update()
    {
        switch (placementNumber)
        {
            case 0:
                placement.sprite = first;
                placement.enabled = true;
                break;

            case 1:
                placement.sprite = second;
                placement.enabled = true;
                break;

            case 2:
                placement.sprite = third;
                placement.enabled = true;
                break;



            default:
                placement.enabled = false;
                break;
        }

    }

    void OnMouseDown()
    {
        if(Portraits.selectedWrestlers)
            return;

        selected = !selected;
        sprite.sprite = selected ? selectedSprite : defaultSprite;

        Portraits.instance.ToggleWrestler(this);
    }

    void OnMouseEnter()
    {
        if (Portraits.selectedWrestlers)
            return;

        if (selected == false)
        {

            sprite.sprite = mouseOverSprite;
        }
        FinalDecisionForm.instance.Show(wrestler.formAnswers, wrestler.name, wrestler.myStickers, wrestler.index);

    }

    public void Disappoint()
    {
        sprite.sprite = defaultSprite;

    }



    void OnGUI()
{
    if(selected && Application.isEditor)
    {
        // GUILayout.Label("");
        // GUILayout.Label(wrestler.opinion + "");
    }
}

    void OnMouseExit()
    {
        if (selected == false)
        {
            sprite.sprite = defaultSprite;
        }

     //   FinalDecisionForm.instance.Hide();

    }




}
