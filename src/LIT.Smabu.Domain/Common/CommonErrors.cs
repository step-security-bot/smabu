﻿using LIT.Smabu.Domain.Shared;
using LIT.Smabu.Shared;

namespace LIT.Smabu.Domain.Common
{
    public static class CommonErrors
    {
        public static readonly Error HasReferences = new("Common.HasReferences", "There are already references to other elements.");
    }
}
