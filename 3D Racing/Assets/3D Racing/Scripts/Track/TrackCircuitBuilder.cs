using UnityEngine;

namespace UnityDrift
{
    public static class TrackCircuitBuilder
    {
        public static TrackPoint[] Build(Transform trackTransform, TrackType trackType)
        {
            TrackPoint[] points = new TrackPoint[trackTransform.childCount];

            InitPoints(trackTransform, points);
            SetLinks(points, trackType);
            MarkPoints(points, trackType);

            return points;
        }

        private static void InitPoints(Transform trackTransform, TrackPoint[] points)
        {
            for (int i = 0; i < points.Length; i++)
            {
                if (trackTransform.GetChild(i).TryGetComponent(out TrackPoint point))
                {
                    points[i] = point;
                }
                else
                {
                    Debug.LogError($"There is no TrackPoint script on {i} child object");
                    return;
                }

                points[i].Reset();
            }

            points[0].AssignAsTarget();
        }

        private static void SetLinks(TrackPoint[] points, TrackType trackType)
        {
            for (int i = 0; i < points.Length - 1; i++)
                points[i].SetNext(points[i + 1]);

            if (trackType == TrackType.Circular)
                points[points.Length - 1].SetNext(points[0]);
        }

        private static void MarkPoints(TrackPoint[] points, TrackType trackType)
        {
            if (trackType == TrackType.Circular)
            {
                points[0].SetCircularFirst();
            }

            if (trackType == TrackType.Sprint)
            {
                points[0].SetFirst();
                points[points.Length - 1].SetLast();
            }
        }
    }
}
