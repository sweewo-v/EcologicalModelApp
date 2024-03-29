﻿using EcologicalModelApp.Domain.Models;

namespace EcologicalModelApp.Domain.Interfaces
{
    public interface IContainer
    {
        uint NumRows { get; }

        uint NumCols { get; }

        Cell GetCellAt(Coordinate coordinate);
    }
}
