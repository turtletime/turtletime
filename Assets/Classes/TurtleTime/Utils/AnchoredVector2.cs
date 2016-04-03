using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TurtleTime
{
    /// <summary>
    /// A struct that represents a 2D position as a an offset from either a corner or a side of a rectangle.
    /// This 2D position may only be evaluated when the rectangle is specified.
    /// </summary>
    public struct AnchoredVector2
    {
        TextAnchor Anchor { get; set; }
        Vector2 Offset { get; set; }

        public AnchoredVector2(TextAnchor anchor, Vector2 offset)
        {
            this.Anchor = anchor;
            this.Offset = offset;
        }

        public Vector2 EvaluateWithRectangle(Rect rect)
        {
            Vector2 result = Vector2.zero;
            switch (Anchor)
            {
                case TextAnchor.LowerCenter:
                    result += new Vector2(rect.x + rect.width / 2, rect.y);
                    break;
                case TextAnchor.LowerLeft:
                    result += new Vector2(rect.x, rect.y);
                    break;
                case TextAnchor.LowerRight:
                    result += new Vector2(rect.x + rect.width, rect.y);
                    break;
                case TextAnchor.MiddleCenter:
                    result += rect.center;
                    break;
                case TextAnchor.MiddleLeft:
                    result += new Vector2(rect.x, rect.y + rect.height / 2);
                    break;
                case TextAnchor.MiddleRight:
                    result += new Vector2(rect.x + rect.width, rect.y + rect.height / 2);
                    break;
                case TextAnchor.UpperCenter:
                    result += new Vector2(rect.x + rect.width / 2, rect.y + rect.height);
                    break;
                case TextAnchor.UpperLeft:
                    result += new Vector2(rect.x, rect.y + rect.height);
                    break;
                case TextAnchor.UpperRight:
                    result += new Vector2(rect.x + rect.width, rect.y + rect.height);
                    break;
            }
            return result + Offset;
        }
    }
}
