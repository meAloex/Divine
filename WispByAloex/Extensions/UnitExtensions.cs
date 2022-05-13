using Divine.Entity.Entities.Abilities;
using Divine.Entity.Entities.Abilities.Components;
using Divine.Entity.Entities.Units;
using Divine.Entity.Entities.Units.Heroes;
using Divine.Extensions;
using Divine.Numerics;

using Wisp.Info;

namespace WispByAloex.Extensions;

internal static class UnitExtensions
{
    public static float IsNetworkDirectlyFacing(this Unit source, Hero target)
{
        //var diff1 = MathUtil.DegreesToRadians(source.RotationDifference);

        //var vector1 = pos - source.Position;
        //var diff2 = Math.Abs(Math.Atan2(vector1.Y, vector1.X) - (source.NetworkRotationRad + diff1));
        //return diff2 < 0.025f;

        float deltaX = MathF.Sin(source.NetworkRotationRad);
        float deltaY = MathF.Cos(source.NetworkRotationRad);
        var SourceForward = new Vector3(deltaY, deltaX, 0f);
        var SourceRotation = Vector3.Normalize(SourceForward).ToVector2();

        var BaseVec = Vector3.Normalize(target.Position - source.Position).ToVector2();
        var Processing = Vector2.Dot(BaseVec, SourceRotation / (BaseVec.LengthSquared() * SourceRotation.LengthSquared()));

        if (Processing > 1)
        {
            Processing = 1;
        }
        var CheckAngleRad = MathF.Acos(Processing);
        var CheckAngle = (180 / MathF.PI) * CheckAngleRad;
        return CheckAngle;
    }

    public static Vector3 CustomInFront(this Unit source, float range, float angle = 0, bool rotationDifference = true)
    {
        var diff = MathUtil.DegreesToRadians((rotationDifference ? source.RotationDifference : 0) + angle);
        var alpha = source.NetworkRotationRad + diff;
        var polar = new Vector3((float)Math.Cos(alpha), (float)Math.Sin(alpha), 0);

        return source.Position + (polar * range);
    }

    //public static bool IsNetworkDirectlyFacing(this Unit source, Unit target)
    //{
    //    return source.IsNetworkDirectlyFacing(target.Position);
    //}
}