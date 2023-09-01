using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Story", menuName = "athena/Story", order = 0)]
public class StorySO : ScriptableObject
{
    public List<StoryPanel> Panels;

}

