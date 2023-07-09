using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Draws the field/property ONLY if the compared property compared by the comparison type with the value of comparedValue returns true.
/// Based on: https://forum.unity.com/threads/draw-a-field-only-if-a-condition-is-met.448855/
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class DrawIfAttribute : PropertyAttribute
{
    #region Fields

    public string comparedPropertyName { get; private set; }
    public object comparedValue { get; private set; }
    public ComparisonType comparisonType { get; private set; }
    public DisablingType disablingType { get; private set; }

    /// <summary>
    /// Types of comperisons.
    /// </summary>
    public enum DisablingType
    {
        ReadOnly = 2,
        DontDraw = 3
    }

    /// <summary>
    /// Types of comperisons.
    /// </summary>
    public enum ComparisonType
    {
        Equals = 1,
        NotEqual = 2,
        GreaterThan = 3,
        SmallerThan = 4,
        SmallerOrEqual = 5,
        GreaterOrEqual = 6
    }

    #endregion

    /// <summary>
    /// Only draws the field only if a condition is met. Supports enum and bools.
    /// </summary>
    /// <param name="comparedPropertyName">The name of the property that is being compared (case sensitive).</param>
    /// <param name="comparedValue">The value the property is being compared to.</param>
    /// <param name="disablingType">The type of disabling that should happen if the condition is NOT met. Defaulted to DisablingType.DontDraw.</param>
    public DrawIfAttribute(string comparedPropertyName, object comparedValue, ComparisonType comparisonType = ComparisonType.Equals, DisablingType disablingType = DisablingType.DontDraw)
    {
        this.comparedPropertyName = comparedPropertyName;
        this.comparedValue = comparedValue.GetType().IsEnum ? (int)comparedValue : comparedValue;
        this.comparisonType = comparisonType;
        this.disablingType = disablingType;
    }
}

#if UNITY_EDITOR

/// <summary>
/// Based on: https://forum.unity.com/threads/draw-a-field-only-if-a-condition-is-met.448855/
/// </summary>
[CustomPropertyDrawer(typeof(DrawIfAttribute))]
public class DrawIfPropertyDrawer : PropertyDrawer
{
    #region Fields

    // Reference to the attribute on the property.
    DrawIfAttribute drawIf;

    // Field that is being compared.
    SerializedProperty comparedField;

    #endregion

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (!ShowMe(property) && drawIf.disablingType == DrawIfAttribute.DisablingType.DontDraw)
            return 0f;

        // The height of the property should be defaulted to the default height.
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    /// <summary>
    /// Errors default to showing the property.
    /// </summary>
    private bool ShowMe(SerializedProperty property)
    {
        drawIf = attribute as DrawIfAttribute;
        // Replace propertyname to the value from the parameter
        string path = property.propertyPath.Contains(".") ? System.IO.Path.ChangeExtension(property.propertyPath, drawIf.comparedPropertyName) : drawIf.comparedPropertyName;

        comparedField = property.serializedObject.FindProperty(path);

        if (comparedField == null)
        {
            Debug.LogError("Cannot find property with name: " + path);
            return true;
        }

        // Get the value of the compared field.
        object comparedFieldValue = comparedField.boxedValue;

        // References to the values as numeric types.
        NumericType numericComparedFieldValue = null;
        NumericType numericComparedValue = null;

        try
        {
            // Try to set the numeric types.
            numericComparedFieldValue = new NumericType(comparedFieldValue);
            numericComparedValue = new NumericType(drawIf.comparedValue);
            // Debug.Log($"<color=yellow>DrawIfPropertyDrawer</color> <color=orange>{numericComparedFieldValue} {drawIf.comparisonType} {numericComparedValue}</color>");
        }
        catch (Exceptions.NumericTypeExpectedException)
        {
            // This place will only be reached if the type is not a numeric one. If the comparison type is not valid for the compared field type, log an error.
            if (drawIf.comparisonType != DrawIfAttribute.ComparisonType.Equals && drawIf.comparisonType != DrawIfAttribute.ComparisonType.NotEqual)
            {
                Debug.LogError("The only comparsion types available to type '" + comparedFieldValue.GetType() + "' are Equals and NotEqual. (On object '" + property.serializedObject.targetObject.name + "')");
                return true;
            }
        }

        // Is the condition met? Should the field be drawn?
        bool conditionMet = drawIf.comparisonType switch
        {
            DrawIfAttribute.ComparisonType.Equals => comparedFieldValue.Equals(drawIf.comparedValue),
            DrawIfAttribute.ComparisonType.NotEqual => !comparedFieldValue.Equals(drawIf.comparedValue),
            DrawIfAttribute.ComparisonType.GreaterThan => numericComparedFieldValue > numericComparedValue,
            DrawIfAttribute.ComparisonType.SmallerThan => numericComparedFieldValue < numericComparedValue,
            DrawIfAttribute.ComparisonType.SmallerOrEqual => numericComparedFieldValue <= numericComparedValue,
            DrawIfAttribute.ComparisonType.GreaterOrEqual => numericComparedFieldValue >= numericComparedValue,
            _ => false,
        };

        // Debug.Log($"<color=yellow>DrawIfPropertyDrawer</color> <color=white>{property.propertyPath}</color> <color=orange>compared to</color> <color=white>{drawIf.comparedPropertyName}</color> <color=orange>is</color> <color=cyan>{conditionMet}</color>\n" +
        //     $"<color=lime>comparedFieldValue</color>:<color=white>{comparedFieldValue}</color> - <color=lime>comparedValue</color>:<color=white>{drawIf.comparedValue}</color>");

        // Return the condition.
        return conditionMet;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // If the condition is met, simply draw the field.
        if (ShowMe(property))
        {
            EditorGUI.PropertyField(position, property, label, true);
        } //...check if the disabling type is read only. If it is, draw it disabled
        else if (drawIf.disablingType == DrawIfAttribute.DisablingType.ReadOnly)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }

}
#endif