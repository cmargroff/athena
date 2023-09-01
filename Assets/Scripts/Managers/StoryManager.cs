using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class StoryManager : MonoBehaviour
{
    public List<Pan> Pans;
    protected Sequence _seq;

    [SerializeField]
    private RectTransform _image;

    [SerializeField]
    private RectTransform _canvas;

    private @PlayerInputActions _controls;

    public string SceneName;

    protected  void Start()
    {
        Console.WriteLine("Story started");
        _controls.Menues.Enable();
        StartImageSequence();

        _controls.Menues.Submit.performed+=context => {
            SceneManager.LoadSceneAsync(SceneName,LoadSceneMode.Single);
        };
    }

    public void ConfigureStory()
    {
        Console.WriteLine("Story configured");
    }

    private void StartImageSequence()
    {
        _seq = DOTween.Sequence(); //new Sequence()
        _seq.SetUpdate(UpdateType.Manual);

        //Vector2 ratio = new Vector2(_image.rect.width / _canvas.rect.width, _image.rect.height / _canvas.rect.height);
        var baseCoordinates = _image.transform.position;
        var oldCoordinates = new Vector3(
            _image.transform.position.x - (_image.rect.width - _canvas.rect.width) * Pans[0].Coordinates.x,
            _image.transform.position.y + (_image.rect.height - _canvas.rect.height) * Pans[0].Coordinates.y,
            0);

        foreach (var pan in Pans.Skip(1).Append(Pans[0]))
        {
            var newCoordinates = new Vector3(
                baseCoordinates.x - (_image.rect.width - _canvas.rect.width) * pan.Coordinates.x,
                baseCoordinates.y + (_image.rect.height - _canvas.rect.height) * pan.Coordinates.y,
                0);
            var tween = DOTween.To(x => { _image.transform.position = Vector3.Lerp(oldCoordinates, newCoordinates, x); }, 0,
                1, pan.Time);
            tween.SetEase(pan.Ease);
            _seq.Append(tween);
            _seq.AppendCallback(() => { oldCoordinates = _image.transform.position; });
        }

        _seq.SetLoops(-1);
    }


    protected void FixedUpdate()
    {
       // base.PlausibleFixedUpdate();
        _seq.ManualUpdate(Time.fixedDeltaTime, Time.fixedDeltaTime);
    }
}
