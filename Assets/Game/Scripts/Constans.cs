using UnityEngine;

namespace Assets.Game.Scripts
{
    public static class Constans
    {
        public const float Epsilon = 0.001f;
        public const float MaxValueOpeningProgress = 100f;
        public const string TurretSaveOpenProgressName = "Turret";
        public const float TurretOpeningFactor = 1f;

        public static Color ClosedColor { get => Color.black; }
        public static Color OpenColor { get => Color.white; }
        public static Color ReadyColor { get => Color.green; }

        #region Paths

        //PlayerPath
        public const string HeroPath = "Prefabs/Entity/Player/Player";

        #region PopupsPath

        //OverlayPopupPath
        public const string OverlayPopupPath = "Prefabs/UI/Popups/OverlayPopup";

        //StartPopupPath
        public const string StartPopupPath = "Prefabs/UI/Popups/StartPopup";

        //StagePopupPath
        public const string StagePopupPath = "Prefabs/UI/Popups/StagePopup";

        //VictoryPopupPath
        public const string VictoryPopupPath = "Prefabs/UI/Popups/VictoryPopup";

        #endregion PopupsPath

        #region ObjectPath

        //TurretPath
        public const string TurretPath = "Prefabs/Entity/Turret/Turret";

        //BulletPath
        public const string ProjectilePath = "Prefabs/Entity/Bullet/Projectile";

        //EnemyPath
        public const string EnemyPath = "Prefabs/Entity/Enemy/Enemy";

        //WeaponPathS
        public const string WeaponPath = "Prefabs/Weapon/Weapon";

        //ErrorModel
        public const string ErrorModelPath = "ErrorModel/ErrorModel";

        #endregion ObjectPath

        #region DataPath

        //TurretsDataPath
        public const string TurretsDataPath = "ScriptableObject/TurretScriptableObjects";

        //GameFactory
        public const string GameFactorySettingsPath = "ScriptableObject/GameFactorySettings/GameFactorySettings";

        //LevelLoadDataPath
        public const string LevelLoadDataPath = "ScriptableObject/LevelScriptableObject/LevelLoadData";

        //TurretsDataPath
        public const string UITurretCardsPath = "Prefabs/UI/UITurretCards/TurretCard";

        //SpawnScriptableObjectsScenes
        public const string SpawnScriptableObjectsScenesPath = "ScriptableObject/SpawnerScriptableObject/Scene ";

        //SpawnScriptableObjectslevels
        public const string SpawnScriptableObjectsLevelsPath = "/Level ";

        //SpawnScriptableObjectsStages
        public const string SpawnScriptableObjectsStagesPath = "/Stage ";

        #endregion DataPath

        #endregion Paths
    }
}