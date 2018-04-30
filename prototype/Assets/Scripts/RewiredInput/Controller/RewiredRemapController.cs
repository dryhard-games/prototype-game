namespace Decoy.Feature.RewiredInput {
    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections.Generic;
    using Rewired;
    using Decoy.Core.Utilities;
    using Decoy.Core.EventSystem;
    using Decoy.Core.InputSystem;
    /// <summary>
    /// RewiredRemapController.cs
    /// <summary>
    /// Author: Thomas van Opstal
    /// </summary>
    [DisallowMultipleComponent]
    public class RewiredRemapController : MonoBehaviour {
        [Header("Models")]
        public RewiredRemapModel remapModel;
        public RewiredGlyphController gamepadGlyphController;

        [Header("Group Transforms")]
        public RectTransform fieldGroupTransform;
        public RectTransform actionGroupTransform;

        [Header("Status Text")]
        public Text inputDevice;

        [Header("Pop up panels")]
        public GameObject conflictResolveButtons;

        [Header("Navigation Requirements")]
        public Button staticSelectableTop;
        public Button staticSelectableBottom;

        private const string category = "Default";
        private const string layout = "Default";
        private const string setNavigation = "SetNavigation";
        private const int playerID = 0;

        private InputMapper inputMapper = new InputMapper();
        private ControllerType selectedControllerType = ControllerType.Keyboard;
        private int selectedControllerId = 0;

        private List<Row> rows = new List<Row>();

        private InputMapper.ConflictFoundEventData conflictData;

        private Player RewiredPlayer {
            get {
                return ReInput.players.GetPlayer(playerID);
            }
        }

        private Controller Controller {
            get {
                return RewiredPlayer.controllers.GetController(selectedControllerType, selectedControllerId);
            }
        }

        private ControllerMap ControllerMap {
            get {
                if (Controller == null)
                    return null;

                return RewiredPlayer.controllers.maps.GetMap(Controller.type, Controller.id, category, layout);
            }
        }

        private void Start() {
            if (!ReInput.isReady)
                return;

            //Set up the Input Mapper
            inputMapper.options.timeout = remapModel.timeOut;
            inputMapper.options.ignoreMouseYAxis = remapModel.ignoreMouseYAxis;
            inputMapper.options.ignoreMouseXAxis = remapModel.ignoreMouseXAxis;
            inputMapper.options.checkForConflicts = remapModel.checkForConflicts;
            inputMapper.options.defaultActionWhenConflictFound = remapModel.conflictResponse;

            //Subscribe to events
            inputMapper.InputMappedEvent += OnInputMapped;
            inputMapper.ConflictFoundEvent += OnConflictFound;

            ReInput.ControllerConnectedEvent += OnControllerChanged;
            ReInput.ControllerDisconnectedEvent += OnControllerChanged;

            //Create the buttons
            InitializeRemapUI();
        }

        private void OnDestroy() {
            inputMapper.Stop();

            inputMapper.RemoveAllEventListeners();
            ReInput.ControllerConnectedEvent -= OnControllerChanged;
            ReInput.ControllerDisconnectedEvent -= OnControllerChanged;
        }

        private void InitializeRemapUI() {
            rows = new List<Row>();

            // Delete placeholders
            foreach (Transform t in actionGroupTransform)
                Destroy(t.gameObject);

            foreach (Transform t in fieldGroupTransform)
                Destroy(t.gameObject);

            if (selectedControllerType == ControllerType.Joystick && RewiredPlayer.controllers.joystickCount == 0) {
                inputDevice.text = "No Gamepad found";
                return;
            } else if (selectedControllerType == ControllerType.Mouse && !RewiredPlayer.controllers.hasMouse) {
                inputDevice.text = "No Mouse found";
                return;
            } else if (selectedControllerType == ControllerType.Keyboard && !RewiredPlayer.controllers.hasKeyboard) {
                inputDevice.text = "No Keyboard found";
                return;
            }

            foreach (InputAction action in ReInput.mapping.Actions) {
                if (!action.userAssignable)
                    continue;

                if (action.type == InputActionType.Axis) {
                    //Switch instead of if, makes it easier to add types if needed.
                    switch (selectedControllerType) {
                        case ControllerType.Joystick:
                            CreateRow(action, AxisRange.Full, action.descriptiveName);
                            break;
                        case ControllerType.Keyboard:
                            bool exceptionFound = false;

                            for (int i = 0; i < remapModel.keyboardExceptions.Length; i++) {
                                if (action.name == remapModel.keyboardExceptions[i].ToString()) {
                                    exceptionFound = true;
                                    break;
                                }
                            }

                            if (exceptionFound)
                                continue;

                            CreateRow(action, AxisRange.Positive, !string.IsNullOrEmpty(action.positiveDescriptiveName) ? action.positiveDescriptiveName : action.descriptiveName);
                            CreateRow(action, AxisRange.Negative, !string.IsNullOrEmpty(action.negativeDescriptiveName) ? action.negativeDescriptiveName : action.descriptiveName);
                            break;
                    }
                } else if (action.type == InputActionType.Button)
                    CreateRow(action, AxisRange.Positive, action.descriptiveName);
            }

            RedrawUI();
            Invoke(setNavigation, .1f);
        }

        private void SetNavigation() {
            DynamicUINavigation.SetNavigationDynamicButtonGroup(fieldGroupTransform.GetComponentsInChildren<Button>(), staticSelectableTop, staticSelectableBottom);
        }

        private void CreateRow(InputAction action, AxisRange actionRange, string label) {
            //Setup name
            GameObject labelObject = Instantiate(remapModel.textPrefab, actionGroupTransform);
            labelObject.transform.SetAsLastSibling();
            labelObject.GetComponent<Text>().text = label;

            //Setup button
            GameObject buttonObject = Instantiate(remapModel.buttonPrefab, fieldGroupTransform);
            buttonObject.transform.SetAsLastSibling();
            buttonObject.name = action.name;

            //Add row to list
            rows.Add(new Row {
                action = action,
                actionRange = actionRange,
                button = buttonObject.GetComponent<Button>(),
                text = buttonObject.GetComponentInChildren<Text>(),
                glyph = buttonObject.GetComponentsInChildren<Image>()[1]
            });
        }

        private void RedrawUI() {
            if (Controller == null)
                return;

            inputDevice.text = Controller.name;

            for (int i = 0; i < rows.Count; i++) {
                Row row = rows[i];
                InputAction action = rows[i].action;

                string name = string.Empty;
                int actionElementMapId = -1;
                row.glyph.enabled = false;

                foreach (var actionElementMap in ControllerMap.ElementMapsWithAction(action.id)) {
                    if (actionElementMap.ShowInField(row.actionRange)) {
                        name = actionElementMap.elementIdentifierName;
                        actionElementMapId = actionElementMap.id;

                        if (selectedControllerType == ControllerType.Joystick) {
                            Sprite glyph = gamepadGlyphController.GetGlyph(((Joystick)Controller).hardwareTypeGuid.ToString(), actionElementMap.elementIdentifierId, actionElementMap.axisRange);
                            row.glyph.sprite = glyph;
                            row.glyph.enabled = !(glyph == null);
                            row.text.enabled = (glyph == null);
                        }
                        break;
                    }
                }

                row.text.text = name;
                row.button.onClick.RemoveAllListeners();
                int index = i;
                row.button.onClick.AddListener(() => OnInputFieldClicked(index, actionElementMapId));
            }
        }

        private void OnInputFieldClicked(int index, int actionElementMapToReplaceId) {
            if (index < 0 || index >= rows.Count || Controller == null)
                return;

            inputMapper.Start(new InputMapper.Context() {
                actionId = rows[index].action.id,
                controllerMap = ControllerMap,
                actionRange = rows[index].actionRange,
                actionElementMapToReplace = ControllerMap.GetElementMap(actionElementMapToReplaceId)
            });
        }

        private void OnConflictFound(InputMapper.ConflictFoundEventData data) {
            conflictData = data;

            if (data.isProtected)
                conflictData.responseCallback(InputMapper.ConflictResponse.Cancel);
            else
                conflictResolveButtons.SetActive(true);
        }

        private void SetSelectedController(ControllerType controllerType) {
            bool changed = false;

            if (controllerType != selectedControllerType) {
                selectedControllerType = controllerType;
                changed = true;
            }

            int origId = selectedControllerId;

            if (selectedControllerType == ControllerType.Joystick) {
                if (RewiredPlayer.controllers.joystickCount > 0)
                    selectedControllerId = RewiredPlayer.controllers.Joysticks[0].id;
                else
                    selectedControllerId = -1;
            } else
                selectedControllerId = 0;

            if (selectedControllerId != origId)
                changed = true;

            if (changed) {
                inputMapper.Stop();
                InitializeRemapUI();
            }
        }

        private void OnInputMapped(InputMapper.InputMappedEventData data) {
            RedrawUI();

            EventManager.TriggerEvent(InputEventTypes.SAVE_INPUT_MAP);
        }

        private void OnControllerChanged(ControllerStatusChangedEventArgs data) {
            SetSelectedController(selectedControllerType);
        }

        //Button Listeners
        public void OnControllerSelected(int index) {
            SetSelectedController((ControllerType)index);
        }

        public void OnConflictResolveOptionSelected(int index) {
            conflictData.responseCallback((InputMapper.ConflictResponse)index);
            conflictResolveButtons.SetActive(false);
        }

        public void OnLoadDefaultSettingsSelected(int index) {
            RewiredPlayer.controllers.maps.LoadDefaultMaps((ControllerType)index);
            InitializeRemapUI();
        }
    }
}
