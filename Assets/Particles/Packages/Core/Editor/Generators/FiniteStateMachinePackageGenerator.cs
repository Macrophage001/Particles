using System;
using System.IO;
using Particles.Packages.Core.Runtime.FSM;
using UnityEditor;
using UnityEngine;
using File = System.IO.File;
using TypeHelper = Particles.Packages.Core.Editor.Helpers.TypeHelper;

namespace Particles.Packages.Core.Editor.Generators
{
    [CustomEditor(typeof(ParticleBaseFiniteStateMachine), true)]
    public class FiniteStateMachinePackageGenerator : UnityEditor.Editor
    {
        private bool alreadyExists;

        private static bool TryGenerateNecessaryTypes(Type genericType)
        {
            var typeName = genericType.Name;
            var stateTypeName = $"State<{typeName}, FiniteStateMachine<{typeName}>>";
            var transitionTypeName = $"Transition<{typeName}, FiniteStateMachine<{typeName}>>";

            bool doesStateTypeExist = false;
            bool doesTransitionTypeExist = false;
            if ((doesStateTypeExist = TypeHelper.TryFindSubtypeOfGenericString(stateTypeName, out var _) == true) && 
                (doesTransitionTypeExist = TypeHelper.TryFindSubtypeOfGenericString(transitionTypeName, out var _)) == true)
                return true;
            
            if (!doesStateTypeExist) GenerateRequiredStateType(genericType);
            if (!doesTransitionTypeExist) GenerateRequiredTransitionType(genericType);
            
            return false;
        }

        private static void GenerateRequiredStateType(Type targetContext, string relativeFilePath = "")
        {
            var templateStateTypeString =
                $@"// Path: Assets\Particles\Runtime\FSM\States\State<{targetContext.Name}, FiniteStateMachine<{targetContext.Name}>>

using Particles.Core.Runtime.FSM;
using UnityEngine;

namespace FSM.States
{{
    [CreateAssetMenu(menuName = ""Game/FSM/States/{targetContext.Name}State"")]
    public class {targetContext.Name}State : State<{targetContext.Name}, FiniteStateMachine<{targetContext.Name}>> {{ }}
}}
";
            var filePath = Path.Combine(Directory.GetParent(Application.dataPath).FullName, "Assets/");
            Debug.Log($"File Path: {filePath}");
            if (!string.IsNullOrEmpty(relativeFilePath))
            {
                filePath = Path.Combine(relativeFilePath, filePath);
            }
            filePath = Path.Combine(filePath, $"{targetContext.Name}State.cs");
            File.WriteAllText(filePath, templateStateTypeString);
            AssetDatabase.Refresh();
        }

        private static void GenerateRequiredTransitionType(Type targetContext, string relativeFilePath = "")
        {
            var templateTransitionTypeString =
                $@"// Path: Assets\Particles\Runtime\FSM\Transitions\Transition<{targetContext.Name}, FiniteStateMachine<{targetContext.Name}>>

using Particles.Core.Runtime.FSM;
using UnityEngine;

namespace FSM.Transitions
{{
    [CreateAssetMenu(menuName = ""Game/FSM/Transitions/{targetContext.Name} State Transition"")]
    public class {targetContext.Name}StateTransition : Transition<{targetContext.Name}, FiniteStateMachine<{targetContext.Name}>> {{ }}
}}
";
            string filePath = Path.Combine(Directory.GetParent(Application.dataPath).FullName, "Assets/");
            Debug.Log($"File Path: {filePath}");
            if (!string.IsNullOrEmpty(relativeFilePath))
            {
                filePath = Path.Combine(relativeFilePath, filePath);
            }
            filePath = Path.Combine(filePath, $"{targetContext.Name}StateTransition.cs");
            File.WriteAllText(filePath, templateTransitionTypeString);
            AssetDatabase.Refresh();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!GUILayout.Button("Generate")) return;
            var targetType = target.GetType();
            var baseType = targetType.BaseType;

            if (baseType is not { IsGenericType: true }) return;
            var targetContext = baseType.GetGenericArguments()[0];
            if (TryGenerateNecessaryTypes(targetContext))
            {
                Debug.Log("All types already exist");
            }
            else
            {
                Debug.Log("Types generated");
            }
        }
    }
}
