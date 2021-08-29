using System;

namespace ApplicationCore.Interfaces.Base
{
    public interface IBaseEntity<T>
        where T : IComparable, IConvertible, IComparable<T>, IEquatable<T>
    {
        T Id { get; set; }
    }
}
