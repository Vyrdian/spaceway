using System.Collections;
using UnityEngine;

[RequireComponent(typeof(UIMenuController))]
public class UIPlayerDeathSwitch : MonoBehaviour
{
    [SerializeField] private UIMenu _deathMenu = null;

    private UIMenuController _menuController;

    private void Start() => _menuController = GetComponent<UIMenuController>();


    private void OnEnable() => PlayerController.OnPlayerDeath += StartDeathMenuCoroutine;

    private void OnDisable() => PlayerController.OnPlayerDeath -= StartDeathMenuCoroutine;

    private void StartDeathMenuCoroutine() => StartCoroutine(OpenDeathMenu());

    private IEnumerator OpenDeathMenu()
    {
        _menuController.ClearActiveMenu();

        yield return new WaitForSeconds(3);

        _menuController.Pause();
        _menuController.ChangeActiveMenu(_deathMenu);
    }
}
