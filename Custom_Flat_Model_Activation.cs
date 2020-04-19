using UnityEngine;
using System;
using System.Collections;
using DaggerfallWorkshop.Game;
using DaggerfallWorkshop.Game.Utility.ModSupport;
using DaggerfallWorkshop.Game.UserInterface;
using DaggerfallWorkshop.Game.UserInterfaceWindows;

namespace BLB.CustomFlatModelActivation
{
    //this class initializes the mod.
    public class CustomFlatModelActivationModLoader : MonoBehaviour
    {
        public static Mod mod;
		public static GameObject go;

        //like in the last example, this is used to setup the Mod.  This gets called at Start state.
        [Invoke]
        public static void InitAtStartState(InitParams initParams)
        {
            mod = initParams.Mod;
            var go = new GameObject(mod.Title);
            go.AddComponent<CustomFlatModelActivationModLoader>();

            Debug.Log("Started setup of : " + mod.Title);

            PlayerActivate.RegisterCustomActivation(mod, 210, 1, CampfireActivation);
            PlayerActivate.RegisterCustomActivation(mod, 211, 20, ScarecrowActivation);
            PlayerActivate.RegisterCustomActivation(mod, 41005, BroomstickCabinetActivation);
            PlayerActivate.RegisterCustomActivation(mod, 41030, EmptyBookshelfActivation);

            PlayerActivate.RegisterCustomActivation(mod, 57089, CrystalActivation);

            mod.IsReady = true;
        }

        private static void ShowMessageBox(string message, bool clickAnywhereToClose = true)
        {
            DaggerfallMessageBox messageBox = new DaggerfallMessageBox(DaggerfallUI.UIManager);
            messageBox.ClickAnywhereToClose = clickAnywhereToClose;
            messageBox.ParentPanel.BackgroundColor = Color.clear;
            messageBox.ScreenDimColor = new Color32(0, 0, 0, 0);

            messageBox.SetText(message);

            //messageBox.AddButton(DaggerfallMessageBox.MessageBoxButtons.Yes);
            //messageBox.AddButton(DaggerfallMessageBox.MessageBoxButtons.No);
            messageBox.AddCustomButton(99, "SNEAK", false);
            messageBox.AddCustomButton(99, "YELL", false);
            messageBox.OnCustomButtonClick += Generic_messageBox_OnButtonClick;

            messageBox.Show();
        }

        public static void Generic_messageBox_OnButtonClick(DaggerfallMessageBox sender, string messageBoxButton)
        {
            if(messageBoxButton == "SNEAK") {
                Debug.Log("Sneak button was clicked");
            } else if(messageBoxButton == "YELL") {
                Debug.Log("Yell button was clicked");
            }
            sender.CloseWindow();
        }

        private static void CrystalActivation(RaycastHit hit)
        {
            string message = "Crystal activated";
            Debug.Log(message);
            ShowMessageBox(message);
        }

        private static void EmptyBookshelfActivation(RaycastHit hit)
        {
            string message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";
            Debug.Log(message);
            ShowMessageBox(message);
        }

        private static void BroomstickCabinetActivation(RaycastHit hit)
        {
            string message = "This is a classic DF broomstick cabinet model";
            Debug.Log(message);
            ShowMessageBox(message);
        }

        private static void ScarecrowActivation(RaycastHit hit)
        {
            string message = "Oh nose, it's a scarecrow replacement mesh";
            Debug.Log(message);
            ShowMessageBox(message);
        }

        private static void CampfireActivation(RaycastHit hit)
        {
            //Debug.Log("I CAN HAS ACTIVATED CAMPFIRES");
            IUserInterfaceManager uiManager = DaggerfallUI.UIManager;
            uiManager.PushWindow(new DaggerfallRestWindow(uiManager, true));
        }

        [Invoke(StateManager.StateTypes.Game)]
        public static void InitAtGameState(InitParams initParams)
        {

        }
    }
}
