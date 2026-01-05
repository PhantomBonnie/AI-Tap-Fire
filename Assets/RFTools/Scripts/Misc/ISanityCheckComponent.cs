using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISanityCheckComponent
{
    bool RunSanityCheck(out string message);
}
