using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ultimate : MonoBehaviour
{
    public Transform shootingPoint;
    public Slider ultimateSlider;
    public float ultimateCharge = 0f;   
    private Coroutine ultimateCoroutine;
    void Start()
    {
        ultimateSlider.maxValue = 100f;
        ultimateSlider.value = ultimateCharge;
        StartCoroutine(UltimateAttack());
    }

    // Update is called once per frame

    [SerializeField] private Animator ultAnimator;

    private IEnumerator UltimateAttack()
    {
        while(ultimateCharge < ultimateSlider.maxValue)
        {
            ultimateCharge += 1f;
            ultimateSlider.value = ultimateCharge;
            yield return new WaitForSeconds(0.25f);
        }
    }

    void Update()
    {
        Image colorFill = ultimateSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>();
        if(ultimateSlider.value >= ultimateSlider.maxValue)
        {
            colorFill.color = Color.green;
        }
        else
        {
            colorFill.color = Color.yellow;
        }
        //ultimate press g
        if(Input.GetKeyDown(KeyCode.G) && ultimateCharge >= ultimateSlider.maxValue)
        {
            ultAnimator.SetBool("isUlt", true);
            ultimateCharge = 0f;
            ultimateSlider.value = ultimateCharge;
            if(ultimateCoroutine != null)
            {
                StopCoroutine(ultimateCoroutine);
                ultimateCoroutine = null;
            }
        }else{
            ultAnimator.SetBool("isUlt", false);
            if(ultimateCoroutine == null)
            {
                ultimateCoroutine = StartCoroutine(UltimateAttack());
            }
        }
    }
}
