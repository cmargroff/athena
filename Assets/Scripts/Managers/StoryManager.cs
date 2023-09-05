using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(AudioSource))]
public class StoryManager : BaseMonoBehavior
{

    protected Sequence _imageSeq;
    protected Sequence _audioSeq;
    [SerializeField]
    private RectTransform _image;


    [SerializeField]
    private RectTransform _canvas;

    private @PlayerInputActions _controls;

    [SerializeField]
    private StorySO _story;

    public UnityEvent Completed;

    private AudioSource _voiceAudioSource;
    private AudioSource _musicAudioSource;
    [SerializeField]
    protected TextMeshProUGUI _text;

    protected void Start()
    {
        _controls = new();
        var soundSources = GetComponents<AudioSource>();
        _voiceAudioSource = soundSources[0];
        _musicAudioSource = soundSources[1];
        _musicAudioSource.loop=true;
        _musicAudioSource.PlayOneShot(_story.Song);
        

        _controls.Menues.Enable();
        StartImageSequence();

        _controls.Menues.Submit.performed += context =>
        {
            Completed?.Invoke();
        };
    }

    public void ConfigureStory(StorySO story)
    {
        _story = story;
    }

    private void StartImageSequence()
    {
        var imageImage = _image.GetComponent<UnityEngine.UI.Image>();

        Vector3 oldCoordinates = new Vector3();
        Vector3 baseCoordinates = _image.transform.position; ;
        _imageSeq = DOTween.Sequence();
        _audioSeq = DOTween.Sequence();
        _imageSeq.SetUpdate(UpdateType.Manual);
        _audioSeq.SetUpdate(UpdateType.Manual);
        foreach (var panel in _story.Panels)
        {
            foreach (var voiceLine in panel.VoiceLines)
            {
                _audioSeq.AppendCallback(() =>
                {
                    if (voiceLine.VoiceClip != null)
                    {
                        _voiceAudioSource.PlayOneShot(voiceLine.VoiceClip);
                    }

                    _text.text = voiceLine.Text;
                });
                _audioSeq.AppendInterval(voiceLine.Time);
            }

            float extraTime = panel.Pans.Sum(x => x.Time) - panel.VoiceLines.Sum(x => x.Time);
            if (extraTime > 0)
            {
                _audioSeq.AppendInterval(extraTime);
            }

            //set up voice clips

            //set up image
            _imageSeq.AppendCallback(() =>
            {
                imageImage.material.SetTexture("_Image", panel.Image);
                //imageImage.sprite = panel.Image;

            });
            oldCoordinates = new Vector3(
                    _image.transform.position.x -
                    (_image.rect.width - _canvas.rect.width) * panel.Pans[0].Coordinates.x,
                    _image.transform.position.y +
                    (_image.rect.height - _canvas.rect.height) * panel.Pans[0].Coordinates.y,
                    0);
            var oldZoom= panel.Pans[0].Zoom;

            foreach (var pan in panel.Pans.Skip(1).Append(panel.Pans[0]))
            {
                var newCoordinates = new Vector3(
                    baseCoordinates.x - (_image.rect.width - _canvas.rect.width) * pan.Coordinates.x,
                    baseCoordinates.y + (_image.rect.height - _canvas.rect.height) * pan.Coordinates.y,
                    0);
                var coordinates = oldCoordinates;
                var tween = DOTween.To(
                    x =>
                    {
                        _image.transform.position = Vector3.Lerp(coordinates, newCoordinates, x);
                        imageImage.material.SetFloat("_Zoom", Mathf.Lerp(oldZoom,  pan.Zoom, x));

                    }, 0,
                    1, pan.Time);
                tween.SetEase(pan.Ease);
                _imageSeq.Append(tween);
                _imageSeq.AppendCallback(() => { oldCoordinates = _image.transform.position; });
            }
        }

        _imageSeq.AppendCallback(() =>
            {
                Completed?.Invoke();
            }

        );

    }


    protected void FixedUpdate()
    {
        // base.PlausibleFixedUpdate();
        _imageSeq.ManualUpdate(Time.fixedDeltaTime, Time.fixedDeltaTime);
        _audioSeq.ManualUpdate(Time.fixedDeltaTime, Time.fixedDeltaTime);
    }
}
