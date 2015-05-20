using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InGameMenuUI : Singleton<InGameMenuUI> {

	[Header ("Main Buttons")]
	public GameObject MainButtons;

	[Header ("Main Panels")]
	public GameObject SolicitInformationPanel;
	public GameObject PhysicalExamPanel;
	public GameObject ConsultServicesPanel;
	public GameObject TestPanel;
	public GameObject PerformInterventionsPanel;

	public GameObject OptionsPanel;

	[Header ("Question Panels")]
	public GameObject Q1Panel;
	public GameObject Q2Panel;
	public GameObject Q3Panel;
	public GameObject Q4Panel;
	public GameObject Q5Panel;

	[Header ("Physical Exam Panels")]
	public GameObject VisualizeChestPanel;
	public GameObject StethoscopePanel;
	public GameObject FaceExamPanel;

	[Header ("Consult Services Panels")]
	public GameObject CardiologyPanel;
	public GameObject SurgeryPanel;
	
	[Header ("Test Panels")]
	public GameObject ExtremityBloodPressurePanel;
	public GameObject HyperoxiaPanel;
	public GameObject ECHOPanel;
	public GameObject LaboratoryPanel;
	public GameObject EKGPanel;
	public GameObject XRayPanel;
	public GameObject TransilluminatePanel;

	[Header ("Laboratory Test Panels")]
	public GameObject BloodCulturePanel;
	public GameObject CBCPanel;
	public GameObject BloodGlucosePanel;
	public GameObject ABGPanel;
	public GameObject BMPPanel;

	[Header ("Perform Interventions Panels")]
	public GameObject ChestCompressionPanel;
	public GameObject IVMedicationsPanel;
	public GameObject InhaledMedicationsPanel;


	[Header ("Cameras")]
	public Camera mainCamera;
	public Camera chestCamera;

	[Header ("Stethoscope Targets")]
	public Transform leftChestTarget;
	public Transform rightChestTarget;


	private GameObject activePanel;
	private GameObject previousPanel;
	private GameObject previousPreviousPanel;
	

	// Use this for initialization
	void OnEnable() {
		chestCamera.enabled = false;

		CloseAllPanels();
	}

	private void CloseAllPanels () {
		activePanel = null;
		previousPanel = null;
		previousPreviousPanel = null;

		SolicitInformationPanel.SetActive(false);
		PhysicalExamPanel.SetActive(false);
		ConsultServicesPanel.SetActive(false);
		TestPanel.SetActive(false);
		PerformInterventionsPanel.SetActive(false);
		OptionsPanel.SetActive(false);

		Q1Panel.SetActive(false);
		Q2Panel.SetActive(false);
		Q3Panel.SetActive(false);
		Q4Panel.SetActive(false);
		Q5Panel.SetActive(false);

		VisualizeChestPanel.SetActive(false);
		StethoscopePanel.SetActive(false);
		FaceExamPanel.SetActive(false);

		CardiologyPanel.SetActive(false);
		SurgeryPanel.SetActive(false);

		ExtremityBloodPressurePanel.SetActive(false);
		HyperoxiaPanel.SetActive(false);
		ECHOPanel.SetActive(false);
		EKGPanel.SetActive(false);
		LaboratoryPanel.SetActive(false);
		XRayPanel.SetActive(false);
		TransilluminatePanel.SetActive(false);

		BloodCulturePanel.SetActive(false);
		CBCPanel.SetActive(false);
		BloodGlucosePanel.SetActive(false);
		ABGPanel.SetActive(false);
		BMPPanel.SetActive(false);

		ChestCompressionPanel.SetActive(false);
		IVMedicationsPanel.SetActive(false);
		InhaledMedicationsPanel.SetActive(false);
	}

	private void OpenPanel(GameObject panel) {
		panel.SetActive(true);
		activePanel = panel;
	}

	public void BackOnePanel() {
		ClosePanel();
		OpenPanel(previousPanel);
		previousPanel = previousPreviousPanel;
	}

	public void ClosePanel() {
		activePanel.SetActive (false);
	}

	private void ShowMainButtons() {
		MainButtons.SetActive(true);
	}

	private void HideMainButtons() {
		MainButtons.SetActive(false);
	}

	//
	// Main Panels
	//

	public void SolicitInformation() {
		CloseAllPanels();
		OpenPanel(SolicitInformationPanel);
	}

	public void PhysicalExam() {
		CloseAllPanels();
		OpenPanel(PhysicalExamPanel);
	}

	public void ConsultServices() {
		CloseAllPanels();
		OpenPanel(ConsultServicesPanel);
	}

	public void Test() {
		CloseAllPanels();
		OpenPanel(TestPanel);
	}

	public void PerformInterventions() {
		CloseAllPanels();
		OpenPanel(PerformInterventionsPanel);
	}

	public void Options() {
		CloseAllPanels();
		OpenPanel(OptionsPanel);
	}

	//
	// Question Panels
	//

	public void Q1() {
		previousPanel = SolicitInformationPanel;

		ClosePanel();
		OpenPanel(Q1Panel);
	}

	public void Q2() {
		previousPanel = SolicitInformationPanel;

		CloseAllPanels();
		OpenPanel(Q2Panel);
	}

	public void Q3() {
		previousPanel = SolicitInformationPanel;

		ClosePanel();
		OpenPanel(Q3Panel);
	}

	public void Q4() {
		previousPanel = SolicitInformationPanel;

		ClosePanel();
		OpenPanel(Q4Panel);
	}

	public void Q5() {
		previousPanel = SolicitInformationPanel;

		ClosePanel();
		OpenPanel(Q5Panel);
	}

	//
	// Physical Exam Panels
	//

	public void EnterVisualizeChest() {
		ClosePanel();
		HideMainButtons();
		OpenPanel(VisualizeChestPanel);

		chestCamera.enabled = true;
		mainCamera.enabled = false;
	}
	
	public void ExitVisualizeChest() {
		ClosePanel();
		ShowMainButtons();

		mainCamera.enabled = true;
		chestCamera.enabled = false;
	}

	public void EnterStethoscope() {
		ClosePanel();
		HideMainButtons();
		OpenPanel(StethoscopePanel);
	}

	public void ExitStethoscope() {
		ClosePanel();
		ShowMainButtons();

		ArmAnimatorController.Instance.FinishSteth();
	}

	public void EnterFaceExam() {
		ClosePanel();
		HideMainButtons();

		OpenPanel(FaceExamPanel);

		mainCamera.animation ["faceExam"].speed = 1f;
		mainCamera.animation["faceExam"].time = 0;
		mainCamera.animation.Play("faceExam");
	}

	public void ExitFaceExam() {
		ClosePanel();
		ShowMainButtons();

		mainCamera.animation ["faceExam"].speed = -1f;
		mainCamera.animation["faceExam"].time = mainCamera.animation["faceExam"].length;
		mainCamera.animation.Play("faceExam");
	}

	//
	// Services Panels
	//

	public void Cardiology() {
		previousPanel = ConsultServicesPanel;
		
		ClosePanel();
		OpenPanel(CardiologyPanel);
	}

	public void Surgery() {
		previousPanel = ConsultServicesPanel;
		
		ClosePanel();
		OpenPanel(SurgeryPanel);
	}

	//
	// Test Panels
	//

	public void OpenTestPanel(GameObject panel) {
		previousPanel = TestPanel;

		ClosePanel();
		OpenPanel(panel);
	}

	public void OpenLabTestPanel(GameObject panel) {
		previousPanel = LaboratoryPanel;
		previousPreviousPanel = TestPanel;
		
		ClosePanel();
		OpenPanel(panel);
	}

	public void Laboratory() {
		previousPanel = TestPanel;
		
		ClosePanel();
		OpenPanel(LaboratoryPanel);
	}

	public void EnterTransilluminate() {
		ClosePanel();
		HideMainButtons();

		OpenPanel(TransilluminatePanel);
		
		mainCamera.GetComponent<TransilluminationLeft>().BeginToggle();
	}

	public void ExitTransilluminate() {
		ClosePanel();
		ShowMainButtons();

		mainCamera.GetComponent<TransilluminationLeft>().EndToggle();
	}

	//
	// Perform Interventions
	//

	
	public void Suction() {
		ClosePanel();
		
		ArmAnimatorController.Instance.TriggerAnimation("ButtonSuction");
	}

	public void NeedleDecompression() {
		ClosePanel();

		AnimationHandler.Instance.HandleAnimation("ButtonNeedle");
	}

	public void Reintubation() {
		ClosePanel();
		
		AnimationHandler.Instance.HandleAnimation("ButtonIntubation");
	}

	public Camera[] cameras;
	public void ChestCompression() {
		ClosePanel();
		HideMainButtons();

		foreach (Camera camera in cameras) {
			camera.animation ["chestCompZoom"].time = 0;
			camera.animation ["chestCompZoom"].speed = 1.0f;
			camera.animation.Play ("chestCompZoom");
		}
		AnimationHandler.Instance.HandleAnimation("ButtonChest");

		OpenPanel(ChestCompressionPanel);
	}

	//
	// Administer Fluids Panels
	//

	public void AdministerIV() {
		ClosePanel();
		previousPanel = PerformInterventionsPanel;

		OpenPanel(IVMedicationsPanel);
	}

	public void AdministerInhaled() {
		ClosePanel();
		previousPanel = PerformInterventionsPanel;

		OpenPanel(InhaledMedicationsPanel);
	}

	//
	// IV Medications Panels
	//

	public void Dopamine() {
		ClosePanel();

		TestHandler.Instance.Toast("Dopamine Administered");
	}

	public void Dobutamine() {
		ClosePanel();
		
		TestHandler.Instance.Toast("Dobutamine Administered");
	}

	public void Hydrocortisone() {
		ClosePanel();
		
		TestHandler.Instance.Toast("Hydrocortisone Administered");
	}

	public void AdministerSaline() {
		ClosePanel();
		
		TestHandler.Instance.Toast("Saline Administered");
	}

	public void Epinephrine() {
		ClosePanel();
		
		TestHandler.Instance.Toast("Epinephrine Administered");
	}

	public void ProstaglandinDrip() {
		ClosePanel();
		
		TestHandler.Instance.Toast("Prostaglandin Administered");
	}

	//
	// Inhaled Medications Panels
	//

	public void Albuterol() {
		ClosePanel();
		
		TestHandler.Instance.Toast("Albuterol Administered");
	}

	public void Racemic() {
		ClosePanel();
		
		TestHandler.Instance.Toast("Racemic Epinephrine Administered");
	}

	public void Atrovent() {
		ClosePanel();
		
		TestHandler.Instance.Toast("Atrovent Administered");
	}

	//
	// Probably move elsewhere?
	//
	
	public void LeftStethoscope() {
		if(leftChestTarget != null) {
			ArmAnimatorController.Instance.Stethescope(leftChestTarget);
			return;
		} else if(leftChestTarget == null) {
			throw new MissingReferenceException("Left stethoscope target is missing");
		}
	}

	public void RightStethoscope() {
		if(rightChestTarget != null) {
			ArmAnimatorController.Instance.Stethescope(rightChestTarget);
			return;
		} else if(rightChestTarget == null) {
			throw new MissingReferenceException("Right stethoscope target is missing");
		}
	}

	public void TransilluminateLeft() {
		mainCamera.GetComponent<TransilluminationLeft> ().CamToggle();
	}

	public void TransilluminateRight() {
		mainCamera.GetComponent<TransilluminationRight> ().CamToggle();
	}

	public void BeginChestCompression() {
		AnimationHandler.Instance.HandleAnimation("StartCC");
	}

	public void EndChestCompression() {
		ClosePanel();
		ShowMainButtons();

		AnimationHandler.Instance.HandleAnimation("EndCC");
	}
}
