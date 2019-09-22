using System;
using System.Collections.Generic;
using System.Text;

namespace Sylph
{
    /// <summary>
    /// Specifies that a property contains the basic information of a rom.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class BasicInformationAttribute : Attribute
    {
    }
    /// <summary>
    /// Specifies that the property contains extended or advanced information of a rom.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ExtendedInformationAttribute : Attribute
    {

    }
}
