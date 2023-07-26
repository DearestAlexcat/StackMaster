using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackViewService : MonoBehaviour, IEnumerable<TrackView>
{
    private LinkedList<TrackView> tracksViews = new LinkedList<TrackView>();

    public void AddLast(TrackView value)
    {
        tracksViews.AddLast(value);
    }

    public TrackView Last()
    {
        return tracksViews.Last.Value;
    }

    public void RemoveFirst()
    {
        Object.Destroy(tracksViews.First.Value.gameObject);
        tracksViews.RemoveFirst();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public IEnumerator<TrackView> GetEnumerator()
    {
        foreach (var item in tracksViews)
        {
            yield return item;
        }
    }
}
