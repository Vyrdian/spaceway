using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenuController : MonoBehaviour
{
    [SerializeField] private UIMenu _activeMenu;

    private void Start()
    {
        if (_activeMenu)
            _activeMenu.gameObject.SetActive(true);
    }

    public void LoadScene(string sceneName)
    {
        UnPause();
        SceneManager.LoadScene(sceneName);
    }


    public void Pause() => Time.timeScale = 0;

    public void UnPause() => Time.timeScale = 1;

    public void ChangeActiveMenu(UIMenu newMenu)
    {
        if(_activeMenu)
            _activeMenu.gameObject.SetActive(false);

        _activeMenu = newMenu;
        _activeMenu.gameObject.SetActive(true);
        _activeMenu.LoadedThroughController();
    }

    public void ClearActiveMenu()
    {
        _activeMenu.gameObject.SetActive(false);
        _activeMenu = null;
    }

    public void DisplayText(string textToDisplay, TextMeshProUGUI text) => text.text = textToDisplay;
}
