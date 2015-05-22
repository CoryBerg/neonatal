using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UserEvaluationPerformance : MonoBehaviour {

	public GameObject textPrefab;
	public float offset = 20f;

	public GameObject yourPerformanceBox;
	public Text yourPerformanceText;
	public Text summaryText;

	private Dictionary<string, string> effective = new Dictionary<string, string>();
	private Dictionary<string, string> ineffective = new Dictionary<string, string>();
	private Dictionary<string, string> inappropriate = new Dictionary<string, string>();


	// From gold standard document
	private static Dictionary<string, string> _translationDict = new Dictionary<string, string> {
		{"Cardiology", "Cardiology Consult"},
		{"Suction Baby Services", "Suction Baby"},
		{"Surgery", "Surgery Consult"},
		{"Extremity Blood Pressure", "4 Extremity Blood Pressure"},
		{"Suction", "Suction Baby"},
		{"Stethoscope Left", "Stethoscope"},
		{"Stethoscope Right", "Stethoscope"},
		{"Transilluminate Left", "Transilluminate Chest"},
		{"Transilluminate Right", "Transilluminate Chest"},
		{"Begin Chest Compression", "Chest Compression"},
		{"Administer Saline", "Normal Saline"}
	};
	
	private static Dictionary<string, string> _effectiveRespInterventionDict = new Dictionary<string, string> {
		{"Stethoscope", "You appropriately examined the baby’s chest using a stethoscope, as physical exam should be one of the first steps taken to address any patient demonstrating an acute change in clinical status. The right side of the patient’ chest clearly demonstrated breath sounds, however, breath sounds were absent on the left side of the chest. Absent breath sounds on one side of the chest should alert the clinician to the possibility of a pneumothorax on the affected side."},
		//{"Attach ETT", "Asked nurse to attach the ETT to the flow inflating bag to hand bag. Hand bag ventilation in a decompensating mechanically ventilated patient is another important measure to take. However, this will not improve the clinical status in an infant with a tension pneumothorax."},
		{"Suction Baby", "Suctioning a ventilated infant who is displaying hypoxia is certainly an important measure to uncover the underlying etiology of an acute respiratory decompensation. In this case, however, suctioning did not change the infant’s clinical picture as it is not a helpful measure in an infant with a tension pneumothorax. "},
		{"Transilluminate Chest", "You correctly decided that transillumination of the chest wall would be an important step in the diagnostic work up of this infant. When the light source was applied to the axillary region of the left side of the infant’s chest, that side of the chest appeared very bright (as opposed to the right side of the patient’s chest, which only brightened a small amount when the light source was applied). The finding of the two sides of the chest differing in their appearance in response to a light source is suggestive that the infant may have a pneumothorax on the affected side."},
		{"Needle Decompression", "Excellent job! You appropriately performed needle decompression (2nd intercostal space in midclavicular line) performed within 15 minutes. Excellent job! You correctly recognized that this infant was suffering from a tension pneumothorax and that the only effective treatment is prompt needle decompression of the affected side of the chest. Needle decompression is properly performed by introducing a needle into the second intercostal space in the midclavicular line on the affected side, with subsequent release of the trapped air in the extrapleural space."}
	};

	private static Dictionary<string, string> _ineffectiveRespInterventionDict = new Dictionary<string, string> {
		{"ABG", "You ordered an ABG as part of your diagnostic workup. The results of the ABG showed a mixed respiratory and metabolic acidosis, which did not necessarily aid in diagnosing the etiology of the patient’s hypoxemia and respiratory distress."},
		{"CBC", "The results of the CBC were not particularly helpful or revealing of any particular diagnosis in this case."},
		{"Blood Culture", "Although this infant did not have sepsis, sending a blood culture in an acutely decompensating infant to rule out sepsis is not an unreasonable laboratory test to send."},
		{"Glucose", "The blood glucose in this infant was normal."},
		{"BMP", "The BMP in this case revealed a low bicarbonate value as a result of the metabolic acidosis which this patient developed."},
		{"Reintubation", "Any mechanically ventilated patient who displays an acute change in respiratory status should be thoroughly evaluated. The possibility that the endotracheal tube has become displaced should be considered, and if no other cause for the acute decompensation is found, reintubation of the infant is a reasonable option. However, the physical exam finding of absent breath sounds on one side of the chest, such as in this patient, should alert the clinician to the possibility of a pneumothorax, which should be ruled out prior to attempting reintubation."},
		{"Normal Saline", "In this patient, the cause of hypotension and tachycardia was inadequate systemic venous return to the heart. Although a large fluid bolus may slightly and temporarily increase the blood pressure, this will not result in a resolution of the underlying issue and blood pressure will then continue to fall."},
		{"Dopamine", "Administering blood pressure support medications in the face of hypotension will likely temporarily increase blood pressure that has fallen secondary to a variety of etiologies. In this case, however, although blood pressure increased slightly and temporarily, these medications cannot reverse the underlying etiology of the fallen blood pressure and as a result, the blood pressure will then continue to fall."},
		{"Dobutamine", "Administering blood pressure support medications in the face of hypotension will likely temporarily increase blood pressure that has fallen secondary to a variety of etiologies. In this case, however, although blood pressure increased slightly and temporarily, these medications cannot reverse the underlying etiology of the fallen blood pressure and as a result, the blood pressure will then continue to fall."},
		{"Epinephrine", "Administering blood pressure support medications in the face of hypotension will likely temporarily increase blood pressure that has fallen secondary to a variety of etiologies. In this case, however, although blood pressure increased slightly and temporarily, these medications cannot reverse the underlying etiology of the fallen blood pressure and as a result, the blood pressure will then continue to fall."},
		{"Hydrocortisone", "Administering blood pressure support medications in the face of hypotension will likely temporarily increase blood pressure that has fallen secondary to a variety of etiologies. In this case, however, although blood pressure increased slightly and temporarily, these medications cannot reverse the underlying etiology of the fallen blood pressure and as a result, the blood pressure will then continue to fall."},
		{"SlowNeedle", "No needle decompression by 15 minutes (regardless of any other intervention attempted/ medication administered). Unfortunately, this patient’s tension pneumothorax was not addressed within a timely fashion, and therefore the patient continued to display further clinical deterioration. As air continued to accumulate in the extrapleural space, the mediastinal structures continued to shift, further exacerbating the patient’s already low venous return to the heart and subsequently, cardiac output. Therefore, the patient’s blood pressure continued to drop, the patient became more tachycardic, and oxygen saturations continued to deteriorate.  A tension pneumothorax should be addressed in a very prompt manner, as only a matter of minutes may mean the difference between life and death."},
		{"X-Ray", "Chest X-Ray. The chest X-ray showed absent lung markings on the left side of the chest, indication the presence of a left sided pneumothorax. Furthermore, the mediastinal structures appear shifted to the opposite side of the chest, further suggesting the presence of a tension pneumothorax. It is important to note, however, that chest X ray should not be relied upon in order to diagnose a tension pneumothorax as this may delay and interfere with prompt life-saving intervention, thereby causing further clinical deterioration and even death."}
	};

	private static Dictionary<string, string> _inappropriateRespInterventionDict = new Dictionary<string, string> {
		{"Chest Compression", "Chest compression performed. This patient displays tachycardia, not bradycardia, and therefore chest compressions are not indicated. Chest compressions are indicated when the patient’s heart rate falls below 60bpm."},
		{"EKG", "EKG Test Performed. You chose to perform an EKG on this patient, which demonstrated sinus tachycardia at 180bpm. While this test is not necessarily harmful to the patient directly, it does not add much additional information to the case and may ultimately delay care and cause the patient to further decompensate in the meantime."},
		{"ECHO", "ECHO Test Performed. In an acutely unstable patient such as this patient, physical exam should reveal the primary disturbance, and an ECHO is not indicated as it would only delay treatment to this critically ill infant."},
		{"Baby Died", "Needle decompression attempted in inappropriate location. Needle decompression should only be performed in the second intercostal space in the midclavicular line on the affected side of the chest."}
	};

	private static Dictionary<string, string> _effectiveCardioInterventionDict = new Dictionary<string, string> {
		{"Stethoscope", "You appropriately examined the baby’s chest using a stethoscope, as physical exam should be one of the first steps taken to address any patient demonstrating an acute change in clinical status. Although auscultation did not reveal any abnormalities in this patient (as not all ducts will produce a murmur, depending on the size of the duct), it nevertheless provided important information in aiding to rule out a respiratory etiology for the patient’s hypoxia."},
		{"Suction Baby", "Suctioning a ventilated infant who is displaying hypoxia is certainly an important measure to uncover the underlying etiology of an acute respiratory decompensation. In this case, however, suctioning did not change the infant’s clinical picture as it is not a helpful measure in treating desaturation secondary to a ductal dependent cardiac lesion."},
		//{"Attach ETT", "Bag and tube ventilation in a decompensating mechanically ventilated patient is another important measure to take. However, this will not improve the clinical status in an infant with a ductal dependent congenital heart lesion."},
		{"ABG", "You ordered an ABG as part of your diagnostic workup. The results of the ABG showed a metabolic acidosis, which can help to rule out a respiratory etiology in this case, as the patient’s ventilation status was normal."},
		{"Blood Culture", "Although this infant did not have sepsis, sending a blood culture in an acutely decompensating infant to rule out sepsis is an important laboratory test to send. Always consider sepsis, and have a low threshold to empirically treat for it pending a more thorough work-up."},
		{"Hyperoxia", "The hyperoxia test is an excellent way to distinguish between a cardiac and respiratory etiology in a patient presenting with hypoxia. The cyanotic infant should be placed on 100% oxygen via non re-breather mask for 10-20 minutes. If the PaO2 on an ABG increases significantly >150mmHg , then the infant probably has pulmonary pathology. If the PaO2 does not increase, then the likely hood of a Congenital Cyanotic Heart disease is extremely high."},
		{"Prostaglandin Drip", "Excellent job! You correctly recognized that this infant was suffering from a ductal-dependent cardiac lesion and initial management is prompt initiation of a prostaglandin infusion to keep the ductus open. Congenital heart disease lesions that present in the first two to three weeks of life are typically ductal-dependent cardiac lesions. The patent ductus arteriosus had been sustaining blood flow for these infants, and when the ductus closes after birth, these infants suddenly become ill. Depending upon the underlying structural abnormality, these neonates will present with either sudden cyanosis or signs of cardiovascular collapse. If ductal-dependent congenital heart disease is suspected, prostaglandin E1 infusion (PGE1) should be initiated at a rate 0.05µg/kg/min initially then reduced to 0.01-0.05 ug/kg/minute. Prostaglandin is a very potent vasodilator and will have immediate effects on the ductus. Improvement is usually seen within fifteen minutes; however, the practitioner should be prepared to intubate since there is an approximately 12% incidence of apnea following PGE1 initiation."}
	};

	static Dictionary<string, string> _ineffectiveCardioInterventionDict = new Dictionary<string, string> {
		{"X-Ray", "The chest X ray in this case did not contribute any additional information to the diagnosis of this patient. In addition, it is important to note that chest X ray should not be relied upon in order to diagnose an acutely decompensating patient as this may delay and interfere with prompt life-saving intervention, thereby causing further clinical deterioration and even death. However a chest x-ray is partially beneficial in confirming ET-tube position, reviewing cardiothoracic ratio while ruling out pneumonia."},
		{"CBC", "The results of the CBC were not particularly helpful or revealing of any particular diagnosis in this case."},
		{"Glucose", "The blood glucose in this infant was normal."},
		{"BMP", "The BMP in this case revealed a low bicarbonate value as a result of the metabolic acidosis which this patient developed."},
		{"ECHO", "Although an ECHO would likely reveal the cause of the acute clinical decompensation, it should not be relied upon in an acutely unstable patient as it would delay the initiation of treatment for this critically ill infant."},
		{"EKG", "You chose to perform an EKG on this patient, which demonstrated sinus tachycardia at 180bpm. While this test may have been helpful in ruling out an arrhythmia as the cause for the acute cardiovascular decompensation, it may ultimately delay care if relied upon prior to initiating treatment. It is important to note that EKGs can help diagnosis complete A-V canal and Tricupid Atresia by revealing superior axis deviation."},
		{"Reintubation", "Any mechanically ventilated patient who displays an acute change in respiratory status should be thoroughly evaluated. The possibility that the endotracheal tube has become displaced should be considered, and if no other cause for the acute decompensation is found, reintubation of the infant is a reasonable option."},
		{"Normal Saline", "In this patient, the cause of hypotension and tachycardia was inadequate cardiac output secondary to the closing ductus arteriosus. Although a large fluid bolus may temporarily increase the blood pressure, this will not result in a resolution of the underlying issue and blood pressure will then continue to fall."},
		{"Dopamine", "Inotropes and steroids are excellent medications for treating hypotention from multiple etiologies. Often times this reversal is permanent, but occasionally may be temporal depending on the primary cause. In this case, however, although blood pressure increased slightly and temporarily, these medications cannot reverse the underlying etiology of the fallen blood pressure and as a result, the blood pressure will then continue to fall."},
		{"Dobutamine", "Inotropes and steroids are excellent medications for treating hypotention from multiple etiologies. Often times this reversal is permanent, but occasionally may be temporal depending on the primary cause. In this case, however, although blood pressure increased slightly and temporarily, these medications cannot reverse the underlying etiology of the fallen blood pressure and as a result, the blood pressure will then continue to fall."},
		{"Epinephrine", "Inotropes and steroids are excellent medications for treating hypotention from multiple etiologies. Often times this reversal is permanent, but occasionally may be temporal depending on the primary cause. In this case, however, although blood pressure increased slightly and temporarily, these medications cannot reverse the underlying etiology of the fallen blood pressure and as a result, the blood pressure will then continue to fall."},
		{"Hydrocortisone", "Inotropes and steroids are excellent medications for treating hypotention from multiple etiologies. Often times this reversal is permanent, but occasionally may be temporal depending on the primary cause. In this case, however, although blood pressure increased slightly and temporarily, these medications cannot reverse the underlying etiology of the fallen blood pressure and as a result, the blood pressure will then continue to fall."},
		{"Transilluminate Chest", "Although transillumination of the chest wall may reveal a pneumothorax as the cause for cyanosis and desaturation, this infant demonstrated clear breath sounds bilaterally, and therefore transillumination is not a necessary step in the diagnostic workup of this patient."}
	};

	private static Dictionary<string, string> _inappropriateCardioInterventionDict = new Dictionary<string, string> {
		{"Needle Decompression", "This patient did not have a tension pneumothorax, therefore needle decompression was not an appropriate part of the management for this patient."},
		{"Albuterol", "This patient demonstrated clear breath sounds, which is not consistent with an acute episode of bronchoconstriction. Waiting for the completion of an inhaled medication treatment to end may also delay and interfere with life-saving interventions and cause further clinical deterioration of the patient."},
		{"Racemic Epinephrine", "This patient demonstrated clear breath sounds, which is not consistent with an acute episode of bronchoconstriction. Waiting for the completion of an inhaled medication treatment to end may also delay and interfere with life-saving interventions and cause further clinical deterioration of the patient."},
		{"Atrovent", "This patient demonstrated clear breath sounds, which is not consistent with an acute episode of bronchoconstriction. Waiting for the completion of an inhaled medication treatment to end may also delay and interfere with life-saving interventions and cause further clinical deterioration of the patient."},
		{"Chest Compression", "This patient displays tachycardia, not bradycardia, and therefore chest compressions are not indicated. Chest compressions are indicated when the patient’s heart rate falls below 60bpm."}
	};

	// Use this for initialization
	void Start () {
		if(CaseHandler.Instance.babyAlive == false) {
			UILoggerNew.ButtonsPressed.Add ("Baby Died");
		}

		// Get effective, ineffective and inappropriate dictionaries for current case
		UseRelevantDict();

		List<string> displayedKeys = new List<string>();
		if(UILoggerNew.ButtonsPressed != null) {
			summaryText.text += "\n\nEvaluation of Player Performance";

			string textVal = string.Format("{0:D}. {1}", 1, UILoggerNew.ButtonsPressed[0]);
			int currentStepNumber = 1;
			for(int i = 0; i < UILoggerNew.ButtonsPressed.Count; i++) {
				if(UILoggerNew.ButtonsPressed[i].Contains("Exit")) {
					continue;
				}

				// If the ButtonPressed string exists in the translate dictionary, use the translated version
				string displayKey;
				if(_translationDict.ContainsKey(UILoggerNew.ButtonsPressed[i])) {
					displayKey = _translationDict[UILoggerNew.ButtonsPressed[i]];
					textVal = string.Format("{0:D}. {1}", 1, displayKey);
				} else {
					displayKey = UILoggerNew.ButtonsPressed[i];
				}

				// If the string has already been displayed, ignore it
				if(displayedKeys.Contains(displayKey)) {
					continue;
				}

				displayedKeys.Add(displayKey);

				Color textColor = Color.white;
				bool activeTooltip = true;
				string tooltip = "";

				// If the string is a part of the effective Dictionary, make it green
				// If the string is a part of the ineffective Dictionary, make it yellow
				// Otherwise, it was inappropriate, make it red
				if(effective.ContainsKey(displayKey)) {
					textColor = Color.green;
					try {
						tooltip = effective[displayKey];
					} catch(KeyNotFoundException e) {
						print(e.Message + ": " + displayKey);
						continue;
					}
				} else if(ineffective.ContainsKey(displayKey)) {
					textColor = Color.yellow;
					try {
						tooltip = ineffective[displayKey];
					} catch(KeyNotFoundException e) {
						print(e.Message + ": " + displayKey);
						continue;
					}
				} else if(inappropriate.ContainsKey(displayKey)) {
					textColor = Color.red;
					try {
						tooltip = inappropriate[displayKey];
					} catch(KeyNotFoundException e) {
						print(e.Message + ": " + displayKey);
						continue;
					}
				} else {
					activeTooltip = false;
				}

				GameObject textInstance = (GameObject)Instantiate(textPrefab);
				textInstance.transform.SetParent(yourPerformanceBox.transform, false);
				textInstance.transform.position = yourPerformanceText.transform.position + new Vector3(0, -offset * (currentStepNumber - 1));

				textVal = string.Format("{0:D}. {1}",currentStepNumber,displayKey);
				textInstance.GetComponent<Text>().text = textVal;
				textInstance.GetComponent<Text>().color = textColor;

				if(activeTooltip) {
					summaryText.text += "\n\n" + textVal + " - " + tooltip;
					textInstance.GetComponent<HoverableText>().textToDisplay = tooltip;
				} else {
					summaryText.text += "\n\n" + textVal;
					textInstance.GetComponent<HoverableText>().enabled = false;
				}

				currentStepNumber++;
			}
		} else { // If no data, display "No data"
			yourPerformanceText.text = "No data";
		}
	}

	private void UseRelevantDict() {
		if(CaseHandler.Instance.currentCase == NeonatalCase.Respiratory) {
			effective = _effectiveRespInterventionDict;
			ineffective = _ineffectiveRespInterventionDict;
			inappropriate = _inappropriateRespInterventionDict;
		} else if(CaseHandler.Instance.currentCase == NeonatalCase.Cardiac) {
			effective = _effectiveCardioInterventionDict;
			ineffective = _ineffectiveCardioInterventionDict;
			inappropriate = _inappropriateCardioInterventionDict;
		}
	}
}
