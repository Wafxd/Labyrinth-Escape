using UnityEngine;
using UnityEngine.UI;
using TMPro; // Tambahkan namespace ini

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _sliderText;

    void Start()
    {
        _slider.onValueChanged.AddListener((v) => {
            _sliderText.text = v.ToString("0.00");
        });
    }
}
