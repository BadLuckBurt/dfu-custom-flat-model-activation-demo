using UnityEngine;
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
        public static GameObject CustomFlatModelActivation;

        public static GameObject addCrystalLight;

        //like in the last example, this is used to setup the Mod.  This gets called at Start state.
        [Invoke]
        public static void InitAtStartState(InitParams initParams)
        {
            mod = initParams.Mod;
            var go = new GameObject(mod.Title);
            go.AddComponent<CustomFlatModelActivationModLoader>();
			
            //var addCrystalLight = new AddCrystalLight();

            Debug.Log("Started setup of : " + mod.Title);
            //start loading all assets asynchrousnly - the bool paramater tells it to unload the asset bundle since all assets are loaded
            //ModManager.Instance.GetComponent<MonoBehaviour>().StartCoroutine(mod.LoadAllAssetsFromBundleAsync(false));

            PlayerActivate.RegisterCustomActivation(210, 1, CampfireActivation);
            PlayerActivate.RegisterCustomActivation(211, 20, ScarecrowActivation);
            PlayerActivate.RegisterCustomActivation(41005, BroomstickCabinetActivation);
            PlayerActivate.RegisterCustomActivation(41030, EmptyBookshelfActivation);

            PlayerActivate.RegisterCustomActivation(57089, CrystalActivation);

            mod.IsReady = true;
        }

        private static void ShowMessageBox(string message, bool clickAnywhereToClose = false)
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

        private static void CrystalActivation(Transform transform)
        {
            string message = "Crystal activated";
            Debug.Log(message);
            ShowMessageBox(message);
        }

        private static void EmptyBookshelfActivation(Transform transform)
        {
            string message = "This is a empty bookshelf replacement mesh";
            Debug.Log(message);
            ShowMessageBox(message);
        }

        private static void BroomstickCabinetActivation(Transform transform)
        {
            string message = "This is a classic DF broomstick cabinet model";
            Debug.Log(message);
            ShowMessageBox(message);
        }

        private static void ScarecrowActivation(Transform transform)
        {
            string message = "Oh nose, it's a scarecrow replacement mesh";
            Debug.Log(message);
            ShowMessageBox(message);
        }

        private static void CampfireActivation(Transform transform)
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
