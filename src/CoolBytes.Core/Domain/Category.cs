﻿using System;
using CoolBytes.Core.Interfaces;

namespace CoolBytes.Core.Domain
{
    public class Category : ISortable
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int SortOrder { get; private set; }

        public Category(int id, string name, int sortOrder) : this(name, sortOrder) 
            => Id = id;

        public Category(string name, int sortOrder)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            if (sortOrder <= 0) throw new ArgumentOutOfRangeException(nameof(sortOrder));

            SortOrder = sortOrder;
        }

        public void UpdateName(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void SetSortOrder(int sortOrder)
        {
            if (sortOrder <= 0) throw new ArgumentOutOfRangeException(nameof(sortOrder));
            SortOrder = sortOrder;
        }
    }
}
