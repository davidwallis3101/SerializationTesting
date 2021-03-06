﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SerializationTesting
{
    /// <summary>
    /// Enum containing the possible states that the house could be in.
    /// A state can be a substate of another state.
    /// </summary>
    [Serializable]
    public enum State
    {
        UnOccupied,
        Occupied,
        Asleep,
        ChildOccupied
    }
}
