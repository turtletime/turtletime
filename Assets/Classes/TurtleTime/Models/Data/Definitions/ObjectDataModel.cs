using UnityMVC;
using System.Collections.Generic;

namespace TurtleTime
{
    /// <summary>
    /// Represents any object's static characteristics, including its name, friendly name, width, and height.
    /// Objects are anything that holds data other than the cafe layout itself.
    /// TODO: Width/height are physical properties, move to a subclass.
    /// </summary>
    abstract class ObjectDataModel : Model
    {
        /// <summary>
        /// The name of this object, for asset loading and referencing.
        /// </summary>
        public string ID { get; protected set; }
        /// <summary>
        /// The friendly name of this object, for UI purposes.
        /// </summary>
        public string FriendlyID { get; protected set; }
        /// <summary>
        /// The width of this object.
        /// </summary>
        public int Width { get; protected set; }
        /// <summary>
        /// The height of this object.
        /// </summary>
        public int Height { get; protected set; }

        /// <summary>
        /// The kind of object this is.
        /// </summary>
        public abstract ObjectDataType DataType { get; }
        
        public override void LoadFromJson(IJsonObject jsonNode)
        {
            ID = jsonNode["id"].AsString;
            FriendlyID = jsonNode["friendlyID"].AsString;
            if (jsonNode["dimensions"] != null)
            {
                Width = jsonNode["dimensions"][0].AsInt;
                Height = jsonNode["dimensions"][1].AsInt;
            }
        }
    }

    /// <summary>
    /// All of the different types of static data types available.
    /// </summary>
    enum ObjectDataType
    {
        TURTLE, SEAT, TABLE, DECORATION, FOOD
    }

    /// <summary>
    /// Represents a normally uninteractable component of the game.
    /// </summary>
    class DecorationDataModel : ObjectDataModel
    {
        public override ObjectDataType DataType { get { return ObjectDataType.DECORATION; } }
    }

    /// <summary>
    /// Represents anything that turtles can consume.
    /// </summary>
    class FoodDataModel : ObjectDataModel
    {
        public override ObjectDataType DataType { get { return ObjectDataType.FOOD; } }
    }

    /// <summary>
    /// Represents anything that turtles can occupy when they are not moving.
    /// It's a rule that if turtles are not moving, they must be in a seat.
    /// </summary>
    class SeatDataModel : ObjectDataModel
    {
        public override ObjectDataType DataType { get { return ObjectDataType.SEAT; } }
    }

    /// <summary>
    /// Represents anything that on which food may be set. This includes countertops.
    /// </summary>
    class TableDataModel : ObjectDataModel
    {
        public override ObjectDataType DataType { get { return ObjectDataType.TABLE; } }
    }

    /// <summary>
    /// Represents turtles.
    /// </summary>
    class TurtleDataModel : ObjectDataModel
    {
        public override ObjectDataType DataType { get { return ObjectDataType.TURTLE; } }
    }
}
