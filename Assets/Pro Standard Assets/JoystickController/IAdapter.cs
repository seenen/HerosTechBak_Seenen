using UnityEngine;
using System.Collections;

public interface IAdapter 
{
    string controller { set; get; }

    void CurrentAxis(string xname, float x, string yname, float y);

    void CurrentButton(string name);
}
