using UnityEngine;
using System.Collections;

namespace CmdEditor
{
    public class CmdRecv : MonoBehaviour {

	    // Use this for initialization
	    void Start () {
            Debuger.Log( CommandLineReader.GetCommandLine() );
            Debuger.Log(CommandLineReader.GetCommandLine());
            Debuger.Log(CommandLineReader.GetCustomArgument("Language"));
            Debuger.Log(CommandLineReader.GetCustomArgument("Version"));
	    }
	
	    // Update is called once per frame
	    void Update () {
	
	    }
    }

}
