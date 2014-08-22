using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

// Interface to communicate with the XML Data for the rest of the project
// There is a data container for each segment of the UI that is formatted a specific way
public class DataHandler : MonoBehaviour {
	private DataContainer diagnosticInfo;
	public DataContainer DiagnosticInfo {
		get { return diagnosticInfo; }
	}

	public void Start() {
		diagnosticInfo = DataContainer.LoadFromTextFile ("XML/DiagnosticInfo");
		print(diagnosticInfo.Lookup("Laboratory"));
	}
}
