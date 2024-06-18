using UnityEngine;

namespace Client
{
    static class GameInitialization
    {
        public static void FullInit()
        {
            InitializeUI();
        }

        public static void InitializeUI()
        {
            var config = Service<StaticData>.Get();

            var ui = Service<UI>.Get();

            if (ui == null)
            {
                ui = Object.Instantiate(config.UI);
                Service<UI>.Set(ui);
            }
        }
    }
}