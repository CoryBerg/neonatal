﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserEvaluationPerformance : MonoBehaviour {
	public GameObject labelPrefab;
	public float offset = 2f;
	public Transform root;
	// From gold standard document
	static Dictionary<string, string> _translationDict = new Dictionary<string, string>
	{
		{"ButtonStethLL","Stethoscope"},
		{"ButtonStethLR","Stethoscope"},
		{"ButtonStethUL","Stethoscope"},
		{"ButtonStethUR","Stethoscope"},
		{"ButtonNeedle","Needle Decompression"},
		{"ButtonSuctionbaby","Suction the Baby"},
		{"ButtonTransilL","Transilluminate Chest"},
		{"ButtonETT","Attach ETT"},
		{"ButtonTransilR","Transilluminate Chest"},
		{"ButtonXRay","X-Ray"},
		{"SlowNeedle","No Needle"},
		{"TheBabyDied","Baby Died"},
		{"ButtonECHO","ECHO Test"},
		{"ButtonEKG","EKG Test"},
		{"ButtonChest","Chest Compression"},
		{"ButtonDopamine","BP Medication"},
		{"ButtonDobutamine","BP Medication"},
		{"ButtonEpinephrine","BP Medication"},
		{"ButtonHydrocortisone","BP Medication"},
		{"ButtonAdministerSaline","Normal Saline"},
		{"ButtonIntubation","Intubation"},
		{"ButtonBMP","BMP Test"},
		{"ButtonBloGlu","Glucose Test"},
		{"ButtonBloCul","Blood Culture"},
		{"ButtonCBC","CBC Test"},
		{"ButtonInitialABG","ABG Test"},
		{"Debug","Nothing"}
	};

	static Dictionary<string, string> _effectiveInterventionDict = new Dictionary<string, string>
	{
		{"Stethoscope","You appropriately examined the baby’s chest using a stethoscope, as physical exam should be one of the first steps taken to address any patient demonstrating an acute change in clinical status.  The right side of the patient’ chest clearly demonstrated breath sounds, however, breath sounds were absent on the left side of the chest.  Absent breath sounds on one side of the chest should alert the clinician to the possibility of a pneumothorax on the affected side."},
		{"Debug","Tooltip not implemented!"},
		{"Attach ETT","Asked nurse to attach the ETT to the flow inflating bag to hand bag. Hand bag ventilation in a decompensating mechanically ventilated patient is another important measure to take.  However, this will not improve the clinical status in an infant with a tension pneumothorax."},
		{"Suction the Baby","Suctioning a ventilated infant who is displaying hypoxia is certainly an important measure to uncover the underlying etiology of an acute respiratory decompensation.  In this case, however, suctioning did not change the infant’s clinical picture as it is not a helpful measure in an infant with a tension pneumothorax.  "},
		{"Transilluminate Chest","You correctly decided that transillumination of the chest wall would be an important step in the diagnostic work up of this infant.  When the light source was applied to the axillary region of the left side of the infant’s chest, that side of the chest appeared very bright (as opposed to the right side of the patient’s chest, which only brightened a small amount when the light source was applied).  The finding of the two sides of the chest differing in their appearance in response to a light source is suggestive that the infant may have a pneumothorax on the affected side."},
		{"Needle Decompression","Appropriately performed needle decompression (2nd intercostal space in midclavicular line) performed within 15 minutes.  Excellent job!  You correctly recognized that this infant was suffering from a tension pneumothorax and that the only effective treatment is prompt needle decompression of the affected side of the chest.  Needle decompression is properly performed by introducing a needle into the second intercostal space in the midclavicular line on the affected side, with subsequent release of the trapped air in the extrapleural space."}
	};
	static Dictionary<string, string> _ineffectiveInterventionDict = new Dictionary<string, string>
	{
		{"Nothing","Filler"},
		{"ABG Test","You ordered an ABG as part of your diagnostic workup.  The results of the ABG showed a mixed respiratory and metabolic acidosis, which did not necessarily aid in diagnosing the etiology of the patient’s hypoxemia and respiratory distress."},
		{"CBC Test","The results of the CBC were not particularly helpful or revealing of any particular diagnosis in this case."},
		{"Blood Culture","Although this infant did not have sepsis, sending a blood culture in an acutely decompensating infant to rule out sepsis is not an unreasonable laboratory test to send."},
		{"Glucose Test","The blood glucose in this infant was normal."},
		{"BMP Test","The BMP in this case revealed a low bicarbonate value as a result of the metabolic acidosis which this patient developed."},
		{"Intubation","Any mechanically ventilated patient who displays an acute change in respiratory status should be thoroughly evaluated.  The possibility that the endotracheal tube has become displaced should be considered, and if no other cause for the acute decompensation is found, reintubation of the infant is a reasonable option.  However, the physical exam finding of absent breath sounds on one side of the chest, such as in this patient, should alert the clinician to the possibility of a pneumothorax, which should be ruled out prior to attempting reintubation."},
		{"Normal Saline","In this patient, the cause of hypotension and tachycardia was inadequate systemic venous return to the heart.  Although a large fluid bolus may slightly and temporarily increase the blood pressure, this will not result in a resolution of the underlying issue and blood pressure will then continue to fall."},
		{"BP Medication","Administering blood pressure support medications in the face of hypotension will likely temporarily increase blood pressure that has fallen secondary to a variety of etiologies.  In this case, however, although blood pressure increased slightly and temporarily, these medications cannot reverse the underlying etiology of the fallen blood pressure and as a result, the blood pressure will then continue to fall."},
		{"No Needle","No needle decompression by 15 minutes (regardless of any other intervention attempted/ medication administered).  Unfortunately, this patient’s tension pneumothorax was not addressed within a timely fashion, and therefore the patient continued to display further clinical deterioration.  As air continued to accumulate in the extrapleural space, the mediastinal structures continued to shift, further exacerbating the patient’s already low venous return to the heart and subsequently, cardiac output.  Therefore, the patient’s blood pressure continued to drop, the patient became more tachycardic, and oxygen saturations continued to deteriorate.   A tension pneumothorax should be addressed in a very prompt manner, as only a matter of minutes may mean the difference between life and death."},
		{"X-Ray","Chest X Ray. The chest X ray showed absent lung markings on the left side of the chest, indication the presence of a left sided pneumothorax.  Furthermore, the mediastinal structures appear shifted to the opposite side of the chest, further suggesting the presence of a tension pneumothorax. It is important to note, however, that chest X ray should not be relied upon in order to diagnose a tension pneumothorax as this may delay and interfere with prompt life-saving intervention, thereby causing further clinical deterioration and even death."}
	};
	static Dictionary<string, string> _inappropriateInterventionDict = new Dictionary<string, string>
	{
		{"Nothing","Filler"},
		{"Chest Compression","Chest compression performed. This patient displays tachycardia, not bradycardia, and therefore chest compressions are not indicated.  Chest compressions are indicated when the patient’s heart rate falls below 60bpm."},
		{"EKG Test","EKG Test Performed. You chose to perform an EKG on this patient, which demonstrated sinus tachycardia at 180bpm.  While this test is not necessarily harmful to the patient directly, it does not add much additional information to the case and may ultimately delay care and cause the patient to further decompensate in the meantime."},
		{"ECHO Test","ECHO Test Performed. In an acutely unstable patient such as this patient, physical exam should reveal the primary disturbance, and an ECHO is not indicated as it would only delay treatment to this critically ill infant."},
		{"Baby Died","Needle decompression attempted in inappropriate location. Needle decompression should only be performed in the second intercostal space in the midclavicular line on the affected side of the chest."}
	};
	// Use this for initialization
	void Start () {
//		For Debugging in scene...
//		UILogger.ButtonsPressed = new List<string> ();
//		for (int i = 0; i < 15; i++) {
//			UILogger.ButtonsPressed.Add ("Debug");
//		}
		if(CaseHandler.Instance.babyAlive == false) {
			UILogger.ButtonsPressed.Add ("TheBabyDied");
		}
		if(UILogger.ButtonsPressed != null) {
			string textVal = string.Format("{0:D}. {1}", 1, UILogger.ButtonsPressed[0]);
			int c = 1;
			for(int i = 0; i < UILogger.ButtonsPressed.Count; i++) {
				if(UILogger.ButtonsPressed[i].Contains("Exit") || ! _translationDict.ContainsKey(UILogger.ButtonsPressed[i])) {
					continue;
				}
				GameObject labelInst = (GameObject)dfGUIManager.Instantiate(labelPrefab);
				labelInst.transform.parent = this.transform;
				dfLabel lbl = labelInst.GetComponent<dfLabel>();
				lbl.Position = root.transform.position + new Vector3(0,-offset * (c - 1));
				string v = _translationDict[UILogger.ButtonsPressed[i]];
				string tooltip = _effectiveInterventionDict["Debug"] + " " + v;
				if(_effectiveInterventionDict.ContainsKey(v)) {
					lbl.Color = Color.green;
					tooltip = _effectiveInterventionDict[v];
				} else if(_ineffectiveInterventionDict.ContainsKey(v)) {
					lbl.Color = Color.yellow;
					tooltip = _ineffectiveInterventionDict[v];
				} else {
					lbl.Color = Color.red;
					tooltip = _inappropriateInterventionDict[v];
				}
				textVal = string.Format("{0:D}. {1}",c,v);
				lbl.Text = textVal;
				lbl.Tooltip = tooltip;
				c++;
			}
		} else {
			GameObject labelInst = (GameObject)dfGUIManager.Instantiate(labelPrefab);
			labelInst.transform.parent = this.transform;
			labelInst.transform.position = root.transform.position;
			dfLabel lbl = labelInst.GetComponent<dfLabel>();
			lbl.Text = "No data";
			lbl.Tooltip = "No available description.";
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
