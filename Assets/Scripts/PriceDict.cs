using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class PriceDict : SerializableDictionary<string, int> { }

// [CustomPropertyDrawer(typeof(PriceDict))]
// public class PriceDictPropertyDrawer : SerializableDictionaryPropertyDrawer {}