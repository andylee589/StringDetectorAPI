using System;

namespace StringDetector.Domain.Entities {

    public interface IEntity {

        Guid Key { get; set; }
    }
}