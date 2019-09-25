using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace POC.Saga.Domain
{
    public abstract class Entity
    {
        [NotMapped] public Queue<Event> Events { get; } = new Queue<Event>();
    }

    public abstract class Entity<TKey> : Entity, IEquatable<Entity<TKey>>
    {
        public TKey Id { get; private set; }

        protected Entity() { }
        protected Entity(TKey id) => Id = id;

        public override bool Equals(object obj)
            => obj is Entity<TKey> o && Equals(o);

        public bool Equals(Entity<TKey> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (other.GetType() != GetType()) return false;
            return EqualityComparer<TKey>.Default.Equals(Id, other.Id);
        }

        public override int GetHashCode()
            => EqualityComparer<TKey>.Default.GetHashCode(Id);

        public override string ToString() => $"{Id}";

        #region Static methods
        public static bool operator ==(Entity<TKey> left, Entity<TKey> right) => Equals(left, right);
        public static bool operator !=(Entity<TKey> left, Entity<TKey> right) => !Equals(left, right);
        #endregion
    }
}
