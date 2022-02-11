using System.Collections;
using UnityEngine;
public interface IUpdaterProfile
{
    public IEnumerator UpdateUrl(string url);
    public IEnumerator UpdateCondition(string sost);
}