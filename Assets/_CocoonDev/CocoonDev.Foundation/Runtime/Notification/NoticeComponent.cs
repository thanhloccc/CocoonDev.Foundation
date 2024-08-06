using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace CocoonDev.Foundation
{
    public class NoticeComponent : MonoBehaviour
    {
        [Title("Component Refs", titleAlignment: TitleAlignments.Centered)]
        [SerializeField, Required]
        private PlayableDirector _director;
        [SerializeField, Required]
        private TextMeshProUGUI _messageText;

        [Title("Asset Loader", titleAlignment: TitleAlignments.Centered)]
        [SerializeField]
        private TimelineAsset _timelineAsset;

        public void Initialize(string message)
        {
            _messageText.text = message;

            _director.playableAsset = _timelineAsset;
            _director.time = 0;
            _director.initialTime = 0;
            _director.Play();

        }

    }
}
