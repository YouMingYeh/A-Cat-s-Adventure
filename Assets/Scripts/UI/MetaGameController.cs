using Platformer.Mechanics;
using Platformer.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer.UI
{
    /// <summary>
    /// The MetaGameController is responsible for switching control between the high level
    /// contexts of the application, eg the Main Menu and Gameplay systems.
    /// </summary>
    public class MetaGameController : MonoBehaviour
    {
        /// <summary>
        /// The main UI object which used for the menu.
        /// </summary>
        public MainUIController mainMenu;

        /// <summary>
        /// A list of canvas objects which are used during gameplay (when the main ui is turned off)
        /// </summary>
        public Canvas[] gamePlayCanvasii;

        /// <summary>
        /// The game controller.
        /// </summary>
        public GameController gameController;
        
        bool showMainCanvas = false;

        bool showEndCanvas = false;
        public Canvas EndCanvas;

        bool showStartCanvas = true;
        public Canvas StartCanvas;

        public void ToggleMenuCanvas()
        {
            if (showStartCanvas) //close
            {
                Time.timeScale = 1;
                StartCanvas.gameObject.SetActive(false);
                foreach (var i in gamePlayCanvasii) i.gameObject.SetActive(true);
            }
            else //open
            {
                Time.timeScale = 0;
                StartCanvas.gameObject.SetActive(true);
                foreach (var i in gamePlayCanvasii) i.gameObject.SetActive(false);
            }
        }
        

        public void ToggleEndCanvas()
        {
            if (showEndCanvas) //close
            {
                Time.timeScale = 1;
                EndCanvas.gameObject.SetActive(false);
                foreach (var i in gamePlayCanvasii) i.gameObject.SetActive(true);
            }
            else //open
            {
                Time.timeScale = 0;
                EndCanvas.gameObject.SetActive(true);
                foreach (var i in gamePlayCanvasii) i.gameObject.SetActive(false);
            }
        }

        void OnEnable()
        {

            
            if (StaticClass.IsRestart)
            {
                Time.timeScale = 1;
                mainMenu.gameObject.SetActive(false);
                foreach (var i in gamePlayCanvasii) i.gameObject.SetActive(false);
            }
            else
            {
                Time.timeScale = 1;
                mainMenu.gameObject.SetActive(false);
                foreach (var i in gamePlayCanvasii) i.gameObject.SetActive(true);
            }

            if(StaticClass.IsStarted)
            {
                StartCanvas.gameObject.SetActive(false);

            } else
            {
                StartCanvas.gameObject.SetActive(true);
                mainMenu.gameObject.SetActive(false);
                foreach (var i in gamePlayCanvasii) i.gameObject.SetActive(false);
            }
           
        }

        /// <summary>
        /// Turn the main menu on or off.
        /// </summary>
        /// <param name="show"></param>
        public void ToggleMainMenu(bool show)
        {
            if (this.showMainCanvas != show)
            {
                _ToggleMainMenu(show);
            }
        }

        public void OpenMainMenu()
        {
            mainMenu.gameObject.SetActive(true);
        }

        void _ToggleMainMenu(bool show)
        {
            if (show)
            {
                Time.timeScale = 0;
                mainMenu.gameObject.SetActive(true);
                foreach (var i in gamePlayCanvasii) i.gameObject.SetActive(false);
            }
            else
            {
                Time.timeScale = 1;
                mainMenu.gameObject.SetActive(false);
                if(!showStartCanvas)
                    foreach (var i in gamePlayCanvasii) i.gameObject.SetActive(true);
            }
            this.showMainCanvas = show;
        }

        void Update()
        {
            if (Input.GetButtonDown("Menu"))
            {
                ToggleMainMenu(show: !showMainCanvas);
            }
        }

        public void RestartGame()
        {
            StaticClass.IsRestart = false;
            StaticClass.IsStarted = true;

            SceneManager.LoadSceneAsync("MainScene");
        }

        public void ExitGame()
        {
            StaticClass.IsRestart = true;
            StaticClass.IsStarted = false;
            //SceneManager.LoadSceneAsync("MenuScene");
            SceneManager.LoadSceneAsync("MainScene");
            
        }

    }
}
