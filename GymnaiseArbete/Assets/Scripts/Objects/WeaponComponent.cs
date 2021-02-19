using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR 
using UnityEditor;
#endif

public class WeaponComponent : MonoBehaviour
{

    

    [HideInInspector]
    public enum ComponentType { Stock, Frame, Barrel, Magazine }

    public ComponentType componentState;


    
    [SerializeField]
    [HideInInspector]
    public float accuracy, shootForce, maxAmmo, fireRate, damage;

    [SerializeField]
    public Transform[] positions;

    #region Editor
#if UNITY_EDITOR
    [CustomEditor(typeof(WeaponComponent)), CanEditMultipleObjects]
    public class WeaponComponentEditor : Editor
    {
        public SerializedProperty
        stateProp, stockProp, frameProp, barrelProp, magazineProp;

        private void OnEnable()
        {
            stateProp = serializedObject.FindProperty("componentState");
            stockProp = serializedObject.FindProperty("Stock");
            frameProp = serializedObject.FindProperty("Frame");
            barrelProp = serializedObject.FindProperty("Barrel");
            magazineProp = serializedObject.FindProperty("Magazine");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            base.OnInspectorGUI();

            WeaponComponent weapComp = (WeaponComponent)target;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("READ ONLY", EditorStyles.boldLabel, GUILayout.MaxWidth(90));
            EditorGUILayout.PropertyField(stateProp);

            WeaponComponent.ComponentType setCompType = (WeaponComponent.ComponentType)stateProp.enumValueIndex;


            EditorGUILayout.Space();


            EditorGUILayout.LabelField("Details", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            switch (setCompType)
            {

                    //Stock
                case WeaponComponent.ComponentType.Stock:

                    EditorGUILayout.LabelField("Stock", GUILayout.MaxWidth(70));

                    EditorGUILayout.LabelField("Accuracy", GUILayout.MaxWidth(70));
                    weapComp.accuracy = EditorGUILayout.FloatField(weapComp.accuracy, GUILayout.MaxWidth(50));
                    weapComp.positions = new Transform[0];

                    weapComp.maxAmmo = 0;
                    weapComp.shootForce = 0;
                    weapComp.fireRate = 0;
                    weapComp.damage = 0;

                    break;

                    //Frame
                case WeaponComponent.ComponentType.Frame:

                    EditorGUILayout.LabelField("Frame", GUILayout.MaxWidth(70));

                    EditorGUILayout.LabelField("Fire rate", GUILayout.MaxWidth(70));
                    weapComp.fireRate = EditorGUILayout.FloatField(weapComp.fireRate, GUILayout.MaxWidth(50));

                    EditorGUILayout.LabelField("Shoot force", GUILayout.MaxWidth(70));
                    weapComp.shootForce = EditorGUILayout.FloatField(weapComp.shootForce, GUILayout.MaxWidth(50));
                    weapComp.positions = new Transform[3];

                    weapComp.maxAmmo = 0f;
                    weapComp.damage = 0f;
                    weapComp.accuracy = 0f;
                    break;

                    //Barrel
                case WeaponComponent.ComponentType.Barrel:

                    EditorGUILayout.LabelField("Barrel", GUILayout.MaxWidth(70));

                    EditorGUILayout.LabelField("Damage", GUILayout.MaxWidth(70));
                    weapComp.damage = EditorGUILayout.FloatField(weapComp.damage, GUILayout.MaxWidth(50));
                    weapComp.positions = new Transform[0];

                    weapComp.maxAmmo = 0;
                    weapComp.shootForce = 0;
                    weapComp.fireRate = 0;
                    weapComp.accuracy = 0;
                    break;

                    //Magazine
                case WeaponComponent.ComponentType.Magazine:

                    EditorGUILayout.LabelField("Magazine", GUILayout.MaxWidth(70));

                    EditorGUILayout.LabelField("Max Ammo", GUILayout.MaxWidth(70));
                    weapComp.maxAmmo = EditorGUILayout.FloatField(weapComp.maxAmmo, GUILayout.MaxWidth(50));
                    weapComp.positions = new Transform[0];

                    weapComp.damage = 0;
                    weapComp.shootForce = 0;
                    weapComp.fireRate = 0;
                    weapComp.accuracy = 0;
                    break;
            }
            EditorGUILayout.EndHorizontal();
        }
    }
#endif
    #endregion

}
