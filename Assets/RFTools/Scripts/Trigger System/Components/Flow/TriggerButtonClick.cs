using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ravenfield.Trigger
{
    [AddComponentMenu("Trigger/Flow/Trigger Button Click")]
    [TriggerDoc("Sends OnClick when the attached UI button is clicked.")]
    [RequireComponent(typeof(UnityEngine.UI.Button))]
    public partial class TriggerButtonClick : TriggerBaseComponent
    {
        [SignalDoc("Sent when UI button is clicked")]
        public TriggerSend onClick;
    }
}