using UnityEngine;

namespace UnityDrift
{
    public static class TrackCircuitBuilder
    {
        public static TrackPoint[] Build(Transform trackTransform, TrackType trackType, bool inverted)
        {
            if (trackType == TrackType.None)
                return null;

            TrackPoint[] points = new TrackPoint[trackTransform.childCount];

            InitPoints(trackTransform, points, trackType, inverted);
            SetLinks(points, trackType);
            MarkPoints(points, trackType);

            return points;
        }

        private static void InitPoints(Transform trackTransform, TrackPoint[] points, TrackType trackType, bool inverted)
        {
            if (inverted == true)
            {
                if (trackType == TrackType.Circular)
                {
                    InitPointWithIndex(trackTransform, points, 0, 0);

                    for (int i = points.Length - 1; i > 0; i--)
                        InitPointWithIndex(trackTransform, points, i, points.Length - i);
                }

                if (trackType == TrackType.Sprint)
                {
                    for (int i = points.Length - 1; i >= 0; i--)
                        InitPointWithIndex(trackTransform, points, i, points.Length - 1 - i);
                }
            }
            else
            {
                for (int i = 0; i < points.Length; i++)
                    InitPointWithIndex(trackTransform, points, i, i);
            }

            points[0].AssignAsTarget();
        }

        private static void InitPointWithIndex(Transform trackTransform, TrackPoint[] points, int pointIndex, int childIndex)
        {
            if (trackTransform.GetChild(childIndex).TryGetComponent(out TrackPoint point))
            {
                points[pointIndex] = point;
            }
            else
            {
                Debug.LogError($"There is no TrackPoint script on {pointIndex} child object");
                return;
            }

            points[pointIndex].Reset();
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
