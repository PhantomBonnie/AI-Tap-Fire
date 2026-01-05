using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ravenfield.Trigger
{
    [AddComponentMenu("Trigger/Events/Trigger On Key")]
    [TriggerDoc("Sends OnPressed/OnReleased when a key is pressed or released.")]
    public partial class TriggerOnKey : TriggerBaseComponent
    {
        public enum Mode
        {
            SpecificKey,
            KeyBind,
        }

        public Mode mode = Mode.SpecificKey;

        [ConditionalField("mode", Mode.SpecificKey)]
        public KeyCode key;

        [ConditionalField("mode", Mode.KeyBind)]
        public SteelInput.KeyBinds keyBind;

        public bool testModeOnly = false;

        public TriggerSend onPressed;
        public TriggerSend onReleased;
    }
}