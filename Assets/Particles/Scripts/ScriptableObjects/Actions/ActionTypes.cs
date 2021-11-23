using Particles.Scripts.ScriptableObjects.Actions;
using UnityEngine;

namespace Actions
{
    #region Value Action Types

    public abstract class IntAction : GenericAction<int> { }
    public abstract class FloatAction : GenericAction<float> { }
    public abstract class DoubleAction : GenericAction<double> { }
    public abstract class BoolAction : GenericAction<bool> { }
    
    public abstract class LongReturningAction : GenericReturningAction<long> { }
    public abstract class ShortReturningAction : GenericReturningAction<short> { }

    #endregion

    #region Reference Action Types

    public abstract class StringAction : GenericAction<string> { }
    public abstract class String2Action : GenericAction<string, string> { }

    public abstract class TransformAction : GenericAction<Transform> { }
    public abstract class Transform2Action : GenericAction<Transform, Transform> { }
    
    public abstract class GameObjectAction : GenericAction<GameObject> { }
    public abstract class GameObject2Action : GenericAction<GameObject, GameObject> { }
    
    public abstract class CollisionAction : GenericAction<Collision> { }
    public abstract class Collision2DAction : GenericAction<Collision2D> { }
    public abstract class ColliderAction : GenericAction<Collider> { }
    public abstract class Collider2DAction : GenericAction<Collider2D> { }
    public abstract class RigidbodyAction : GenericAction<Rigidbody> { }
    public abstract class Rigidbody2DAction : GenericAction<Rigidbody2D> { }
    
    #endregion
}