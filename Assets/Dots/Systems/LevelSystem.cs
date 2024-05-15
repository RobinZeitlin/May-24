using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace ECS
{
    public partial class LevelSystem : SystemBase
    {
        public static LevelSystem instance;

        public void Initialize()
        {
            instance = this;
        }

        private int _currentLevel;

        public int currentLevel 
        {
            get
            { 
                return _currentLevel;
            }
            set
            {
                if(_currentLevel != value)
                {
                    _currentLevel = value;
                }
            }
        }

        public void ChangeLevel(int level)
        {
            currentLevel = level;
            Debug.Log("Its a me");
            ClearEntitys();
        }

        public void ClearEntitys()
        {
            PlaceFences.instance.ResetFences();

            EntitySpawnerSystem.instance.ClearAllEntitys(this.EntityManager);
        }

        protected override void OnUpdate()
        {
            
        }
    }
}
