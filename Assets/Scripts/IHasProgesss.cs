using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasProgesss 
{
    public event EventHandler<OnprogressChangedEventArgs> OnprogressChanged;
    public class OnprogressChangedEventArgs : EventArgs {
        public float progressNomalized;
    }
}
