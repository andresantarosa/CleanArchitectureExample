using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureExample.Domain.Base
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {

        }

        public DateTime DateCreated { get; private set; } = DateTime.Now;
        public bool IsActive { get; private set; } = true;
    }
}
