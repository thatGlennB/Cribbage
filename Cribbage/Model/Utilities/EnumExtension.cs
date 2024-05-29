namespace Cribbage.Model.Utilities
{
    internal static class EnumExtension
    {
        internal static int Count(this object obj) => obj.GetType().IsEnum ? Enum.GetNames(obj.GetType()).Length : throw new ArgumentException("Invalid argument type: this method can only be used with enumerables", nameof(obj));

    }
}
