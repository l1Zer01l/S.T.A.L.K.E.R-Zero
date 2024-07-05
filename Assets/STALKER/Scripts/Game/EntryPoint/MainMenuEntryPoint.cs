/**************************************************************************\
   Copyright SkyForge Corporation. All Rights Reserved.
\**************************************************************************/

using StalkerZero.Infrastructure;
using System.Collections;
using UnityEngine;

namespace StalkerZero
{
    public class MainMenuEntryPoint : MonoBehaviour, IEntryPoint
    {
        public IEnumerator Intialization(DIContainer parentContainer)
        {
            Debug.Log("Init Main Menu");
            yield return new WaitForSeconds(2);
        }
    }
}
